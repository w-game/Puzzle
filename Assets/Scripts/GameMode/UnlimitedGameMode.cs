using Common;
using DG.Tweening;

public class UnlimitedGameMode : PuzzleGame
{
    private const float TrendTime = 30f;

    private Timer _trendTimer;

    protected override void AddInitGameProcess(Process process)
    {
        process.Add("", p =>
        {
            _trendTimer = new Timer(TrendTime, -1, GenerateNewRowAtBottom);
            p.Next();
        });
    }

    protected override void OnInit()
    {
    }

    protected override void OnStart()
    {
        _trendTimer.Reset();
    }

    protected override void OnRefresh()
    {
        NextRound();
    }

    protected override void OnRoundEnd()
    {
        CheckAddBlockType();
        CheckAllRemove();
    }

    protected override void OnGameOver()
    {
        UIManager.Instance.PushPop<PopGameResultData>();
        _trendTimer.Pause();
    }

    protected override void OnGameEnd()
    {
        _trendTimer.End();
    }

    public void Revive()
    {
        for (int j = BoardLength - 1; j > BoardLength - 4; j--)
        {
            for (int i = 0; i < BoardWidth; i++)
            {
                var slot = GridSlots[j * BoardWidth + i];
                slot.RemoveGrid();
            }
        }
        
        var sequence = DOTween.Sequence();
        sequence.AppendInterval(AnimaTime);
        sequence.AppendCallback(() => CheckComeDown());
        _trendTimer.Reset();
        EventCenter.Invoke(GameView.EventKeys.EnableStartBtn);
    }

    private void CheckAllRemove()
    {
        var isAllRemove = true;

        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = GridSlots[(BoardLength - 1) * BoardWidth + i];
            if (slot.SubGrid)
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
    
    private void MoveUp(GridSlot slot)
    {
        if (slot.UpSlot)
        {
            if (slot.UpSlot.SubGrid)
            {
                MoveUp(slot.UpSlot);
            }

            if (!slot.UpSlot.SubGrid)
            {
                slot.UpSlot.SetGrid(slot.SubGrid);
                slot.SubGrid = null;
            }
            else
            {
                GameOver();
            }
        }
    }
    
    private void GenerateNewRowAtBottom()
    {
        for (int i = 0; i < BoardWidth; i++)
        {
            var slot = GridSlots[(BoardLength - 1) * BoardWidth + i];
            if (slot.SubGrid)
            {
                MoveUp(slot);
            }

            if (!slot.SubGrid)
            {
                slot.GenerateGrid(BlockColors);
            }
        }
        
        DoAnima(CheckRemove);
    }
}