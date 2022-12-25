using System;
using Blocks;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class BlockSlot : MonoBehaviour
{
    public Vector2Int Pos { get; set; }
    public Block SubBlock { get; set; }
    public SpecialBlock SecondBlock { get; set; }
    public PuzzleGame GameBoard { get; set; }

    public BlockSlot UpSlot => Pos.y > 0 ? GameManager.Instance.GameMode.BlockSlots[(Pos.y - 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public BlockSlot DownSlot => Pos.y < PuzzleGame.BoardLength - 1 ? GameManager.Instance.GameMode.BlockSlots[(Pos.y + 1) * PuzzleGame.BoardWidth + Pos.x] : null;
    public BlockSlot RightSlot => Pos.x < PuzzleGame.BoardWidth - 1 ? GameManager.Instance.GameMode.BlockSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x + 1] : null;
    public BlockSlot LeftSlot => Pos.x > 0 ? GameManager.Instance.GameMode.BlockSlots[Pos.y * PuzzleGame.BoardWidth + Pos.x - 1] : null;

    public bool IsEmpty => SubBlock == null;
    
    public Block GenerateBlock(Type blockType, Color color)
    {
        var blockGo = Instantiate(PuzzleGame.GridPrefab, transform);
        var block = blockGo.AddComponent(blockType) as Block;
        if (block == null) block = blockGo.AddComponent<NormalBlock>();
        if (block.MainBlock)
        {
            SubBlock = block;
        }
        else
        {
            SecondBlock = block as SpecialBlock;
        }

        if (color == Color.black)
        {
            color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
        }
        
        block.Init(color);
        return block;
    }

    private void RemoveBlock(Block block, Action callback, bool anima)
    {
        if (block)
        {
            if (anima)
            {
                block.DoRemoveAnima(() =>
                {
                    OnRemoveBlockAnimaEnd(block, callback);
                });
            }
            else
            {
                OnRemoveBlockAnimaEnd(block, callback);
            }
        }
    }

    private void OnRemoveBlockAnimaEnd(Block block, Action callback)
    {
        Destroy(block.gameObject);
        callback?.Invoke();
    }

    public void RemoveMainBlock(bool anima = true)
    {
        RemoveBlock(SubBlock, () => SubBlock = null, anima);
    }

    public void RemoveSecondBlock(bool anima = true)
    {
        RemoveBlock(SecondBlock, () => SecondBlock = null, anima);
    }

    public void RemoveAllBlock(bool anima = true)
    {
        RemoveMainBlock(anima);
        RemoveSecondBlock(anima);
    }

    internal void SetGrid(Block block, bool anima = false)
    {
        block.transform.SetParent(transform);
        SubBlock = block;
        if (!anima)
        {
            block.transform.localPosition = Vector3.zero;
        }
        else
        {
            if (!SecondBlock)
            {
                block.HideSpecialIcon();
            }
            var sequence = DOTween.Sequence();
            sequence.Append(block.transform.DOLocalMove(Vector3.zero, PuzzleGame.AnimaTime).SetEase(Ease.InQuad));
            sequence.AppendCallback(() =>
            {
                if (SecondBlock)
                {
                    block.ShowSpecialIcon(SecondBlock.SpecialIcon.sprite, SecondBlock.SpecialIconColorChange);
                }
            });
        }
    }
}