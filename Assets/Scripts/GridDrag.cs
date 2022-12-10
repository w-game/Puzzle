using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public GridControl control;
    public GridSlot Slot { get; set; }

    private Vector2 _lastPos;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var gap = eventData.position.x - transform.position.x;

        if (gap >= 60)
        {
            MoveBlock(Slot, 1);
        }
        else if (gap <= -60)
        {
            MoveBlock(Slot, -1);
        }

        _lastPos = eventData.position;
    }

    private void MoveBlock(GridSlot slot, int dir)
    {
        var index = control.NextGridSlots.IndexOf(slot) + dir;
        GridSlot nextSlot = null;
        if (index >= 0 && index <= control.NextGridSlots.Count - 1)
        {
            nextSlot = control.NextGridSlots[index];
        }


        if (slot != Slot && !nextSlot)
        {
            if (dir == 1)
            {
                nextSlot = control.NextGridSlots.First();
            }
            else if (dir == -1)
            {
                nextSlot = control.NextGridSlots.Last();
            }
        }
        
        
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
                SoundManager.Instance.PlaySlideSound();
            }
        }
    }
}