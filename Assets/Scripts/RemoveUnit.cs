using System.Collections.Generic;
using UnityEngine;

public enum RemoveType
{
    Horizontal,
    Vertical
}

public class RemoveUnit
{
    public RemoveType RemoveType { get; }
    public List<GridSlot> Slots { get; }
    public Color BlockIndex { get; }

    public RemoveUnit(List<GridSlot> slots, RemoveType removeType)
    {
        RemoveType = removeType;
        Slots = slots;
        BlockIndex = slots[0].SubGrid.Pattern;
    }

    public int Execute(int rate)
    {
        foreach (var slot in Slots)
        {
            slot.SubGrid?.OnRemove();
            slot.RemoveGrid();
        }

        GameManager.Instance.ChallengeSystem.CheckProcess(this);

        return Slots.Count * rate;
    }
}