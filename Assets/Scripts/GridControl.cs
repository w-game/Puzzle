using System.Collections.Generic;
using Blocks;
using Common;
using UnityEngine;
using UnityEngine.UI;

public class GridControl : MonoSingleton<GridControl>
{
    [SerializeField] private Transform mainCtrlContent;
    [SerializeField] private Transform secondaryCtrlContent;
    
    public List<BlockSlot> NextGridSlots { get; } = new();
    private List<BlockSlot> NextNextGridSlots { get; } = new();

    private static List<int> _indexs = new() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    private List<int> _remaining = new();

    public void Init()
    {
        for (int i = 0; i < PuzzleGame.BoardWidth; i++)
        {
            var slot = Instantiate(PuzzleGame.SlotPrefab, mainCtrlContent).GetComponent<BlockSlot>();
            ColorUtility.TryParseHtmlString("#0B2837", out var color);
            slot.GetComponent<Image>().color = color;
            NextGridSlots.Add(slot);
        }

        for (int i = 0; i < PuzzleGame.BoardWidth; i++)
        {
            var slot = Instantiate(PuzzleGame.SlotPrefab, secondaryCtrlContent).GetComponent<BlockSlot>();
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
            if (slot.SubBlock)
            {
                target.SetGrid(slot.SubBlock);
                slot.SubBlock = null;
                
                target.SubBlock.transform.localScale = Vector3.one;
                var drag = target.SubBlock.gameObject.AddComponent<GridDrag>();
                drag.control = this;
                drag.Slot = target;
            }
        }

        GeneratePreview();
    }

    private void GeneratePreview()
    {
        _remaining.AddRange(_indexs);
        var num = Random.Range(1, 6);
        for (int i = 0; i < num; i++)
        {
            var index = _remaining[Random.Range(0, _remaining.Count)];
            _remaining.Remove(index);
            var slot = NextNextGridSlots[index];
            var block = slot.GenerateBlock(typeof(NormalBlock), PuzzleGame.RandomColor);
            block.transform.localScale *= 0.3f;
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
            slot.RemoveAllBlock(false);
        }

        foreach (var slot in NextNextGridSlots)
        {
            slot.RemoveAllBlock(false);
        }
        
        GeneratePreview();
        
        NextRow();
    }
}