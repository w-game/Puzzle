using System;
using System.Collections.Generic;
using Common;
using GameMode.LevelGame;
using Newtonsoft.Json;
using TapTap.Common;
using TapTap.TapDB;
using UI.Popup;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : SMonoSingleton<GameManager>
{
    /// <summary>
    /// 启动游戏间隔时间
    /// </summary>
    public int LaunchGameGap { get; private set; }
    public static User User { get; } = new();
    
    public static LanguageBase Language { get; private set; }
    public static ELanguage LanguageType
    {
        get => Language.L;
        set
        {
            PlayerPrefs.SetString("LANGUAGE", value.ToString());
            Language = LanguageBase.Languages[value];
        }
    }

    public static bool IsDebug => Debug.isDebugBuild;

    private void Awake()
    {
        var lastEndGame = PlayerPrefs.GetInt("LastEndGame", TimeUtil.Timestamp);
        LaunchGameGap = TimeUtil.Timestamp - lastEndGame;
        SLog.D("Game", $"距离上次退出游戏{LaunchGameGap}s");
        
        UIManager.Instance.PushMain<HomeViewData>();
        User.Init();
        SoundManager.Instance.Init();
        Application.targetFrameRate = 60;
        
        InitLanguage();
        LoadConfig();
        InitTapTap();
    }

    private void InitTapTap()
    {
        TapDB.Init("89uji6xvwyqlkeht2j", "taptap", Application.version, true);
        TapDB.SetUser(User.UserId);
        SEvent.TrackEvent("#app_launch", new Dictionary<string, object>()
        {
            {"#user_id", User.UserId}
        });
    }

    private void InitLanguage()
    {
        var l = PlayerPrefs.GetString("LANGUAGE", ELanguage.SimplifiedChinese.ToString());
        Enum.TryParse<ELanguage>(l, out var languageName);
        LanguageBase.Languages.TryGetValue(languageName, out var language);
        Language = language;
    }

    private void LoadConfig()
    {
        AddressableMgr.Load<TextAsset>("Config/level_config", json =>
        {
            PuzzleGame.Config = JsonConvert.DeserializeObject<LevelConfigs>(json.text);
        });
    }

    public void CheckPower(int power, Action callback)
    {
        var result = User.DecreasePower(power);
        if (result) callback?.Invoke();
        else
        {
            UIManager.Instance.PushPop<PopPowerNotEnoughData>();
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("LastEndGame", TimeUtil.Timestamp);
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SEvent.TrackEvent("#app_focus", new Dictionary<string, object>()
        {
            {"#app_focus_status", hasFocus}
        });
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        SEvent.TrackEvent("#app_pause", new Dictionary<string, object>()
        {
            {"#app_pause_status", pauseStatus}
        });
    }
}
