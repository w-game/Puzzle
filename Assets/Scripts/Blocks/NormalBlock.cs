using UnityEngine;

public class NormalBlock : Block
{
    protected override void SetPattern(Color color)
    {
        Pattern = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
        SpecialFrame.gameObject.SetActive(false);
    }
}