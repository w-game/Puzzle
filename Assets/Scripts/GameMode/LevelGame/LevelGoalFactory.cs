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
            _goals.Add("SingleColor", new SingleColorRemoveGoal().GetType());
            _goals.Add("Horizontal", new HorizontalRemoveGoal().GetType());
            _goals.Add("ColorHorizontal", new ColorHorizontalRemoveGoal().GetType());
            _goals.Add("Vertical", new VerticalRemoveGoal().GetType());
            _goals.Add("ColorVertical", new ColorVerticalRemoveGoal().GetType());
            _goals.Add("StaticBlock", new StaticBlockRemoveGoal().GetType());
            _goals.Add("ShadowBlock", new ShadowBlockRemoveGoal().GetType());
            _goals.Add("FiveBlock", new FiveBlockRemoveGoal().GetType());
            _goals.Add("ColorFiveBlock", new ColorFiveBlockRemoveGoal().GetType());
            _goals.Add("StopIceBlock", new StopIceBlockRemoveGoal().GetType());
            _goals.Add("CountIceBlock", new CountIceBlockRemoveGoal().GetType());
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