using System;
using Blocks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSlot : MonoBehaviour
{
    public Vector2Int Pos { get; set; }
    public Block SubBlock { get; set; }

    public BlockSlot UpSlot => Pos.y > 0 ? GameManager.Instance.GameMode.BlockSlots[(Pos.y - 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public BlockSlot DownSlot => Pos.y < PuzzleGame.BoardLength - 1 ? GameManager.Instance.GameMode.BlockSlots[(Pos.y + 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public BlockSlot RightSlot => Pos.x < PuzzleGame.BoardWidth - 1 ? GameManager.Instance.GameMode.BlockSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x + 1] : null;
    public BlockSlot LeftSlot => Pos.x > 0 ? GameManager.Instance.GameMode.BlockSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x - 1] : null;

    public bool IsEmpty => SubBlock == null;

    internal Block GenerateGrid()
    {
        var blockGo = Instantiate(PuzzleGame.GridPrefab, transform);
        SubBlock = CalcBlockType(blockGo);

        var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
        SubBlock.Init(color);

        return SubBlock;
    }

    internal Block GenerateBlock(Type blockType)
    {
        var blockGo = Instantiate(PuzzleGame.GridPrefab, transform);
        SubBlock = blockGo.AddComponent(blockType) as Block;
        if (SubBlock == null) SubBlock = blockGo.AddComponent<NormalBlock>();
        
        var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
        SubBlock.Init(color);
        return SubBlock;
    }
    
    internal Block GenerateGrid(Color color)
    {
        var blockGo = Instantiate(PuzzleGame.GridPrefab, transform);
        SubBlock = CalcBlockType(blockGo);

        SubBlock.Init(color);

        return SubBlock;
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

    internal void RemoveBlock()
    {
        if (SubBlock)
        {
            var block = SubBlock;
            SubBlock = null;
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
        SubBlock = grid;
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