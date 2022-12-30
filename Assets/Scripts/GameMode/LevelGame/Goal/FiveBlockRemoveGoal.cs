using Common.GameMode;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class FiveBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "FiveBlockRemoveGoal";
        public override void Init(LevelGoalConfig config, Color color)
        {
            base.Init(config, Color.white);
        }

        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.BlockCount == 5)
            {
                IncreaseCount(Color.white, 1);
            }
        }
    }
}