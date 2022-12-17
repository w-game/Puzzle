using Common;
using GameMode.LevelGame;
using UI.Popup;
using UnityEngine;

public class LevelGameMode : PuzzleGame
{
    private LevelConfigs _configs;
    private GameLevel _curLevel;
    public GameLevel CurLevel
    {
        get => _curLevel;
        private set
        {
            if (value == null) return;
            _curLevel = value;
            EventCenter.Invoke("SetGoal");
        }
    }

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

    protected override void OnStart()
    {
        NextLevel();
    }

    protected override void OnBlocksRemove(RemoveUnit unit)
    {
        CheckLevelGoal(unit);
    }

    private void CheckLevelGoal(RemoveUnit unit)
    {
        var result = CurLevel.CheckLevelGoal(unit);
        EventCenter.Invoke("RefreshGoal");
        if (result)
        {
            GameManager.User.GameLevel++;
            UIManager.Instance.PushPop<PopPassLevelData>(Score);
        }
    }

    public void NextLevel()
    {
        CurLevel = CreateLevel(GameManager.User.GameLevel);
        ClearSlots();
    }

    private GameLevel CreateLevel(int levelIndex)
    {
        var config = _configs.levels[GameManager.User.GameLevel];
        SLog.D("Level Game", $"当前关卡：{levelIndex} Goal Type:{config.goal.type}");
        var gameLevel = new GameLevel
        {
            LevelIndex = levelIndex
        };
        
        gameLevel.Init(config);

        return gameLevel;
    }
}