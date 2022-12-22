using System;
using Common;
using GameMode.LevelGame;
using GameSystem;
using UI.Popup;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    /// 启动游戏间隔时间
    /// </summary>
    public int LaunchGameGap { get; private set; }
    public static User User { get; } = new();
    
    public PuzzleGame GameMode { get; set; }

    public ChallengeSystem ChallengeSystem { get; } = new();
    void Awake()
    {
        var lastEndGame = PlayerPrefs.GetInt("LastEndGame", TimeUtil.Timestamp);
        LaunchGameGap = TimeUtil.Timestamp - lastEndGame;
        SLog.D("Game", $"距离上次退出游戏{LaunchGameGap}s");
        
        UIManager.Instance.PushMain<HomeViewData>();
        User.Init();
        SoundManager.Instance.Init();
        Application.targetFrameRate = 60;
        
        new SingleColorRemoveGoal();
        new ThreeBlockRemoveGoal();
        new HorizontalRemoveGoal();
        new VerticalRemoveGoal();
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
}
