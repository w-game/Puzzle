using System;
using System.Collections.Generic;
using Common;
namespace GameMode.LevelGame
{
    public class LevelGoalFactory : Singleton<LevelGoalFactory>
    {
        private Dictionary<string, Type> _goals = new();
        public LevelGoalFactory()
        {
            InitGoalInstances();
        }

        private void InitGoalInstances()
        {
            _goals.Add("SingleColor", typeof(SingleColorRemoveGoal));
            _goals.Add("Horizontal", typeof(HorizontalRemoveGoal));
            _goals.Add("ColorHorizontal", typeof(ColorHorizontalRemoveGoal));
            _goals.Add("Vertical", typeof(VerticalRemoveGoal));
            _goals.Add("ColorVertical", typeof(ColorVerticalRemoveGoal));
            _goals.Add("StaticBlock", typeof(StaticBlockRemoveGoal));
            _goals.Add("ShadowBlock", typeof(ShadowBlockRemoveGoal));
            _goals.Add("FiveBlock", typeof(FiveBlockRemoveGoal));
            _goals.Add("ColorFiveBlock", typeof(ColorFiveBlockRemoveGoal));
        }

        public LevelGoal GetGoal(string goalName)
        {
            _goals.TryGetValue(goalName, out var goalType);
            if (goalType == null)
            {
                SLog.D("Level Goal Factory", $"未找到目标类型！[{goalName}]");
                return null;
            }
            
            return Activator.CreateInstance(goalType) as LevelGoal;
        }
    }
}