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
        // public float MaxTime { get; private set; }
        public int BlockCount { get; private set; }
        public Dictionary<string, string> Boards { get; private set; }
        public int RoundCount { get; set; }
        public bool IsPass { get; private set; }

        private LevelConfig _config;
        public void Init(LevelConfig config)
        {
            _config = config;
            // MaxTime = config.time;
            Boards = config.blocks;
            BlockCount = config.blockCount;
            RoundCount = config.roundCount;
        }

        public void InitGoals()
        {
            try
            {
                if (_config.goal.Count > PuzzleGame.BlockColors.Count)
                {
                    SLog.D("Level Goal", "目标数配置大于方块数，请检查配置");
                    return;
                }
                
                foreach (var goalConfig in _config.goal)
                {
                    var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    while (Goals.Find(_ => _.Pattern == color) != null)
                    {
                        color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    }

                    var goal = LevelGoalFactory.Instance.GetGoal(goalConfig.type);
                    goal.Init(goalConfig, color);
                    Goals.Add(goal);
                }
            }
            catch (Exception e)
            {
                SLog.D("Game Level", $"关卡目标初始化失败！ \n{e}");
            }
        }

        public bool CheckLevelGoal(RemoveUnit unit)
        {
            if (IsPass) return false;
            var result = true;
            foreach (var goal in Goals)
            {
                goal.CheckStatus(unit);

                if (!goal.IsComplete)
                {
                    result = false;
                }
            }

            if (result) IsPass = true;

            return result;
        }

        public bool CheckLevelRoundCount()
        {
            if (RoundCount <= 0)
            {
                return false;
            }

            return true;
        }
    }
}