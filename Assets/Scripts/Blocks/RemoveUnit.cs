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

    public virtual int Execute(int rate)
    {
        foreach (var slot in Slots)
        {
            slot.SubBlock?.OnRemove();
            slot.RemoveMainBlock();
        }

        GameManager.Instance.ChallengeSystem.CheckProcess(this);

        return Slots.Count * rate;
    }
}

public class SecondRemoveUnit : RemoveUnit
{
    public SecondRemoveUnit(List<BlockSlot> slots, RemoveType removeType) : base(slots, removeType)
    {
    }

    public override int Execute(int rate)
    {
        foreach (var slot in Slots)
        {
            slot.SecondBlock?.OnRemove();
            slot.RemoveSecondBlock();
        }

        GameManager.Instance.ChallengeSystem.CheckProcess(this);

        return Slots.Count * rate;
    }
}