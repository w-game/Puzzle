using UnityEngine;

public class NormalBlock : Block
{
    protected override void CalcPattern()
    {
        Pattern = GameBoard.BlockLabels[Random.Range(0, GameBoard.BlockLabels.Count)];
        SetPattern($"Textures/Blocks/normal_block_{Pattern}");
        SpecialFrame.gameObject.SetActive(false);
    }
}