using Blocks;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class IceBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "SingleColorRemoveGoal";

        public override void Init(LevelGoalConfig config, Color color)
        {
            SpritePath = "Textures/icon_ice";
            Pattern = Color.white;
        }

        protected override void IncreaseCount(RemoveUnit unit)
        {
            var count = 0;
            unit.Slots.ForEach(slot =>
            {
                if (slot.SubBlock is IceBlock)
                {
                    count++;
                }
            });
            IncreaseCount(Color.white, count);
        }
    }
}