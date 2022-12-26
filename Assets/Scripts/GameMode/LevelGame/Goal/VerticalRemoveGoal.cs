using UnityEngine;

namespace GameMode.LevelGame
{
    public class VerticalRemoveGoal : LevelGoal
    {
        public override string ElementPath => "VerticalRemoveGoal";
        public override void Init(LevelGoalConfig config, Color color)
        {
            base.Init(config, Color.white);
        }

        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.RemoveType == RemoveType.Vertical)
            {
                IncreaseCount(Color.white, 1);
            }
        }
    }
}