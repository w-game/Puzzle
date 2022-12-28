using Common;
using UnityEngine;

namespace Blocks
{
    public class ShadowBlock : SpecialBlock
    {
        public override bool SpecialIconColorChange => true;
        public static Color BlockPattern = new(0.5f, 0.5f, 0.5f);

        public override bool MainBlock => false;

        protected override void SetPattern(Color color)
        {
            SLog.D("Block", "Shadow Block created");
            Pattern = BlockPattern;
            SetSpecialIcon("Textures/shadow_block", Color.white);
        }
    }
}