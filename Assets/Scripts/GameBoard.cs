using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

public class GameBoard : MonoSingleton<GameBoard>
{
    public const float MOVE_SPEED = 1f;

    [SerializeField] private GridControl control;

    public static List<Color> GameColor = new List<Color>()
    {
        Color.red, Color.green, Color.blue, Color.yellow, Color.cyan
    };

    private Dictionary<GameGrid, GridSlot> _undeterminedGrids = new Dictionary<GameGrid, GridSlot>();

    public List<GridSlot> GridSlots { get; } = new List<GridSlot>();

    private Timer _moveTimer;

    public static GameObject GridPrefab { get; private set; }

    private int _score;
    public int Score
    {
        get => _score;
        set
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
        GameGrid grid;
        for (int i = 0; i < control.NextGridSlots.Count; i++)
        {
            grid = control.NextGridSlots[i].SubGrid;
            if (grid != null)
            {
                var slot = GridSlots[i];
                if (slot.SubGrid != null)
                {
                    _moveTimer?.End();
                    UIManager.Instance.PushPop<PopGameResultData>();
                    return;
                }

                var newGrid = slot.GenerateGrid();
                newGrid.Pattern = grid.Pattern;
                _undeterminedGrids.Add(newGrid, slot);
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

            foreach (var grid in new List<GameGrid>(_undeterminedGrids.Keys))
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
            CheckSameCol(GridSlots[i]);
        }

        for (int j = 0; j < 16; j++)
        {
            CheckSameRow(GridSlots[j * 9]);
        }

        foreach (var slot in removeList)
        {
            Score += 20;
            slot.RemoveGrid();
        }

        removeList.Clear();

        var result = CheckComeDown();
        if (result) CheckRemove();
    }

    private void CheckSameCol(GridSlot slot)
    {
        if (slot == null) return;
        List<GridSlot> sameSlots = new List<GridSlot>() { slot };
        while (slot.DownSlot != null)
        {
            if (slot.SubGrid != null && slot.DownSlot.SubGrid != null && slot.SubGrid.Pattern == slot.DownSlot.SubGrid.Pattern)
            {
                sameSlots.Add(slot.DownSlot);
            }
            else
            {
                break;
            }
            slot = slot.DownSlot;
        }

        if (sameSlots.Count >= 3)
        {
            removeList.AddRange(sameSlots);
        }

        CheckSameCol(sameSlots.Last().DownSlot);
    }

    private void CheckSameRow(GridSlot slot)
    {
        if (slot == null) return;
        List<GridSlot> sameSlots = new List<GridSlot>() { slot };

        while (slot.RightSlot != null)
        {
            if (slot.SubGrid != null && slot.RightSlot.SubGrid != null && slot.SubGrid.Pattern == slot.RightSlot.SubGrid.Pattern)
            {
                sameSlots.Add(slot.RightSlot);
            }
            else
            {
                break;
            }
            slot = slot.RightSlot;
        }

        if (sameSlots.Count >= 3)
        {
            removeList.AddRange(sameSlots);
        }

        CheckSameRow(sameSlots.Last().RightSlot);
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