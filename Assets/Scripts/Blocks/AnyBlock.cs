using Common;
using UnityEngine;

public class AnyBlock : Block
{
    public bool Used { get; set; }
    protected override void CalcPattern()
    {
        AddressableMgr.Load<Sprite>("Textures/any", sprite =>
        {
            _img.sprite = sprite;
        });
    }
}