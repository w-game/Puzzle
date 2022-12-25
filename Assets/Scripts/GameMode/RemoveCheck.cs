using System.Collections.Generic;
using Blocks;
using UnityEngine;

public class RemoveCheck
{
    private int _width;
    private int _lenght;
    
    private List<RemoveUnit> _removeList = new();
    private List<BlockSlot> GridSlots { get; set; }
    
    private Color _curCheckColor;
    
    public RemoveCheck(int width, int lenght)
    {
        _width = width;
        _lenght = lenght;
    }
    
    public List<RemoveUnit> Check(List<BlockSlot> slots)
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
    
    private BlockSlot GetRowNextSlot(Vector2Int pos)
    {
        if (pos.x < 0) pos.x = _width - 1;
        if (pos.x > _width - 1) pos.x = 0;
        return GridSlots[pos.y * _width + pos.x];
    }
    
    private BlockSlot GetSlotByPos(Vector2Int pos)
    {
        if (pos.y < 0 || pos.y > _lenght - 1) return null;
        
        if (pos.x < 0) pos.x = _width - 1;
        if (pos.x > _width - 1) pos.x = 0;
        return GridSlots[pos.y * _width + pos.x];
    }

    private bool CheckSlotStatus(List<BlockSlot> sameSlots)
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

    private void CheckSameRow(BlockSlot slot)
    {
        if (!slot || !slot.SubBlock || !slot.SubBlock.CanRemove) return;

        List<BlockSlot> sameSlots = new List<BlockSlot>() { slot };

        if (slot.SubBlock is not AnyBlock)
        {
            _curCheckColor = slot.SubBlock.Pattern;

            var originSlot = slot;
            while (GetRowNextSlot(slot.Pos + Vector2Int.left).SubBlock)
            {
                if (!GetRowNextSlot(slot.Pos + Vector2Int.left).SubBlock.CanRemove)
                {
                    break;
                }
                
                if (GetRowNextSlot(slot.Pos + Vector2Int.left).SubBlock.Pattern == _curCheckColor ||
                    GetRowNextSlot(slot.Pos + Vector2Int.left).SubBlock is AnyBlock { Used: false })
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
            while (GetRowNextSlot(slot.Pos + Vector2Int.right).SubBlock)
            {
                if (!GetRowNextSlot(slot.Pos + Vector2Int.right).SubBlock.CanRemove)
                {
                    break;
                }
                
                if (GetRowNextSlot(slot.Pos + Vector2Int.right).SubBlock.Pattern == _curCheckColor ||
                    GetRowNextSlot(slot.Pos + Vector2Int.right).SubBlock is AnyBlock { Used: false })
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
            List<BlockSlot> secondSlots = new List<BlockSlot>();
            _removeList.Add(removeUnit);
            
            foreach (var s in sameSlots)
            {
                if (s.SubBlock is AnyBlock anyBlock)
                {
                    anyBlock.Used = true;
                }
                
                if (s.SecondBlock)
                {
                    secondSlots.Add(s);
                }
            }
            
            if (secondSlots.Count != 0)
            {
                _removeList.Add(new SecondRemoveUnit(secondSlots, RemoveType.Special));
            }
        }
    }

    private void CheckSameCol(BlockSlot slot)
    {
        if (!slot || !slot.SubBlock) return;

        List<BlockSlot> sameSlots = new List<BlockSlot>() { slot };
        
        if (slot.SubBlock is not AnyBlock)
        {
            _curCheckColor = slot.SubBlock.Pattern;

            var originSlot = slot;
            while (slot.UpSlot && slot.UpSlot.SubBlock)
            {
                if (slot.UpSlot.SubBlock.Pattern == _curCheckColor ||
                    slot.UpSlot.SubBlock is AnyBlock { Used: false })
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
            while (slot.DownSlot && slot.DownSlot.SubBlock)
            {
                if (slot.DownSlot.SubBlock.Pattern == _curCheckColor ||
                    slot.DownSlot.SubBlock is AnyBlock { Used: false})
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
            List<BlockSlot> secondSlots = new List<BlockSlot>();
            _removeList.Add(removeUnit);

            foreach (var s in sameSlots)
            {
                if (s.SubBlock is AnyBlock anyBlock)
                {
                    anyBlock.Used = true;
                }

                if (s.SecondBlock)
                {
                    secondSlots.Add(s);
                }
            }

            if (secondSlots.Count != 0)
            {
                _removeList.Add(new SecondRemoveUnit(secondSlots, RemoveType.Special));
            }
        }
    }
    
    public void CheckRemoveStaticBlocks(List<RemoveUnit> units, List<BlockSlot> slots)
    {
        List<BlockSlot> removeSlots = new List<BlockSlot>();
        foreach (var slot in slots)
        {

            CheckRemoveStaticBlock(removeSlots, slot.Pos + Vector2Int.left);
            CheckRemoveStaticBlock(removeSlots, slot.Pos + Vector2Int.right);
            CheckRemoveStaticBlock(removeSlots, slot.Pos + Vector2Int.up);
            CheckRemoveStaticBlock(removeSlots, slot.Pos + Vector2Int.down);
        }

        if (removeSlots.Count != 0) units.Add(new RemoveUnit(removeSlots, RemoveType.Special));
    }
    
    private void CheckRemoveStaticBlock(List<BlockSlot> slots, Vector2Int pos)
    {
        var slot = GetSlotByPos(pos);
        if (slot && slot.SubBlock is RemoveStaticBlock)
        {
            slots.Add(slot);
        }
    }
}