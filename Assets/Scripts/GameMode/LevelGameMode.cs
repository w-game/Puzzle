using System;
using Common;
using GameMode.LevelGame;
using UI.View;
using UnityEngine;

public class LevelGameMode : PuzzleGame
{
    private const string Tag = "Level Game";
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

    public override void StartGame()
    {
        ClearSlots();
        var config = _configs.levels[GameManager.User.GameLevel];
        RefreshBlockColor(config.blockCount);
        
        var board = PlayerPrefsX.GetIntArray("Game Board", Array.Empty<int>());
        if (board.Length != 0)
        {
            SLog.D(Tag, $"board length: {board.Length}");
            for (int i = board.Length - 1; i >= 0; i--)
            {
                var colorIndex = board[i];
                if (colorIndex != -1)
                {
                    var slot = GridSlots[i];
                    slot.GenerateGrid(BlockColors[colorIndex]);
                }
            }
        }
        InitLevel();
    }

    protected override void OnBlocksRemove(RemoveUnit unit)
    {
        CheckLevelGoal(unit);
    }

    protected override void OnGameOver()
    {
        EventCenter.Invoke(LevelGameView.EventKeys.OnGameOver);
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
        AddColor(_ => _ < CurLevel.BlockCount);
        CurLevel.InitGoals();
        EventCenter.Invoke(LevelGameView.EventKeys.SetGoal);
        RefreshBoard(false);
        SaveBoard();
    }

    private void SaveBoard()
    {
        int[] boardData = new int[BoardWidth * BoardLength];
        for (int i = BoardLength - 1; i > -1; i--)
        {
            for (int j = 0; j < BoardWidth; j++)
            {
                var slot = GridSlots[i * BoardWidth + j];
                if (slot.SubGrid)
                {
                    boardData[i * BoardWidth + j] = BlockColors.IndexOf(slot.SubGrid.Pattern);
                }
                else
                {
                    boardData[i * BoardWidth + j] = -1;
                }
            }
        }

        PlayerPrefsX.SetIntArray("Game Board", boardData);
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

    protected override void OnGameEnd()
    {
        SaveBoard();
    }
}