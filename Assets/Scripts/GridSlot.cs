using Common.Blocks;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public Vector2Int Pos { get; set; }
    public Block SubGrid { get; set; }

    public GridSlot UpSlot => Pos.y > 0 ? GameBoard.Instance.GridSlots[(Pos.y - 1) * 9 + Pos.x] : null;
    public GridSlot DownSlot => Pos.y < 15 ? GameBoard.Instance.GridSlots[(Pos.y + 1) * 9 + Pos.x] : null;
    public GridSlot RightSlot => Pos.x < 8 ? GameBoard.Instance.GridSlots[Pos.y * 9 + Pos.x + 1] : null;
    public GridSlot LeftSlot => Pos.x > 0 ? GameBoard.Instance.GridSlots[Pos.y * 9 + Pos.x - 1] : null;

    public bool IsEmpty => SubGrid == null;

    internal Block GenerateGrid()
    {
        var blockGo = Instantiate(GameBoard.GridPrefab, transform);
        SubGrid = CalcBlockType(blockGo);
        
        SubGrid.Init();
        return SubGrid;
    }

    private Block CalcBlockType(GameObject go)
    {
        var value = Random.value;

        Block block;
        if (value <= 0.9f)
        {
            block = go.AddComponent<NormalBlock>();
        }
        else
        {
            block = go.AddComponent<AnyBlock>();
        }

        return block;
    }

    internal void RemoveGrid()
    {
        if (SubGrid != null)
        {
            Destroy(SubGrid.gameObject);
            SubGrid = null;
        }
    }

    internal void SetGrid(Block grid)
    {
        grid.transform.SetParent(transform);
        SubGrid = grid;
        grid.transform.localPosition = Vector3.zero;
    }
}