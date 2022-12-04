public class AnyBlock : Block
{
    public bool Used { get; set; }
    protected override void CalcPattern()
    {
        SetPattern("Textures/any");
    }
}