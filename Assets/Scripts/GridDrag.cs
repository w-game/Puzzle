using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDrag : MonoBehaviour, IDragHandler
{
    public GridControl control;
    public GridSlot Slot { get; set; }
    private Block _self;

    void Awake()
    {
        _self = GetComponent<Block>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_self.Pattern == Color.white) return;

        var gap = eventData.position.x - transform.position.x;
        SLog.D("Grid Drag", $"{gap}");
        if (gap >= 50)
        {
            var right = control.NextGridSlots.IndexOf(Slot) + 1;
            if (right < control.NextGridSlots.Count)
            {
                var rightSlot = control.NextGridSlots[right];
                if (rightSlot.SubGrid == null)
                {
                    Slot.SubGrid = null;
                    rightSlot.SetGrid(_self);
                    Slot = rightSlot;
                }
            }
        }
        else if (gap <= -50)
        {
            var left = control.NextGridSlots.IndexOf(Slot) - 1;

            if (left >= 0)
            {
                var leftSlot = control.NextGridSlots[left];
                if (leftSlot.SubGrid == null)
                {
                    Slot.SubGrid = null;
                    leftSlot.SetGrid(_self);
                    Slot = leftSlot;
                }
            }
        }
    }
}