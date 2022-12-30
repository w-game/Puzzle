namespace GameMode.EndlessGame
{
    public class ClearSingleColorAllBlocks : Reward
    {
        public override string Tip => GameManager.Language.ClearSingleColorAllBlocksRewardTip;

        public override void Execute()
        {
            var color = PuzzleGame.RandomColor;
            foreach (var slot in PuzzleGame.Cur.BlockSlots)
            {
                if (slot.SubBlock && slot.SubBlock.Pattern == color)
                {
                    slot.RemoveMainBlock();
                }
            }

            PuzzleGame.Cur.StartCheck(false);
        }
    }
}