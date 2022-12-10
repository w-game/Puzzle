using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoBehaviour
{
    [SerializeField] private Transform mainCtrlContent;
    
    public List<GridSlot> NextGridSlots { get; } = new();

    private static List<int> _indexs = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    private List<int> _remaining = new();

    public void Init()
    {
        for (int i = 0; i < GameBoard.BoardWidth; i++)
        {
            var slot = Instantiate(GameBoard.SlotPrefab, mainCtrlContent).GetComponent<GridSlot>();
            ColorUtility.TryParseHtmlString("#1E588D", out var color);
            slot.GetComponent<Image>().color = color;
            NextGridSlots.Add(slot);
        }
    }
    
    internal void NextRow()
    {
        _remaining.AddRange(_indexs);
        var num = Random.Range(1, 6);
        for (int i = 0; i < num; i++)
        {
            var index = _remaining[Random.Range(0, _remaining.Count)];
            _remaining.Remove(index);
            var slot = NextGridSlots[index];
            var grid = slot.GenerateGrid(GameBoard.BlockColor);
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
        foreach (var slot in NextGridSlots)
        {
            if (slot.SubGrid)
            {
                slot.RemoveGrid();
            }
        }
        
        NextRow();
    }
}