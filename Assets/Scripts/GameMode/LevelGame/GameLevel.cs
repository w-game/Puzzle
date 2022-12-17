using System;
using Common;

namespace GameMode.LevelGame
{
    public class GameLevel
    {
        public int LevelIndex { get; set; }
        public LevelGoal Goal { get; set; }
        
        public void Init(LevelConfig config)
        {
            try
            {
                var type = Type.GetType("GameMode.LevelGame." + $"{config.goal.type}Goal");
                Goal = Activator.CreateInstance(type) as LevelGoal;
                Goal.Init(config.goal);
            }
            catch (Exception e)
            {
                var s = $"关卡创建失败！ {e}";
                SLog.D("Game Level", s);
            }
        }

        public bool CheckLevelGoal(RemoveUnit unit)
        {
            if (Goal.IsComplete) return false;
            return Goal.CheckStatus(unit);
        }
    }
}