using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] private List<GridSlot> nextGridSlots;
    public List<GridSlot> NextGridSlots => nextGridSlots;

    private static List<int> _indexs = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    private List<int> _remaining = new();
    internal void NextRow()
    {
        _remaining.AddRange(_indexs);
        var num = Random.Range(1, 6);
        for (int i = 0; i < num; i++)
        {
            var index = _remaining[Random.Range(0, _remaining.Count)];
            _remaining.Remove(index);
            var slot = nextGridSlots[index];
            var grid = slot.GenerateGrid();
            var drag = grid.gameObject.AddComponent<GridDrag>();
            drag.control = this;
            drag.Slot = slot;
        }

        _remaining.Clear();
    }

    public void Regenerate()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (var slot in nextGridSlots)
        {
            if (slot.SubGrid)
            {
                slot.RemoveGrid();
            }
        }
        
        NextRow();
    }
}