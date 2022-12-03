using UnityEngine;

namespace Common.Blocks
{
    public class NormalBlock : Block
    {
        protected override void CalcPattern()
        {
            Pattern = GameBoard.GameColor[Random.Range(0, GameBoard.GameColor.Count)];
        }
    }
}