namespace GameMode.LevelGame
{
    public class ColorFiveBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "FiveBlockRemoveGoal";
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.BlockCount == 5)
            {
                IncreaseCount(unit.BlockIndex, 1);
            }
        }
    }
}