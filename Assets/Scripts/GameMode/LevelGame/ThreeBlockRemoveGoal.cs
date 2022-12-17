namespace GameMode.LevelGame
{
    public class ThreeBlockRemoveGoal : LevelGoal
    {
        public override string ElementPath => "ThreeBlockRemoveGoal";
        protected override void IncreaseCount(RemoveUnit unit)
        {
            if (unit.BlockCount == 3)
            {
                IncreaseCount(unit.BlockIndex, 1);
            }
        }
    }
}