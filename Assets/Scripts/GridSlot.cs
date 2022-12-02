using System;
using Common;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public Vector2Int Pos { get; set; }
    public GameGrid SubGrid { get; set; }

    public GridSlot UpSlot => Pos.y > 0 ? GameBoard.Instance.GridSlots[(Pos.y - 1) * 9 + Pos.x] : null;
    public GridSlot DownSlot => Pos.y < 15 ? GameBoard.Instance.GridSlots[(Pos.y + 1) * 9 + Pos.x] : null;
    public GridSlot RightSlot => Pos.x < 8 ? GameBoard.Instance.GridSlots[Pos.y * 9 + Pos.x + 1] : null;

    public bool IsEmpty => SubGrid == null;

    internal GameGrid GenerateGrid()
    {
        SubGrid = Instantiate(GameBoard.GridPrefab, transform).GetComponent<GameGrid>();
        return SubGrid;
    }

    internal void RemoveGrid()
    {
        if (SubGrid != null)
        {
            Destroy(SubGrid.gameObject);
            SubGrid = null;
        }
    }

    internal void SetGrid(GameGrid grid)
    {
        grid.transform.SetParent(transform);
        SubGrid = grid;
        grid.transform.localPosition = Vector3.zero;
    }
}