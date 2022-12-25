using System;
using Blocks;
using Common;
using GameMode.LevelGame;
using Newtonsoft.Json;
using UI.Popup;
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
                _configs = JsonConvert.DeserializeObject<LevelConfigs>(json.text);
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
                    var slot = BlockSlots[i];
                    slot.GenerateBlock(typeof(NormalBlock), BlockColors[colorIndex]);
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
        InitLevelBoard();
        CurLevel.InitGoals();
        EventCenter.Invoke(LevelGameView.EventKeys.SetGoal);
        RefreshBoard(false);
        SaveBoard();

        CheckShowNewBlockTip();
    }

    private void InitLevelBoard()
    {
        if (CurLevel.BoardIndex == -1) return;

        var levelBoard = _configs.levelBoards[CurLevel.BoardIndex];
        SLog.D(Tag, $"Board Config: {levelBoard}");

        foreach (var posStr in levelBoard.blocks.Keys)
        {
            var pos = posStr.Split(',');
            int x = int.Parse(pos[0]);
            int y = int.Parse(pos[1]);
            
            var slot = BlockSlots[y * BoardWidth + x];
            slot.RemoveAllBlock(false);
        
            slot.GenerateBlock(Type.GetType($"Blocks.{levelBoard.blocks[posStr]}Block"), Color.white);
        }

        CheckComeDown();
    }

    private void SaveBoard()
    {
        int[] boardData = new int[BoardWidth * BoardLength];
        for (int i = BoardLength - 1; i > -1; i--)
        {
            for (int j = 0; j < BoardWidth; j++)
            {
                var slot = BlockSlots[i * BoardWidth + j];
                if (slot.SubBlock)
                {
                    boardData[i * BoardWidth + j] = BlockColors.IndexOf(slot.SubBlock.Pattern);
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

    private void CheckShowNewBlockTip()
    {
        if (_configs.levelNewBlock.TryGetValue(GameManager.User.GameLevel, out var config))
        {
            UIManager.Instance.PushPop<PopNewBlockIntroduceData>(config);
        }
    }
}