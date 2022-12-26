using Blocks;
using UnityEngine;

namespace GameMode.LevelGame
{
    public class ShadowBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "SingleColorRemoveGoal";
        public override void Init(LevelGoalConfig config, Color color)
        {
            SpritePath = "Textures/shadow_block";
            Pattern = new Color(0.5f, 0.5f, 0.5f);
            GameManager.Instance.PuzzleGame.BlockSlots.ForEach(slot =>
            {
                if (slot.SecondBlock is ShadowBlock)
                {
                    GoalCount++;
                }
            });
        }
        
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit is SecondRemoveUnit u)
            {
                if (u.Slots[0].SecondBlock is ShadowBlock block)
                {
                    IncreaseCount(block.Pattern, u.BlockCount);
                }
            }
        }
    }
}