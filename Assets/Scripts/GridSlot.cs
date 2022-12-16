using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridSlot : MonoBehaviour
{
    public Vector2Int Pos { get; set; }
    public Block SubGrid { get; set; }

    public GridSlot UpSlot => Pos.y > 0 ? GameManager.Instance.GameMode.GridSlots[(Pos.y - 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public GridSlot DownSlot => Pos.y < PuzzleGame.BoardLength - 1 ? GameManager.Instance.GameMode.GridSlots[(Pos.y + 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public GridSlot RightSlot => Pos.x < PuzzleGame.BoardWidth - 1 ? GameManager.Instance.GameMode.GridSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x + 1] : null;
    public GridSlot LeftSlot => Pos.x > 0 ? GameManager.Instance.GameMode.GridSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x - 1] : null;

    public bool IsEmpty => SubGrid == null;

    internal Block GenerateGrid(List<Color> blockColors)
    {
        var blockGo = Instantiate(PuzzleGame.GridPrefab, transform);
        SubGrid = CalcBlockType(blockGo);

        var color = blockColors[Random.Range(0, blockColors.Count)];
        SubGrid.Init(color);

        return SubGrid;
    }

    private Block CalcBlockType(GameObject go)
    {
        // var value = Random.value;

        Block block = go.AddComponent<NormalBlock>();
        // if (value <= 0.9f)
        // {
        //     block = go.AddComponent<NormalBlock>();
        // }
        // else
        // {
        //     value = Random.value;
        //     if (value >= 0.5f)
        //     {
        //         block = go.AddComponent<AnyBlock>();
        //     }
        //     else
        //     {
        //         block = go.AddComponent<GiftBlock>();
        //     }
        // }

        return block;
    }

    internal void RemoveGrid()
    {
        if (SubGrid)
        {
            var block = SubGrid;
            SubGrid = null;
            var anima = block.transform.DOScale(0, PuzzleGame.AnimaTime).SetEase(Ease.InQuad);
            anima.onComplete += () =>
            {
                Destroy(block.gameObject);
                anima.Kill();
            };
        }
    }

    internal void SetGrid(Block grid, bool anima = false)
    {
        grid.transform.SetParent(transform);
        SubGrid = grid;
        if (!anima)
        {
            grid.transform.localPosition = Vector3.zero;
        }
        else
        {
            grid.transform.DOLocalMove(Vector3.zero, PuzzleGame.AnimaTime).SetEase(Ease.InQuad);
        }
    }
}