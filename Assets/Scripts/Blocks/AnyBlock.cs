using UnityEngine;

public class AnyBlock : Block
{
    public bool Used { get; set; }

    protected override void SetPattern(Color color)
    {
        SetIcon("Textures/any");
    }
}