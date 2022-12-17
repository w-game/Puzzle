using System.Collections.Generic;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoSingleton<GridControl>
{
    [SerializeField] private Transform mainCtrlContent;
    [SerializeField] private Transform secondaryCtrlContent;
    
    public List<GridSlot> NextGridSlots { get; } = new();
    private List<GridSlot> NextNextGridSlots { get; } = new();

    private static List<int> _indexs = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    private List<int> _remaining = new();

    public void Init()
    {
        for (int i = 0; i < PuzzleGame.BoardWidth; i++)
        {
            var slot = Instantiate(PuzzleGame.SlotPrefab, mainCtrlContent).GetComponent<GridSlot>();
            ColorUtility.TryParseHtmlString("#0B2837", out var color);
            slot.GetComponent<Image>().color = color;
            NextGridSlots.Add(slot);
        }

        for (int i = 0; i < PuzzleGame.BoardWidth; i++)
        {
            var slot = Instantiate(PuzzleGame.SlotPrefab, secondaryCtrlContent).GetComponent<GridSlot>();
            slot.GetComponent<RectTransform>().sizeDelta *= 0.3f;
            NextNextGridSlots.Add(slot);
        }
    }

    public void NextRow()
    {
        for (int i = 0; i < PuzzleGame.BoardWidth; i++)
        {
            var slot = NextNextGridSlots[i];
            var target = NextGridSlots[i];
            if (slot.SubGrid)
            {
                target.SetGrid(slot.SubGrid);
                slot.SubGrid = null;
                
                target.SubGrid.transform.localScale = Vector3.one;
                var drag = target.SubGrid.gameObject.AddComponent<GridDrag>();
                drag.control = this;
                drag.Slot = target;
            }
        }

        GeneratePreview();
    }
    
    public void GeneratePreview()
    {
        _remaining.AddRange(_indexs);
        var num = Random.Range(1, 6);
        for (int i = 0; i < num; i++)
        {
            var index = _remaining[Random.Range(0, _remaining.Count)];
            _remaining.Remove(index);
            var slot = NextNextGridSlots[index];
            var grid = slot.GenerateGrid(PuzzleGame.BlockColors);
            grid.transform.localScale *= 0.3f;
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

        foreach (var slot in NextNextGridSlots)
        {
            if (slot.SubGrid)
            {
                slot.RemoveGrid();
            }
        }
        
        GeneratePreview();
        
        NextRow();
    }
}