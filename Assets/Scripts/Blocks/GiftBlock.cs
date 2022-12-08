using UnityEngine;

public class GiftBlock : Block
{

    protected override void SetPattern(Color color)
    {
        Pattern = color;
        SetIcon($"Textures/Blocks/normal_block_{Pattern}");
        SetSpecialIcon("Textures/Blocks/chest");
    }

    public override void OnRemove()
    {
        GameManager.User.GiftGameTool();
    }
}