using UnityEngine;

public class NormalBlock : Block
{
    protected override void SetPattern(Color color)
    {
        Pattern = PuzzleGame.BlockColor[Random.Range(0, PuzzleGame.BlockColor.Count)];
        SpecialFrame.gameObject.SetActive(false);
    }
}