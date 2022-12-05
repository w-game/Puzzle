using UnityEngine;

public class NormalBlock : Block
{
    protected override void CalcPattern()
    {
        Pattern = GameBoard.BlockColor[Random.Range(0, GameBoard.BlockColor.Count)];
        // SetPattern($"Textures/Blocks/normal_block_{Pattern}");
        SpecialFrame.gameObject.SetActive(false);
    }
}