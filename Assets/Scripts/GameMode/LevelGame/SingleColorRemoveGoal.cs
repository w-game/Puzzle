
using Common;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class SingleColorRemoveGoal : LevelGoal
    {
        public override string ElementPath => "SingleColorRemoveGoal";

        public override bool CheckStatus(RemoveUnit unit)
        {
            IncreaseCount(unit.BlockIndex, unit.BlockCount);

            foreach (var key in GoalCount.Keys)
            {
                if (CurCount[key] < GoalCount[key])
                {
                    return false;
                }
            }

            Complete();
            return true;
        }

        public override void Complete()
        {
            IsComplete = true;
        }
    }
}