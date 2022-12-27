using UnityEngine;

namespace Blocks
{
    public class NormalBlock : Block
    {
        protected override void SetPattern(Color color)
        {
            Pattern = color;
        }
    }
}