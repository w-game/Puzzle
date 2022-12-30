namespace GameMode.EndlessGame
{
    public class ClearBottomRowAllBlocks : Reward
    {
        public override string Tip => GameManager.Language.ClearBottomRowAllBlocksRewardTip;
        public override void Execute()
        {
            for (int i = 0; i < PuzzleGame.BoardWidth; i++)
            {
                var slot = PuzzleGame.Cur.BlockSlots[(PuzzleGame.BoardLength - 1) * PuzzleGame.BoardWidth + i];
                slot.RemoveMainBlock();
            }

            PuzzleGame.Cur.StartCheck(true);
        }
    }
}