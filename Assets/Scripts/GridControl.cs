using System.Collections.Generic;
using Common;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    [SerializeField] private List<GridSlot> nextGridSlots;
    public List<GridSlot> NextGridSlots => nextGridSlots;

    private static List<int> indexs = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
    private List<int> remaining = new List<int>();
    internal void NextRow()
    {
        foreach (var slot in nextGridSlots)
        {
            slot.RemoveGrid();
        }

        remaining.AddRange(indexs);
        var num = Random.Range(1, 6);
        for (int i = 0; i < num; i++)
        {
            var index = remaining[Random.Range(0, remaining.Count)];
            remaining.Remove(index);
            var slot = nextGridSlots[index];
            var grid = slot.GenerateGrid();
            var drag = grid.gameObject.AddComponent<GridDrag>();
            drag.control = this;
            drag.Slot = slot;
            grid.Pattern = GameBoard.GameColor[Random.Range(0, GameBoard.GameColor.Count)];
        }

        remaining.Clear();


        // SLog.D("Grid Control", $"{remaining.Count}");
        // for (int i = 0; i < remaining.Count; i++)
        // {
        //     nextGrids[remaining[i]].Pattern = Color.white;
        // }
    }
}