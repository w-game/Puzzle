using UnityEngine;
using UnityEngine.EventSystems;

public class GridDrag : MonoBehaviour, IDragHandler
{
    public GridControl control;
    public GridSlot Slot { get; set; }
    public void OnDrag(PointerEventData eventData)
    {
        var gap = eventData.position.x - transform.position.x;
        if (gap >= 50)
        {
            MoveBlock(Slot, 1);
        }
        
        else if (gap <= -50)
        {
            MoveBlock(Slot, -1);
        }
    }

    private void MoveBlock(GridSlot slot, int dir)
    {
        var index = control.NextGridSlots.IndexOf(slot) + dir;
        if (index < 0 || index > control.NextGridSlots.Count - 1) return;
        
        var nextSlot = control.NextGridSlots[index];
        if (nextSlot)
        {
            if (nextSlot.SubGrid)
            {
                MoveBlock(nextSlot, dir);
            }
            
            if (!nextSlot.SubGrid)
            {
                nextSlot.SetGrid(slot.SubGrid);
                nextSlot.SubGrid.GetComponent<GridDrag>().Slot = nextSlot;
                slot.SubGrid = null;
            }
        }
    }
}