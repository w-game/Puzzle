using Blocks;
using UnityEngine;

namespace GameMode.EndlessGame
{
    public class GenerateRandomNormalBlockAtTop : Punishment
    {
        public override string Tip => GameManager.Language.GenerateRandomNormalBlockAtTopPunishmentTip;
        public override void Execute()
        {
            for (int i = 0; i < PuzzleGame.BoardWidth; i++)
            {
                var value = Random.value;
                if (value > 0.5f)
                {
                    var slot = PuzzleGame.Cur.BlockSlots[i];
                    slot.GenerateBlock(typeof(NormalBlock), PuzzleGame.RandomColor);
                }
            }

            PuzzleGame.Cur.StartCheck(true);
        }
    }
}