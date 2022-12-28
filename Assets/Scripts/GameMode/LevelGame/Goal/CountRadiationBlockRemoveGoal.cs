using Blocks;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class CountRadiationBlockRemoveGoal : RadiationBlockRemoveGoal
    {
        public override void Init(LevelGoalConfig config, Color color)
        {
            base.Init(config, color);
            GoalCount = config.count;
        }

        protected override void OnRefresh()
        {
            var count = 0;
            GameManager.Instance.PuzzleGame.BlockSlots.ForEach(slot =>
            {
                if (slot.SubBlock is RadiationBlock)
                {
                    count++;
                }
            });

            if (count == 0 && GoalCount > CurCount)
            {
                GameManager.Instance.PuzzleGame.GameOver();
            }
        }
    }
}