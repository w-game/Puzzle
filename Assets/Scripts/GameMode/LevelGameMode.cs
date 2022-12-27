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

    private bool CheckLevelEnd()
    {
        if (GameManager.User.GameLevel >= _configs.levels.Count)
        {
            UIManager.Instance.ShowTip("您已通过现有所有的关卡！敬请期待之后的关卡更新。", () =>
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
        
        ClearSlots(ClearSlotType.All);
        var config = _configs.levels[GameManager.User.GameLevel];
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
        AddColor(_ => _ < CurLevel.BlockCount);
        InitLevelBoard();
        CurLevel.InitGoals();
        RefreshBoard(false);
        SaveBoard();
        CheckShowNewBlockTip();

        EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshRoundCount);
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
        EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshGoal);
    }

    private void InitLevelBoard()
    {
        if (CurLevel.Boards.Count == 0) return;
        
        SLog.D(Tag, $"Board Config: {CurLevel.Boards}");

        foreach (var posStr in CurLevel.Boards.Keys)
        {
            var pos = posStr.Split(',');
            int x = int.Parse(pos[0]);
            int y = int.Parse(pos[1]);
            
            var slot = BlockSlots[y * BoardWidth + x];
            slot.RemoveAllBlock(false);
        
            slot.GenerateBlock(Type.GetType($"Blocks.{CurLevel.Boards[posStr]}Block"), Color.white);
        }

        CheckComeDown();
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
        var config = _configs.levels[GameManager.User.GameLevel];
        SLog.D("Level Game", $"当前关卡：{levelIndex} Goal Count:{config.goal.Count}");
        var gameLevel = new GameLevel
        {
            LevelIndex = levelIndex
        };
        
        gameLevel.Init(config);

        return gameLevel;
    }

    protected override bool CheckGameOver()
    {
        var result = base.CheckGameOver();
        if (result) return true;
        
        return !CurLevel.CheckLevelRoundCount();
    }

    protected override void OnRoundStart()
    {
        CurLevel.RoundCount--;
        EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshRoundCount);
    }

    protected override void OnRoundEnd()
    {
        if (CurLevel.IsPass)
        {
            GameManager.User.GameLevel++;
            EventCenter.Invoke(LevelGameView.LevelEventKeys.OnLevelPass, Score);
        }
    }

    private void CheckShowNewBlockTip()
    {
        if (_configs.levelNewBlock.TryGetValue(GameManager.User.GameLevel, out var config))
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
            UIManager.Instance.PushPop<PopGameToolNotEnoughData>("CLEAR_SLOTS_COUNT", "道具「清空棋盘」数量不足，无法使用！");
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
            UIManager.Instance.PushPop<PopGameToolNotEnoughData>("CLEAR_CONTROL_COUNT", "道具「重置控制栏」数量不足，无法使用！");
        }
    }
}