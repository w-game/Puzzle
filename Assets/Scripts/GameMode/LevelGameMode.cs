using Common;
using GameMode.LevelGame;
using UI.Popup;
using UI.View;
using UnityEngine;

public class LevelGameMode : PuzzleGame
{
    private LevelConfigs _configs;
    public GameLevel CurLevel { get; private set; }

    protected override void AddInitGameProcess(Process process)
    {
        process.Add("Load Level Config", p =>
        {
            AddressableMgr.Load<TextAsset>("Config/level_config", json =>
            {
                _configs = JsonUtility.FromJson<LevelConfigs>(json.text);
                p.Next();
            });
        });
    }

    public override void Restart()
    {
        InitLevel();
    }

    protected override void OnBlocksRemove(RemoveUnit unit)
    {
        CheckLevelGoal(unit);
    }

    private void CheckLevelGoal(RemoveUnit unit)
    {
        var result = CurLevel.CheckLevelGoal(unit);
        EventCenter.Invoke(LevelGameView.EventKeys.RefreshGoal);
        if (result)
        {
            GameManager.User.GameLevel++;
            EventCenter.Invoke(LevelGameView.EventKeys.OnLevelPass, Score);
        }
    }

    public void InitLevel()
    {
        CurLevel = CreateLevel(GameManager.User.GameLevel);
        RefreshBlockColor(CurLevel.BlockCount);
        CurLevel.InitGoals();
        EventCenter.Invoke(LevelGameView.EventKeys.SetGoal);
        RefreshBoard();
    }

    private GameLevel CreateLevel(int levelIndex)
    {
        var config = _configs.levels[GameManager.User.GameLevel];
        SLog.D("Level Game", $"当前关卡：{levelIndex} Goal Count:{config.goal.Count}");
        var gameLevel = new GameLevel
        {
            LevelIndex = levelIndex
        };
        
        gameLevel.Init(config);

        return gameLevel;
    }

    public void LevelFail()
    {
        
    }
}