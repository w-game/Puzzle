namespace GameMode.LevelGame
{
    public class ColorHorizontalRemoveGoal : LevelGoal
    {
        public override string ElementPath => "HorizontalRemoveGoal";
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.RemoveType == RemoveType.Horizontal)
            {
                IncreaseCount(unit.BlockIndex, 1);
            }
        }
    }
}