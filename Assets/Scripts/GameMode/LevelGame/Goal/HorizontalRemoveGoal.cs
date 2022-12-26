using UnityEngine;

namespace GameMode.LevelGame
{
    public class HorizontalRemoveGoal : LevelGoal
    {
        public override string ElementPath => "HorizontalRemoveGoal";
        public override void Init(LevelGoalConfig config, Color color)
        {
            base.Init(config, Color.white);
        }

        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.RemoveType == RemoveType.Horizontal)
            {
                IncreaseCount(Color.white, 1);
            }
        }
    }
}