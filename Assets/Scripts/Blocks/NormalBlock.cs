using UnityEngine;

namespace Blocks
{
    public class NormalBlock : Block
    {
        protected override void SetPattern(Color color)
        {
            Pattern = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
        }
    }
}