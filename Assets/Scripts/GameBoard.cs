using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoard : MonoSingleton<GameBoard>
{
    public static GameObject GridPrefab { get; private set; }

    [SerializeField] private GridControl control;

    public const int BoardWidth = 9;
    public const int BoardLength = 9;

    public GridControl Control => control;

    public static List<Color> BlockColor = new();

    private Dictionary<Block, GridSlot> _undeterminedGrids = new();

    public List<GridSlot> GridSlots { get; } = new();

    private Timer _moveTimer;

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
                if (slot.SubGrid != null)
                {
                    _moveTimer?.End();
                    UIManager.Instance.PushPop<PopGameResultData>();
                    return;
                }

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
        sequence.AppendInterval(0.3f);
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

        foreach (var removeUnit in _removeList)
        {
            Score += removeUnit.Execute(1);
        }

        _removeList.Clear();

        var result = CheckComeDown();
        if (result) InvokeCheckRemove();
        else OperationComplete();
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

                        bottom.SetGrid(slot.SubGrid);
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
    
    private void OperationComplete()
    {
        EventCenter.Invoke("EnableStartBtn");
    }
}