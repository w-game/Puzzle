using Blocks;
using GameMode.EndlessGame;
using UI.View;
using UnityEngine;

public class EndlessGameMode : PuzzleGame
{
    private Challenge Challenge { get; set; }

    protected override void OnRefresh()
    {
        NextRound();
    }

    protected override void OnBlocksRemove(RemoveUnit unit)
    {
        Challenge?.CheckGoal(unit);
        EventCenter.Invoke(GameView.EventKeys.RefreshGoal);
    }

    protected override void OnRoundStart()
    {
        Challenge?.ExecuteRound();
    }

    protected override void OnRoundEnd()
    {
        CheckAddBlockType();
        CheckAllRemove();
        Challenge?.OnRoundEnd();
        CheckRefreshChallenge();
        
        var blockSlots = BlockSlots.FindAll(slot => slot.SubBlock && slot.SubBlock.CheckExecuteEffect());
        blockSlots.ForEach(slot => slot.SubBlock.ExecuteEffect());
        
        Challenge?.Goals.ForEach(goal => goal.Refresh());
        
        Challenge?.CheckResult();
    }

    protected override void OnGameOver()
    {
        Challenge?.End();
        UIManager.Instance.PushPop<PopGameResultData>();
    }

    public void Revive()
    {
        for (int j = BoardLength - 1; j > BoardLength - 4; j--)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                var slot = BlockSlots[j * BoardWidth + i];
                if (slot.SubBlock is NormalBlock)
                {
                    slot.RemoveAllBlock();
                }
            }
        }
        
        ClearSlots(ClearSlotType.NotNormal);
        GameStatus = true;

        StartCheck(true);
        EventCenter.Invoke(GameView.EventKeys.ModifyStartBtnStatus, true);
    }

    private void CheckAllRemove()
    {
        var isAllRemove = true;

        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = BlockSlots[(BoardLength - 1) * BoardWidth + i];
            if (slot.SubBlock)
            {
                isAllRemove = false;
                break;
            }
        }

        if (isAllRemove)
        {
            GameManager.User.UpdateAllRemoveCount(BlockColors.Count);
        }
    }
    
    private void CheckAddBlockType()
    {
        if (Score >= NextBlockScore)
        {
            var count = BlockColors.Count + 1;
            var color = AddColor(_ => _ < count);
            UIManager.Instance.ShowAddBlockTip(color);
        }
    }

    private int _coldDown = 3;
    private void CheckRefreshChallenge()
    {
        _coldDown--;
        if (Challenge == null && _coldDown < 0)
        {
            var value = Random.value;
            if (value > 0.1f) return;
            
            Challenge = new Challenge(Config.levels);
            Challenge.OnComplete += result =>
            {
                _coldDown = 3;
                Challenge = null;
            };
            EventCenter.Invoke(EndlessGameView.EndlessEventKeys.SetChallenge, Challenge.Goals);
        }
    }
}