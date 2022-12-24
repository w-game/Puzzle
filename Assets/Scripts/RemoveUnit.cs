using System.Collections.Generic;
using UnityEngine;

public enum RemoveType
{
    Horizontal,
    Vertical,
    Special
}

public class RemoveUnit
{
    public RemoveType RemoveType { get; }
    public List<BlockSlot> Slots { get; }
    public Color BlockIndex { get; }
    public int BlockCount => Slots.Count;

    public RemoveUnit(List<BlockSlot> slots, RemoveType removeType)
    {
        RemoveType = removeType;
        Slots = slots;
        BlockIndex = slots[0].SubBlock.Pattern;
    }

    public int Execute(int rate)
    {
        foreach (var slot in Slots)
        {
            slot.SubBlock?.OnRemove();
            slot.RemoveBlock();
        }

        GameManager.Instance.ChallengeSystem.CheckProcess(this);

        return Slots.Count * rate;
    }
}