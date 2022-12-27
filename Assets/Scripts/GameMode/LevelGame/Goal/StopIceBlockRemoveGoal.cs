using Blocks;
using UI.View;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class StopIceBlockRemoveGoal : IceBlockRemoveGoal
    {
        public override string CountTipStr => "x全部";

        public override void Init(LevelGoalConfig config, Color color)
        {
            base.Init(config, color);
            OnRefresh();
        }

        protected override void OnRefresh()
        {
            GoalCount = 0;
            CurCount = 0;
            GameManager.Instance.PuzzleGame.BlockSlots.ForEach(slot =>
            {
                if (slot.SubBlock is IceBlock)
                {
                    GoalCount++;
                }
            });
            EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshGoal);
        }
    }
}