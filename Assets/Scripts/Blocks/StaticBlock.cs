using UnityEngine;

namespace Blocks
{
    public class StaticBlock : Block
    {
        public override bool CanMove => false;
        public override bool CanRemove => false;

        protected override void SetPattern(Color color)
        {
            Pattern = Color.white;
        }
    }
}