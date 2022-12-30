using Blocks;

namespace GameMode.EndlessGame
{
    public class GenerateNewRowAtBottom : Punishment
    {
        public override string Tip => GameManager.Language.GenerateNewRowAtBottomPunishmentTip;

        public override void Execute()
        {
            for (int i = 0; i < PuzzleGame.BoardWidth; i++)
            {
                var slot = PuzzleGame.Cur.BlockSlots[(PuzzleGame.BoardLength - 1) * PuzzleGame.BoardWidth + i];
                if (slot.SubBlock)
                {
                    PuzzleGame.Cur.MoveUp(slot);
                }

                if (!slot.SubBlock)
                {
                    slot.GenerateBlock(typeof(NormalBlock), PuzzleGame.RandomColor);
                }
            }

            PuzzleGame.Cur.StartCheck(false);
        }
    }
}