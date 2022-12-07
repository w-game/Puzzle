using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoard : MonoSingleton<GameBoard>
{
    public static GameObject GridPrefab { get; private set; }
    public const float MoveTime = 0.3f;
    private const float TrendTime = 30f;

    [SerializeField] private GridControl control;

    public const int BoardWidth = 9;
    public const int BoardLength = 9;
    public const int BlockSize = 160;

    public GridControl Control => control;

    public static readonly List<Color> BlockColor = new();

    private readonly Dictionary<Block, GridSlot> _undeterminedGrids = new();

    public List<GridSlot> GridSlots { get; } = new();

    private int _score;
    public int Score
    {
        get => _score;
        private set
        {
            _score = value;
            EventCenter.Invoke("RefreshScore");
        }
    }
    
    private Timer _bottomGenerateTimer;
    private bool GameStatus { get; set; }
    
    private void Awake()
    {
        _bottomGenerateTimer = new Timer(TrendTime, -1, GenerateNewRowAtBottom);
        var rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(
            BlockSize * BoardWidth + (BoardWidth - 1) * 10,
            BlockSize * BoardLength + (BoardLength - 1) * 10);
    }

    public void Init(Action callback)
    {
        Score = 0;
        AddressableMgr.Load<GameObject>("Prefabs/GridSlot", prefab =>
        {
            for (int j = 0; j < BoardLength; j++)
            {
                for (int i = 0; i < BoardWidth; i++)
                {
                    var slot = Instantiate(prefab, transform).GetComponent<GridSlot>();
                    slot.name = $"GridSlot_{i}_{j}";
                    slot.Pos = new Vector2Int(i, j);
                    GridSlots.Add(slot);
                }
            }

            AddressableMgr.Load<GameObject>("Prefabs/Grid", prefab =>
            {
                GridPrefab = prefab;
                RefreshBoard();
                callback?.Invoke();
            });
        });
    }

    public void RefreshBoard()
    {
        RefreshBlockColor();
        
        Score = 0;
        _undeterminedGrids.Clear();
        foreach (var slot in GridSlots)
        {
            slot.RemoveGrid();
        }
        EventCenter.Invoke("EnableStartBtn");
        control.Refresh();
        _bottomGenerateTimer?.Play();
        
        GenerateNewRow();
        GameStatus = true;
    }
    
    public void Revive()
    {
        GameStatus = true;
        EventCenter.Invoke("EnableStartBtn");
        _bottomGenerateTimer.Play();
        
        for (int j = BoardLength - 1; j > BoardLength - 4; j--)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                var slot = GridSlots[j * BoardWidth + i];
                slot.RemoveGrid();
            }
        }
        
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(MoveTime);
        sequence.AppendCallback(() => CheckComeDown());
    }

    private void RefreshBlockColor()
    {
        BlockColor.Clear();
        while (BlockColor.Count < 3)
        {
            var colorCode = ColorLibrary.ColorCoder[Random.Range(0, ColorLibrary.ColorCoder.Count)];
            ColorUtility.TryParseHtmlString(colorCode, out var color);
            if (!BlockColor.Contains(color)) BlockColor.Add(color);
        }
    }

    public void GenerateNewRow()
    {
        for (int i = 0; i < control.NextGridSlots.Count; i++)
        {
            var ctrlSlot = control.NextGridSlots[i];
            var grid = ctrlSlot.SubGrid;
            if (grid != null)
            {
                var slot = GridSlots[i];

                ctrlSlot.SubGrid = null;
                Destroy(grid.gameObject.GetComponent<GridDrag>());
                slot.SetGrid(grid);
                _undeterminedGrids.Add(grid, slot);
            }
        }

        control.NextRow();
        StartMove();
    }

    private void StartMove()
    {
        foreach (var grid in _undeterminedGrids.Keys)
        {
            var slot = _undeterminedGrids[grid];
            var downSlot = slot;
            while (downSlot.DownSlot && downSlot.DownSlot.SubGrid == null)
            {
                downSlot = downSlot.DownSlot;
            }

            slot.SubGrid = null;
            downSlot.SetGrid(grid, true);
        }
        _undeterminedGrids.Clear();
        InvokeCheckRemove();
    }

    private List<RemoveUnit> _removeList = new();

    private void InvokeCheckRemove()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(MoveTime);
        sequence.AppendCallback(CheckRemove);
    }

    private void CheckRemove()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            for (int j = 0; j < BoardLength; j++)
            {
                CheckSameCol(GridSlots[j * BoardWidth + i]);
                CheckSameRow(GridSlots[j * BoardWidth + i]);
            }
        }

        var rate = 1;
        if (_removeList.Count != 0) SoundManager.Instance.PlayRemoveSound();
        foreach (var removeUnit in _removeList)
        {
            Score += removeUnit.Execute(rate);
            rate++;
        }

        _removeList.Clear();
        
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(MoveTime);
        sequence.AppendCallback(() =>
        {
            var result = CheckComeDown();
            if (result) InvokeCheckRemove();
            else OperationComplete();
        });
    }
    
    private Color _curCheckColor;
    private void CheckSameCol(GridSlot slot)
    {
        if (!slot || !slot.SubGrid) return;

        List<GridSlot> sameSlots = new List<GridSlot>() { slot };
        
        if (slot.SubGrid is not AnyBlock)
        {
            _curCheckColor = slot.SubGrid.Pattern;

            var originSlot = slot;
            while (slot.UpSlot && slot.UpSlot.SubGrid)
            {
                if (slot.UpSlot.SubGrid.Pattern == _curCheckColor ||
                    slot.UpSlot.SubGrid is AnyBlock { Used: false })
                {
                    slot = slot.UpSlot;
                    sameSlots.Add(slot);
                }
                else
                {
                    break;
                }
            }

            slot = originSlot;
            while (slot.DownSlot && slot.DownSlot.SubGrid)
            {
                if (slot.DownSlot.SubGrid.Pattern == _curCheckColor ||
                    slot.DownSlot.SubGrid is AnyBlock { Used: false})
                {
                    slot = slot.DownSlot;
                    sameSlots.Add(slot);
                }
                else
                {
                    break;
                }
            }
        }

        if (sameSlots.Count >= 3)
        {
            if (CheckSlotStatus(sameSlots)) return;
            RemoveUnit removeUnit = new RemoveUnit(sameSlots, RemoveType.Vertical);
            _removeList.Add(removeUnit);

            foreach (var s in sameSlots)
            {
                if (s.SubGrid is AnyBlock anyBlock)
                {
                    anyBlock.Used = true;
                }
            }
        }
    }

    private void CheckSameRow(GridSlot slot)
    {
        if (!slot || !slot.SubGrid) return;

        List<GridSlot> sameSlots = new List<GridSlot>() { slot };

        if (slot.SubGrid is not AnyBlock)
        {
            _curCheckColor = slot.SubGrid.Pattern;

            var originSlot = slot;
            while (slot.LeftSlot && slot.LeftSlot.SubGrid)
            {
                if (slot.LeftSlot.SubGrid.Pattern == _curCheckColor ||
                    slot.LeftSlot.SubGrid is AnyBlock { Used: false })
                {
                    slot = slot.LeftSlot;
                    sameSlots.Add(slot);
                }
                else
                {
                    break;
                }
            }

            slot = originSlot;
            while (slot.RightSlot && slot.RightSlot.SubGrid)
            {
                if (slot.RightSlot.SubGrid.Pattern == _curCheckColor ||
                    slot.RightSlot.SubGrid is AnyBlock { Used: false })
                {
                    slot = slot.RightSlot;
                    sameSlots.Add(slot);
                }
                else
                {
                    break;
                }
            }
        }

        if (sameSlots.Count >= 3)
        {
            if (CheckSlotStatus(sameSlots)) return;
            RemoveUnit removeUnit = new RemoveUnit(sameSlots, RemoveType.Horizontal);
            _removeList.Add(removeUnit);
            
            foreach (var s in sameSlots)
            {
                if (s.SubGrid is AnyBlock anyBlock)
                {
                    anyBlock.Used = true;
                }
            }
        }
    }

    private bool CheckSlotStatus(List<GridSlot> sameSlots)
    {
        var unit = _removeList.Find(_ =>
        {
            var result = false;
            foreach (var sameSlot in sameSlots)
            {
                result = _.Slots.Contains(sameSlot);
            }

            return result;
        });

        return unit != null;
    }

    private bool CheckComeDown()
    {
        bool result = false;
        for (int j = BoardLength - 1; j >= 0; j--)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                var slot = GridSlots[j * BoardWidth + i];

                if (slot.SubGrid != null)
                {
                    if (slot.DownSlot != null && slot.DownSlot.SubGrid == null)
                    {
                        GridSlot bottom = slot.DownSlot;
                        while (bottom.DownSlot != null && bottom.DownSlot.IsEmpty)
                        {
                            bottom = bottom.DownSlot;
                        }

                        bottom.SetGrid(slot.SubGrid, true);
                        slot.SubGrid = null;

                        result = true;
                    }
                }
            }
        }

        return result;
    }

    public void DoFastDown()
    {
        foreach (var grid in _undeterminedGrids.Keys)
        {
            var slot = _undeterminedGrids[grid];
            slot.SubGrid = null;
            while (slot.DownSlot != null && slot.DownSlot.SubGrid == null)
            {
                slot = slot.DownSlot;
            }

            slot.SetGrid(grid);
        }

        _undeterminedGrids.Clear();
    }

    private void GenerateNewRowAtBottom()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = GridSlots[(BoardLength - 1) * BoardWidth + i];
            if (slot.SubGrid)
            {
                MoveUp(slot);
            }

            if (!slot.SubGrid)
            {
                slot.GenerateGrid();
            }
        }
        
        InvokeCheckRemove();
    }

    private void MoveUp(GridSlot slot)
    {
        if (slot.UpSlot)
        {
            if (slot.UpSlot.SubGrid)
            {
                MoveUp(slot.UpSlot);
            }

            if (!slot.UpSlot.SubGrid)
            {
                slot.UpSlot.SetGrid(slot.SubGrid);
                slot.SubGrid = null;
            }
            else
            {
                GameOver();
            }
        }
    }
    
    private void OperationComplete()
    {
        CheckGameOver();
        CheckAddBlockType();
        EventCenter.Invoke("EnableStartBtn");
    }

    private void CheckAddBlockType()
    {
        if (_score >= 256 && BlockColor.Count < 4)
        {
            AddColor();
        }

        if (_score >= 512 && BlockColor.Count < 5)
        {
            AddColor();
        }
        
        if (_score >= 1024 && BlockColor.Count < 6)
        {
            AddColor();
        }
    }

    private void AddColor()
    {
        while (true)
        {
            var colorCode = ColorLibrary.ColorCoder[Random.Range(0, ColorLibrary.ColorCoder.Count)];
            ColorUtility.TryParseHtmlString(colorCode, out var color);
            if (!BlockColor.Contains(color))
            {
                BlockColor.Add(color);
                break;
            }
        }
    }

    private void CheckGameOver()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = GridSlots[i];
            if (slot.SubGrid)
            {
                GameOver();
                return;
            }
        }
    }

    private void GameOver()
    {
        if (!GameStatus) return;
        GameStatus = false;
        _bottomGenerateTimer.Pause();
        UIManager.Instance.PushPop<PopGameResultData>();
        GameManager.User.MaxScore = _score;
    }
    
    public void Stop()
    {
        _bottomGenerateTimer.End();
    }
}