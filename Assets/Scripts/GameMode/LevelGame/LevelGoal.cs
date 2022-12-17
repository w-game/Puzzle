using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GameMode.LevelGame
{
    public abstract class LevelGoal
    {
        public abstract string ElementPath { get; }
        public Dictionary<Color, int> GoalCount { get; } = new();
        public Dictionary<Color, int> CurCount { get; } = new();
        public bool IsComplete { get; protected set; }

        public void Init(LevelGoalConfig config)
        {
            if (config.counts.Count > PuzzleGame.BlockColors.Count)
            {
                SLog.D("Level Goal", "目标数配置大于方块数，请检查配置");
                return;
            }
            
            foreach (var count in config.counts)
            {
                var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                while (GoalCount.ContainsKey(color))
                {
                    color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                }

                GoalCount.Add(color, count);
                CurCount.Add(color, 0);
            }
        }

        protected abstract void IncreaseCount(RemoveUnit unit);

        protected void IncreaseCount(Color color, int count)
        {
            if (CurCount.ContainsKey(color))
            {
                var newValue = CurCount[color] + count;
                CurCount[color] = newValue > GoalCount[color] ? GoalCount[color] : newValue;
            }
        }
        
        public int GetRemainCount(Color color)
        {
            return GoalCount[color] - CurCount[color];
        }

        public bool CheckStatus(RemoveUnit unit)
        {
            IncreaseCount(unit);
            
            foreach (var key in GoalCount.Keys)
            {
                if (CurCount[key] < GoalCount[key])
                {
                    return false;
                }
            }

            Complete();
            return true;
        }

        private void Complete()
        {
            IsComplete = true;
            OnComplete();
        }

        protected virtual void OnComplete() { }
    }
}