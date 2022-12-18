using System.Collections.Generic;
using Common;
using System;
using Random = UnityEngine.Random;

namespace GameMode.LevelGame
{
    public class GameLevel
    {
        public int LevelIndex { get; set; }
        public List<LevelGoal> Goals { get; } = new();
        
        public void Init(LevelConfig config)
        {
            try
            {
                if (config.goal.Count > PuzzleGame.BlockColors.Count)
                {
                    SLog.D("Level Goal", "目标数配置大于方块数，请检查配置");
                    return;
                }
                
                foreach (var goalConfig in config.goal)
                {
                    var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    while (Goals.Find(_ => _.Pattern == color) != null)
                    {
                        color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    }
                    
                    var type = Type.GetType("GameMode.LevelGame." + $"{goalConfig.type}Goal");
                    var goal = Activator.CreateInstance(type) as LevelGoal;
                    goal.Init(goalConfig, color);
                    Goals.Add(goal);
                }
            }
            catch (Exception e)
            {
                var s = $"关卡创建失败！ {e}";
                SLog.D("Game Level", s);
            }
        }

        public bool CheckLevelGoal(RemoveUnit unit)
        {
            var result = true;
            foreach (var goal in Goals)
            {
                goal.CheckStatus(unit);

                if (!goal.IsComplete)
                {
                    result = false;
                }
            }

            return result;
        }
    }
}