using UnityEngine;

public class GiftBlock : Block
{
    protected override void CalcPattern()
    {
        Pattern = GameBoard.BlockLabels[Random.Range(0, GameBoard.BlockLabels.Count)];
        SetPattern($"Textures/Blocks/normal_block_{Pattern}");
        SetSpecialIcon("Textures/Blocks/chest");
    }

    public override void OnRemove()
    {
        GameManager.User.GiftGameTool();
    }
}