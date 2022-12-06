using System.Collections.Generic;
using Common;
using UnityEngine;

public class User
{
    public Dictionary<GameToolName, GameTool> Tools { get; } = new();

    private int _maxScore;
    public int MaxScore
    {
        get => _maxScore;
        set
        {
            if (value > _maxScore)
            {
                _maxScore = value;
                PlayerPrefs.SetInt("MaxScore", value);
                EventCenter.Invoke("HomeViewMaxScore");
            }
        }
    }

    public void Init()
    {
        _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Tools.Add(GameToolName.RefreshBlock, new RefreshBlock() { Number = 1 });
        Tools.Add(GameToolName.ChangeBlockLocation, new ChangeBlockLocation());
    }

    public void GiftGameTool()
    {
        var tool = (GameToolName) Random.Range(0, Tools.Count);
        Tools[tool].Number++;
        SLog.D("User", $"获得一个游戏道具 [{tool}]");
    }
    
    
}