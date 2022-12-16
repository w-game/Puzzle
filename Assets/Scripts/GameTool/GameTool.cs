public enum GameToolName
{
    RefreshBlock,
    ChangeBlockLocation
}

public abstract class GameTool
{
    public int Number { get; set; }

    public bool CheckUse()
    {
        if (Number <= 0) return false;
        
        Use();
        Number--;
        return true;
    }
    protected abstract void Use();
}

public class RefreshBlock : GameTool
{
    protected override void Use()
    {
        GridControl.Instance.Regenerate();
        // GameBoard.Instance.Control.Regenerate();
    }
}

public class ChangeBlockLocation : GameTool
{
    protected override void Use()
    {
        
    }
}
