using Blocks;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class StaticBlockRemoveGoal : LevelGoal
    {
        public override void Init(LevelGoalConfig config, Color color)
        {
            Pattern = StaticBlock.BlockPattern;
            GoalCount = config.count;
        }

        public override string ElementPath => "StaticBlockRemoveGoal";
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.Slots[0].SubBlock is RemoveStaticBlock block)
            {
                IncreaseCount(block.Pattern, unit.BlockCount);
            }
        }
    }
}