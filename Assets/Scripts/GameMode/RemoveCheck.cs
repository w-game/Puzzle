using System.Collections.Generic;
using UnityEngine;

public class RemoveCheck
{
    private int _width;
    private int _lenght;
    
    private List<RemoveUnit> _removeList = new();
    private List<GridSlot> GridSlots { get; set; }
    
    private Color _curCheckColor;
    
    public RemoveCheck(int width, int lenght)
    {
        _width = width;
        _lenght = lenght;
    }
    
    public List<RemoveUnit> Check(List<GridSlot> slots)
    {
        GridSlots = slots;
        _removeList.Clear();
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _lenght; j++)
            {
                CheckSameCol(GridSlots[j * _width + i]);
                CheckSameRow(GridSlots[j * _width + i]);
            }
        }

        return _removeList;
    }
    
    private GridSlot GetRowNextSlot(Vector2Int pos)
    {
        if (pos.x < 0) pos.x = _width - 1;
        if (pos.x > _width - 1) pos.x = 0;
        return GridSlots[pos.y * _width + pos.x];
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

    private void CheckSameRow(GridSlot slot)
    {
        if (!slot || !slot.SubGrid) return;

        List<GridSlot> sameSlots = new List<GridSlot>() { slot };

        if (slot.SubGrid is not AnyBlock)
        {
            _curCheckColor = slot.SubGrid.Pattern;

            var originSlot = slot;
            while (GetRowNextSlot(slot.Pos + Vector2Int.left).SubGrid)
            {
                if (GetRowNextSlot(slot.Pos + Vector2Int.left).SubGrid.Pattern == _curCheckColor ||
                    GetRowNextSlot(slot.Pos + Vector2Int.left).SubGrid is AnyBlock { Used: false })
                {
                    slot = GetRowNextSlot(slot.Pos + Vector2Int.left);
                    sameSlots.Add(slot);
                }
                else
                {
                    break;
                }
            }

            slot = originSlot;
            while (GetRowNextSlot(slot.Pos + Vector2Int.right).SubGrid)
            {
                if (GetRowNextSlot(slot.Pos + Vector2Int.right).SubGrid.Pattern == _curCheckColor ||
                    GetRowNextSlot(slot.Pos + Vector2Int.right).SubGrid is AnyBlock { Used: false })
                {
                    slot = GetRowNextSlot(slot.Pos + Vector2Int.right);
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
}