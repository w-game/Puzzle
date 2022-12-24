using System.Linq;
using Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    public GridControl control;
    public BlockSlot Slot { get; set; }

    private Vector2 _lastPos;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastPos = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        var gap = eventData.position.x - transform.position.x;

        if (gap >= 80)
        {
            MoveBlock(Slot, 1);
        }
        else if (gap <= -80)
        {
            MoveBlock(Slot, -1);
        }

        _lastPos = eventData.position;
    }

    private void MoveBlock(BlockSlot slot, int dir)
    {
        var index = control.NextGridSlots.IndexOf(slot) + dir;
        BlockSlot nextSlot = null;
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
            if (nextSlot.SubBlock)
            {
                MoveBlock(nextSlot, dir);
            }
            
            if (!nextSlot.SubBlock)
            {
                nextSlot.SetGrid(slot.SubBlock);
                nextSlot.SubBlock.GetComponent<GridDrag>().Slot = nextSlot;
                slot.SubBlock = null;
                SoundManager.Instance.PlaySlideSound();
            }
        }
    }
}