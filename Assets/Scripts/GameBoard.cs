using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class GameBoard : MonoSingleton<GameBoard>
{
    private const float MOVE_SPEED = 1f;
    public static GameObject GridPrefab { get; private set; }

    [SerializeField] private GridControl control;

    public GridControl Control => control;

    public static List<Color> GameColor = new() { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan };

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
            UIEvent.Invoke("RefreshScore");
        }
    }

    public void Init(Action callback)
    {
        Score = 0;
        AddressableMgr.Load<GameObject>("Prefabs/GridSlot", prefab =>
        {
            for (int j = 0; j < 16; j++)
            {
                for (int i = 0; i < 9; i++)
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
                control.NextRow();
                callback?.Invoke();
            });
        });
    }

    public void RefreshBoard()
    {
        Score = 0;
        _undeterminedGrids.Clear();
        foreach (var slot in GridSlots)
        {
            slot.RemoveGrid();
        }
        control.NextRow();
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
                slot.SetGrid(grid);
                _undeterminedGrids.Add(grid, slot);
            }
        }

        control.NextRow();
        StartMove();
    }

    private void StartMove()
    {
        _moveTimer?.End();
        _moveTimer = new Timer(MOVE_SPEED, -1, () =>
        {
            if (_undeterminedGrids.Count == 0)
            {
                CheckRemove();
                _moveTimer?.End();
                GenerateNewRow();
                return;
            }

            foreach (var grid in new List<Block>(_undeterminedGrids.Keys))
            {
                var slot = _undeterminedGrids[grid];
                if (CheckCanMove(slot))
                {
                    _undeterminedGrids.Remove(slot.SubGrid);
                    continue;
                }

                var nextSlot = GridSlots[(slot.Pos.y + 1) * 9 + slot.Pos.x];
                nextSlot.SetGrid(grid);
                slot.SubGrid = null;
                _undeterminedGrids[grid] = nextSlot;
            }
        });
    }

    private bool CheckCanMove(GridSlot slot)
    {
        return slot.Pos.y >= 15 || GridSlots[(slot.Pos.y + 1) * 9 + slot.Pos.x].SubGrid != null;
    }

    private List<GridSlot> removeList = new List<GridSlot>();

    private void CheckRemove()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                CheckSameCol(GridSlots[j * 9 + i]);
                CheckSameRow(GridSlots[j * 9 + i]);
            }
        }

        foreach (var slot in removeList)
        {
            Score += 20;
            slot.SubGrid?.OnRemove();
            slot.RemoveGrid();
        }

        removeList.Clear();

        var result = CheckComeDown();
        if (result) CheckRemove();
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
            removeList.AddRange(sameSlots);

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
            removeList.AddRange(sameSlots);
            
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
        for (int j = 15; j >= 0; j--)
        {
            for (int i = 0; i < 9; i++)
            {
                var slot = GridSlots[j * 9 + i];

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
}