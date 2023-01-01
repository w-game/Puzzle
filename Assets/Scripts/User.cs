using System.Collections.Generic;
using Common;
using GameMode.EndlessGame;
using TapTap.Common;
using UnityEngine;

public class User
{
    private const string Tag = "User";
    public const int MaxPower = 30;
    public const int InitialPower = 10;
    public Dictionary<GameToolName, GameTool> Tools { get; } = new();

    public string UserId
    {
        get
        {
            var id = PlayerPrefs.GetString("USER_ID", "");
            if (string.IsNullOrEmpty(id))
            {
                id = TapUUID.UUID();
                PlayerPrefs.SetString("USER_ID", id);
            }

            return id;
        }
    }

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
                EventCenter.Invoke(HomeView.EventKeys.RefreshView);
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

    public bool NewPlayerGuide
    {
        get => PlayerPrefs.GetInt("NEW_PLAYER_GUIDE", 1) == 1;
        set => PlayerPrefs.SetInt("NEW_PLAYER_GUIDE", value ? 1 : 0);
    }

    public bool IsNewPlayer
    {
        get => PlayerPrefs.GetInt("NEW_PLAYER", 1) == 1;
        set => PlayerPrefs.SetInt("NEW_PLAYER", value ? 1 : 0);
    }

    public bool PrivacyPolicy
    {
        get => PlayerPrefs.GetInt("PrivacyPolicy", 0) == 0;
        set => PlayerPrefs.SetInt("PrivacyPolicy", value ? 0 : 1);
    }

    public int GameLevel
    {
        get => PlayerPrefs.GetInt("GameLevel", 0);
        set => PlayerPrefs.SetInt("GameLevel", value);
    }

    private int _power;

    public int Power
    {
        get => _power;
        private set
        {
            SLog.D(Tag, $"修改体力 修改的预定值value: {value}");
            if (value > MaxPower)
            {
                _power = MaxPower;
                _powerTimer.Pause();
            }
            else
            {
                _power = value;
                _powerTimer.Reset();
            }
            PlayerPrefs.SetInt("Power", _power);
        }
    }

    public int ClearSlotsCount
    {
        get => PlayerPrefs.GetInt("CLEAR_SLOTS_COUNT", 3);
        set => PlayerPrefs.SetInt("CLEAR_SLOTS_COUNT", value);
    }
    
    public int ClearControlPanelCount
    {
        get => PlayerPrefs.GetInt("CLEAR_CONTROL_COUNT", 3);
        set => PlayerPrefs.SetInt("CLEAR_CONTROL_COUNT", value);
    }

    private Timer _powerTimer;

    public void Init()
    {
        InitPower();

        _maxScore = PlayerPrefs.GetInt("MaxScore", 0);
        Tools.Add(GameToolName.RefreshBlock, new RefreshBlock() { Number = 1 });
        Tools.Add(GameToolName.ChangeBlockLocation, new ChangeBlockLocation());
    }

    private void InitPower()
    {
        _powerTimer = new Timer(10 * 60, -1, () =>
        {
            Power++;
        });
        _power = PlayerPrefs.GetInt("Power", InitialPower);

        var gap = PlayerPrefs.GetInt("OfflineAccTime", 0);
        gap += GameManager.Instance.LaunchGameGap;
        var endGamePower = gap / 600;
        gap -= endGamePower * 600;
        SLog.D(Tag, $"线下剩余时间数: {gap}s");
        PlayerPrefs.SetInt("OfflineAccTime", gap);
        SLog.D(Tag, $"Power 线下增加的体力数: {endGamePower}");
        Power += endGamePower;
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
        EventCenter.Invoke(HomeView.EventKeys.RefreshView);
    }

    public void IncreasePower(int power)
    {
        Power += power;
    }

    public bool DecreasePower(int power)
    {
        if (_power < power) return false;

        Power -= power;
        return true;
    }
}