using UnityEngine;

namespace Blocks
{
    public abstract class StaticBlock : SpecialBlock
    {
        public static Color BlockPattern = Color.white;
        public override bool CanMove => false;
        public override bool CanRemove => false;

        protected override void SetPattern(Color color)
        {
            Pattern = BlockPattern;
        }
    }
}