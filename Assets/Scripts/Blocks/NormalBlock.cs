using UnityEngine;

public class NormalBlock : Block
{
    protected override void SetPattern(Color color)
    {
        Pattern = GameBoard.BlockColor[Random.Range(0, GameBoard.BlockColor.Count)];
        SpecialFrame.gameObject.SetActive(false);
    }
}