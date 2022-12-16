using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class PuzzleGame : MonoBehaviour
{
    public const int BoardWidth = 9;
    public const int BoardLength = 9;
    public const float AnimaTime = 0.3f;
    private const int BlockSize = 80;

    public static GameObject GridPrefab { get; private set; }
    public static GameObject SlotPrefab { get; private set; }
    
    public static readonly List<Color> BlockColor = new();
    public List<GridSlot> GridSlots { get; } = new();
    public int Score { get; private set; }
    public float NextBlockScore => Mathf.Pow(4, BlockColor.Count + 1);
    
    
    private static RectTransform _rect;
    
    private readonly Dictionary<Block, GridSlot> _undeterminedGrids = new();
    private GridControl Control => GridControl.Instance;

    private RemoveCheck _removeCheck = new(BoardWidth, BoardLength);

    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _rect.sizeDelta = new Vector2(
            BlockSize * BoardWidth + (BoardWidth - 1) * 10,
            BlockSize * BoardLength + (BoardLength - 1) * 10);
    }
    
    protected virtual void OnInit() { }
    protected virtual void OnStart() { }
    protected virtual void OnRefresh() { }
    protected virtual void OnRoundEnd() { }
    protected virtual void OnGameOver() { }
    
    public void Init(Action callback)
    {
        AddressableMgr.Load<GameObject>("Prefabs/GridSlot", prefab =>
        {
            SlotPrefab = prefab;
            for (int j = 0; j < BoardLength; j++)
            {
                for (int i = 0; i < BoardWidth; i++)
                {
                    var slot = Instantiate(SlotPrefab, transform).GetComponent<GridSlot>();
                    slot.name = $"GridSlot_{i}_{j}";
                    slot.Pos = new Vector2Int(i, j);
                    GridSlots.Add(slot);
                }
            }

            AddressableMgr.Load<GameObject>("Prefabs/Grid", gridPrefab =>
            {
                GridPrefab = gridPrefab;
                GridControl.Instance.Init();
                OnInit();
                callback?.Invoke();
            });
        });
    }
    
    public void Restart()
    {
        RefreshBlockColor();
        RefreshBoard();
        OnStart();
    }

    private void RefreshBoard()
    {
        Score = 0;
        
        _undeterminedGrids.Clear();
        foreach (var slot in GridSlots)
        {
            slot.RemoveGrid();
        }
        
        EventCenter.Invoke("EnableStartBtn");
        EventCenter.Invoke("RefreshGameView");
        Control.Refresh();
        
        OnRefresh();
    }

    protected void RefreshBlockColor()
    {
        BlockColor.Clear();

        var colorLib = ColorLibrary.FirstColorCoder[Random.Range(0, ColorLibrary.FirstColorCoder.Count)];
        AddColor(_ => _ < 3, colorLib);
    }
    
    protected Color AddColor(Predicate<int> match, List<string> colorLib)
    {
        Color color = default;
        while (match(BlockColor.Count))
        {
            var colorCode = colorLib[Random.Range(0, colorLib.Count)];
            ColorUtility.TryParseHtmlString(colorCode, out color);
            if (!BlockColor.Contains(color)) BlockColor.Add(color);
        }

        return color;
    }
    
    public void NextRound()
    {
        for (int i = 0; i < Control.NextGridSlots.Count; i++)
        {
            var ctrlSlot = Control.NextGridSlots[i];
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

        Control.NextRow();
        DownBlocks();
    }
    
    private void DownBlocks()
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
    
    protected void InvokeCheckRemove()
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(AnimaTime);
        sequence.AppendCallback(CheckRemove);
    }
    
    private void CheckRemove()
    {
        var removeUnits = _removeCheck.Check(GridSlots);

        var rate = 1;
        foreach (var removeUnit in removeUnits)
        {
            EventCenter.Invoke("CheckCombo");
            Score += removeUnit.Execute(rate);
            rate++;
        }
        
        if (removeUnits.Count != 0)
        {
            SoundManager.Instance.PlayRemoveSound();
            EventCenter.Invoke("RefreshGameView");
        }
        
        removeUnits.Clear();
        
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(AnimaTime);
        sequence.AppendCallback(() =>
        {
            var result = CheckComeDown();
            if (result) InvokeCheckRemove();
            else EndRound();
        });
    }

    protected bool CheckComeDown()
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

    
    /// <summary>
    /// 回合结束
    /// </summary>
    private void EndRound()
    {
        CheckGameOver();
        OnRoundEnd();
        EventCenter.Invoke("RefreshGameView");
        EventCenter.Invoke("EnableStartBtn");
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

    protected void GameOver()
    {
        // if (!GameStatus) return;
        // GameStatus = false;
        UIManager.Instance.PushPop<PopGameResultData>();
        GameManager.User.MaxScore = Score;
        OnGameOver();
    }
}