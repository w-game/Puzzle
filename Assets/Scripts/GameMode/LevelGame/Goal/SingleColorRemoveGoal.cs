namespace GameMode.LevelGame
{
    public class SingleColorRemoveGoal : LevelGoal
    {
        public override string ElementPath => "SingleColorRemoveGoal";

        protected override void IncreaseCount(RemoveUnit unit)
        {
            IncreaseCount(unit.BlockIndex, unit.BlockCount);
        }
    }
}