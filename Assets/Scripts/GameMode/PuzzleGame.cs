using System;
using System.Collections.Generic;
using Blocks;
using Common;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public enum PuzzleGameMode
{
    Level = 0,
    Unlimited = 1
}

public enum ClearSlotType
{
    All,
    Normal,
    NotNormal
}

public abstract class PuzzleGame : MonoBehaviour
{
    public static class PowerCost
    {
        public const int Level = 1;
        public const int Unlimited = 2;
    }
    public const int BoardWidth = 9;
    public const int BoardLength = 9;
    public const float AnimaTime = 0.3f;
    private const int BlockSize = 80;

    public static GameObject GridPrefab { get; private set; }
    public static GameObject SlotPrefab { get; private set; }
    
    public static readonly List<Color> BlockColors = new();
    public static Color RandomColor => BlockColors[Random.Range(0, BlockColors.Count)];
    public List<BlockSlot> BlockSlots { get; } = new();
    public int Score { get; private set; }
    public float NextBlockScore => Mathf.Pow(4, BlockColors.Count + 1);
    
    
    private static RectTransform _rect;
    
    private readonly Dictionary<Block, BlockSlot> _undeterminedGrids = new();
    private GridControl Control => GridControl.Instance;

    private RemoveCheck _removeCheck = new(BoardWidth, BoardLength);

    public event Action OnGameInit;

    private Process _initGameProcess;
    private void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _rect.sizeDelta = new Vector2(
            BlockSize * BoardWidth + (BoardWidth - 1) * 10,
            BlockSize * BoardLength + (BoardLength - 1) * 10);
    }

    protected virtual void AddInitGameProcess(Process process) { }
    protected virtual void OnInit() { }
    protected virtual void OnStart() { }
    protected virtual void OnRefresh() { }
    protected virtual void OnBlocksRemove(RemoveUnit unit) { }
    protected virtual void OnRoundStart() { }
    protected virtual void OnRoundEnd() { }
    protected virtual void OnGameOver() { }
    protected virtual void OnGameEnd() { }
    
    public void Init()
    {
        _initGameProcess = new Process();
        _initGameProcess.Add("", p =>
        {
            AddressableMgr.Load<GameObject>("Prefabs/GridSlot", prefab =>
            {
                SlotPrefab = prefab;
                for (int j = 0; j < BoardLength; j++)
                {
                    for (int i = 0; i < BoardWidth; i++)
                    {
                        var slot = Instantiate(SlotPrefab, transform).GetComponent<BlockSlot>();
                        slot.name = $"GridSlot_{i}_{j}";
                        slot.Pos = new Vector2Int(i, j);
                        BlockSlots.Add(slot);
                    }
                }
                
                p.Next();
            });
        });
        _initGameProcess.Add("", p =>
        {
            AddressableMgr.Load<GameObject>("Prefabs/Block", gridPrefab =>
            {
                GridPrefab = gridPrefab;
                GridControl.Instance.Init();
                
                p.Next();
            });
        });
        AddInitGameProcess(_initGameProcess);
        _initGameProcess.Add("", p =>
        {
            OnGameInit?.Invoke();
        });
        _initGameProcess.Start();
    }

    public virtual void StartGame()
    {
        RefreshBlockColor();
        RefreshBoard();
        OnStart();
    }

    protected void RefreshBoard(bool clearSlots = true)
    {
        Score = 0;
        _undeterminedGrids.Clear();
        if (clearSlots) ClearSlots(ClearSlotType.All);
        
        Control.Refresh();
        
        EventCenter.Invoke(GameView.EventKeys.EnableStartBtn);
        EventCenter.Invoke(GameView.EventKeys.RefreshView);
        
        OnRefresh();
    }

    /// <summary>
    /// 清除棋盘
    /// </summary>
    protected void ClearSlots(ClearSlotType type)
    {
        switch (type)
        {
            case ClearSlotType.All:
                BlockSlots.ForEach(slot => slot.RemoveAllBlock(false));
                break;
            case ClearSlotType.Normal:
                BlockSlots.ForEach(slot =>
                {
                    if (slot.SubBlock is NormalBlock)
                    {
                        slot.RemoveMainBlock(false);
                    }
                });
                break;
            case ClearSlotType.NotNormal:
                BlockSlots.ForEach(slot =>
                {
                    if (slot.SubBlock is not NormalBlock)
                    {
                        slot.RemoveMainBlock(false);
                    }
                });
                break;
        }
    }

    protected void RefreshBlockColor(int count = 3)
    {
        BlockColors.Clear();
        ColorLibrary.InitThemeColorCoder();
        AddColor(_ => _ < count);
    }
    
    protected Color AddColor(Predicate<int> match)
    {
        Color color = default;
        while (match(BlockColors.Count))
        {
            var colorLib = BlockColors.Count < 4 ? ColorLibrary.ThemeColorCoder : ColorLibrary.RandomColorCoder;
            var colorCode = colorLib[Random.Range(0, colorLib.Count)];
            ColorUtility.TryParseHtmlString(colorCode, out color);
            if (!BlockColors.Contains(color)) BlockColors.Add(color);
        }

        return color;
    }
    
    public void NextRound()
    {
        OnRoundStart();
        for (int i = 0; i < Control.NextGridSlots.Count; i++)
        {
            var ctrlSlot = Control.NextGridSlots[i];
            var grid = ctrlSlot.SubBlock;
            if (grid != null)
            {
                var slot = BlockSlots[i];

                ctrlSlot.SubBlock = null;
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
            while (downSlot.DownSlot && downSlot.DownSlot.SubBlock == null)
            {
                downSlot = downSlot.DownSlot;
            }

            slot.SubBlock = null;
            downSlot.SetGrid(grid, true);
        }
        _undeterminedGrids.Clear();
        DoAnima(CheckRemove);
    }
    
    protected void DoAnima(TweenCallback callback)
    {
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(AnimaTime);
        sequence.AppendCallback(callback);
    }
    
    protected void CheckRemove()
    {
        var removeUnits = _removeCheck.Check(BlockSlots);

        List<RemoveUnit> staticBlocksRemoveUnits = new List<RemoveUnit>();
        var rate = 1;
        foreach (var removeUnit in removeUnits)
        {
            OnBlocksRemove(removeUnit);
            
            EventCenter.Invoke(GameView.EventKeys.CheckCombo);
            _removeCheck.CheckRemoveStaticBlocks(staticBlocksRemoveUnits, removeUnit.Slots);
            Score += removeUnit.Execute(rate);
            rate++;
        }

        foreach (var removeUnit in staticBlocksRemoveUnits)
        {
            OnBlocksRemove(removeUnit);
            Score += removeUnit.Execute(rate);
            rate++;
        }
        
        if (removeUnits.Count != 0)
        {
            SoundManager.Instance.PlayRemoveSound();
            EventCenter.Invoke(GameView.EventKeys.RefreshView);
        }

        DoAnima(CheckComeDown);
    }

    protected void CheckComeDown()
    {
        bool result = false;
        for (int j = BoardLength - 1; j >= 0; j--)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                var slot = BlockSlots[j * BoardWidth + i];

                if (slot.SubBlock != null && slot.SubBlock.CanMove)
                {
                    if (slot.DownSlot != null && slot.DownSlot.SubBlock == null)
                    {
                        BlockSlot bottom = slot.DownSlot;
                        while (bottom.DownSlot != null && bottom.DownSlot.IsEmpty)
                        {
                            bottom = bottom.DownSlot;
                        }

                        bottom.SetGrid(slot.SubBlock, true);
                        slot.SubBlock = null;

                        result = true;
                    }
                }
            }
        }

        if (result) DoAnima(CheckRemove);
        else EndRound();
    }

    
    /// <summary>
    /// 回合结束
    /// </summary>
    private void EndRound()
    {
        var result = CheckGameOver();
        if (result) GameOver();
        else OnRoundEnd();
        
        EventCenter.Invoke(GameView.EventKeys.RefreshView);
        EventCenter.Invoke(GameView.EventKeys.EnableStartBtn);
    }
    
    protected virtual bool CheckGameOver()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = BlockSlots[i];
            if (slot.SubBlock)
            {
                return true;
            }
        }

        return false;
    }

    public void GameOver()
    {
        // if (!GameStatus) return;
        // GameStatus = false;
        GameManager.User.MaxScore = Score;
        OnGameOver();
    }

    public void EndGame()
    {
        OnGameEnd();
    }
}