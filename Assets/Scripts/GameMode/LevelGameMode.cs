using System.Collections.Generic;
using Blocks;
using Common;
using GameMode.LevelGame;
using UI.Popup;
using UI.View;
using UnityEngine;

public class LevelGameMode : PuzzleGame
{
    private const string Tag = "Level Game";
    public GameLevel CurLevel { get; private set; }

    private bool CheckLevelEnd()
    {
        if (GameManager.User.GameLevel >= Config.levels.Count)
        {
            UIManager.Instance.ShowTip(GameManager.Language.LevelEndTipDes, () =>
            {
                UIManager.Instance.BackToHome();
            });
            return true;
        }

        return false;
    }

    public override void StartGame()
    {
        if (CheckLevelEnd()) return;
        GameStatus = true;
        
        ClearSlots(ClearSlotType.All);
        var config = Config.levels[GameManager.User.GameLevel];
        RefreshBlockColor(config.blockCount);

        var boardData = PlayerPrefs.GetString("Game Board Data", "");
        if (!string.IsNullOrEmpty(boardData))
        {
            SLog.D(Tag, $"board data: {boardData}");
            var slotsData = boardData.Split('|');
            foreach (var data in slotsData)
            {
                var str = data.Split(':');
                var posStr = str[0].Split(',');
                int x = int.Parse(posStr[0]);
                int y = int.Parse(posStr[1]);
                var slot = BlockSlots[y * BoardWidth + x];

                var colorIndex = int.Parse(str[1]);
                slot.GenerateBlock(typeof(NormalBlock), BlockColors[colorIndex]);
            }
        }
        InitLevel();
    }
    
    public void InitLevel()
    {
        if (CheckLevelEnd()) return;

        CurLevel = CreateLevel(GameManager.User.GameLevel);
        SEvent.TrackEvent("#cur_level", new Dictionary<string, object>()
        {
            { "#level", CurLevel.LevelIndex }
        });
        AddColor(_ => _ < CurLevel.BlockCount);
        InitLevelBoard();
        CurLevel.InitGoals();
        RefreshBoard(false);
        SaveBoard();
        CheckShowNewBlockTip();

        EventCenter.Invoke(GameView.EventKeys.RefreshRoundCount, CurLevel.RoundCount);
        EventCenter.Invoke(LevelGameView.LevelEventKeys.SetGoal);
    }

    protected override void OnBlocksRemove(RemoveUnit unit)
    {
        CheckLevelGoal(unit);
    }

    protected override void OnGameOver()
    {
        EventCenter.Invoke(LevelGameView.LevelEventKeys.OnGameOver);
    }

    private void CheckLevelGoal(RemoveUnit unit)
    {
        CurLevel.CheckLevelGoal(unit);
        EventCenter.Invoke(GameView.EventKeys.RefreshGoal);
    }

    private void InitLevelBoard()
    {
        GenerateSpecialBlocks(CurLevel.SpecialBlocks, ClearSlotType.NotNormal);
    }

    private void SaveBoard()
    {
        string boardData = "";
        for (int y = BoardLength - 1; y > -1; y--)
        {
            for (int x = 0; x < BoardWidth; x++)
            {
                var slot = BlockSlots[y * BoardWidth + x];
                if (slot.SubBlock is NormalBlock)
                {
                    if (!string.IsNullOrEmpty(boardData))
                    {
                        boardData += "|";
                    }
                    boardData += $"{x},{y}:{BlockColors.IndexOf(slot.SubBlock.Pattern)}";
                }
            }
        }

        PlayerPrefs.SetString("Game Board Data", boardData);
    }

    private GameLevel CreateLevel(int levelIndex)
    {
        var config = Config.levels[GameManager.User.GameLevel];
        SLog.D("Level Game", $"当前关卡：{levelIndex} Goal Count:{config.goal.Count}");
        var gameLevel = new GameLevel
        {
            LevelIndex = levelIndex
        };
        
        gameLevel.Init(config);

        return gameLevel;
    }

    protected override void OnRoundStart()
    {
        CurLevel.RoundCount--;
        EventCenter.Invoke(GameView.EventKeys.RefreshRoundCount, CurLevel.RoundCount);
    }

    protected override void OnRoundEnd()
    {
        if (CurLevel.IsPass)
        {
            GameManager.User.GameLevel++;
            EventCenter.Invoke(LevelGameView.LevelEventKeys.OnLevelPass, Score);
            return;
        }
        
        if (!CurLevel.CheckLevelRoundCount())
        {
            GameOver();
            return;
        }

        var blockSlots = BlockSlots.FindAll(slot => slot.SubBlock && slot.SubBlock.CheckExecuteEffect());
        blockSlots.ForEach(slot => slot.SubBlock.ExecuteEffect());
        
        CurLevel.Goals.ForEach(goal => goal.Refresh());
    }

    private void CheckShowNewBlockTip()
    {
        if (Config.levelNewBlock.TryGetValue(GameManager.User.GameLevel, out var config))
        {
            UIManager.Instance.PushPop<PopNewBlockIntroduceData>(config);
        }
    }

    public void CheckClearSlots()
    {
        if (GameManager.User.ClearSlotsCount > 0)
        {
            ClearSlots(ClearSlotType.Normal);
            GameManager.User.ClearSlotsCount--;
            EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshTool);
            SaveBoard();
        }
        else
        {
            UIManager.Instance.PushPop<PopGameToolNotEnoughData>("CLEAR_SLOTS_COUNT", GameManager.Language.ToolClearSlotsName);
        }
    }

    public void CheckRefreshControl()
    {
        if (GameManager.User.ClearControlPanelCount > 0)
        {
            GridControl.Instance.Refresh();
            GameManager.User.ClearControlPanelCount--;
            EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshTool);
        }
        else
        {
            UIManager.Instance.PushPop<PopGameToolNotEnoughData>("CLEAR_CONTROL_COUNT", GameManager.Language.ToolClearControlName);
        }
    }
}