using System.Collections.Generic;

public class User
{
    public Dictionary<GameToolName, GameTool> Tools { get; } = new();

    public void Init()
    {
        Tools.Add(GameToolName.RefreshBlock, new RefreshBlock() {Number = 1});
        Tools.Add(GameToolName.ChangeBlockLocation, new ChangeBlockLocation());
    }
}