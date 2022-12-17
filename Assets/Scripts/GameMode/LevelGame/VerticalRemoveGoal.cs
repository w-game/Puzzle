namespace GameMode.LevelGame
{
    public class VerticalRemoveGoal : LevelGoal
    {
        public override string ElementPath => "VerticalRemoveGoal";
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.RemoveType == RemoveType.Vertical)
            {
                IncreaseCount(unit.BlockIndex, 1);
            }
        }
    }
}