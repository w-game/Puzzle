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
                EventCenter.Invoke("RefreshView");
            }
        }
    }

    public int AllRemoveCount
    {
        get
        {
            PlayerPrefs.GetInt("AllRemoveCount", 0);
            var level = 3;
            var count = 0;
            while (true)
            {
                if (!PlayerPrefs.HasKey($"AllRemoveCount_{level}")) break;
                count += PlayerPrefs.GetInt($"AllRemoveCount_{level}", 0);
                level++;
            }

            return count;
        }
    }

    public bool IsNewPlayer
    {
        get
        {
            var status = PlayerPrefs.GetInt($"NewPlayer", 0);
            return status == 0;
        }
    }

    public bool PrivacyPolicy
    {
        get => PlayerPrefs.GetInt("PrivacyPolicy", 0) == 0;
        set => PlayerPrefs.SetInt("PrivacyPolicy", value ? 0 : 1);
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

    public void UpdateAllRemoveCount(int blockColorCount)
    {
        var count = PlayerPrefs.GetInt($"AllRemoveCount_{blockColorCount}", 0) + 1;
        PlayerPrefs.SetInt($"AllRemoveCount_{blockColorCount}", count);
        EventCenter.Invoke("RefreshView");
    }

    public void SetOldPlayer()
    {
        PlayerPrefs.SetInt("NewPlayer", 1);
    }
}