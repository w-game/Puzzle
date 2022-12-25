using Common;
using UnityEngine;

namespace Blocks
{
    public class ShadowBlock : Block
    {
        public override bool MainBlock => false;

        protected override void SetPattern(Color color)
        {
            SLog.D("Block", "Shadow Block created");
            Pattern = new Color(0.5f, 0.5f, 0.5f);
            SetSpecialIcon("Textures/shadow_block");
        }
    }
}