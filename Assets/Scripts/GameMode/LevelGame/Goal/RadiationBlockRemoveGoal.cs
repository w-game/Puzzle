using Blocks;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class RadiationBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "SingleColorRemoveGoal";

        public override void Init(LevelGoalConfig config, Color color)
        {
            SpritePath = "Textures/icon_radiation";
            Pattern = Color.white;
        }

        protected override void IncreaseCount(RemoveUnit unit)
        {
            var count = 0;
            unit.Slots.ForEach(slot =>
            {
                if (slot.SubBlock is RadiationBlock)
                {
                    count++;
                }
            });
            IncreaseCount(Color.white, count);
        }
    }
}