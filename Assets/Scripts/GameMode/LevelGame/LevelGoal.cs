using UnityEngine;

namespace GameMode.LevelGame
{
    public abstract class LevelGoal
    {
        public abstract string ElementPath { get; }
        public string SpritePath { get; protected set; }
        public Color Pattern { get; protected set; }
        protected int GoalCount { get; set; }
        private int CurCount { get; set; }
        public int RemainCount => GoalCount - CurCount;
        public bool IsComplete { get; private set; }

        public virtual void Init(LevelGoalConfig config, Color color)
        {
            Pattern = color;
            GoalCount = config.count;
        }

        protected abstract void IncreaseCount(RemoveUnit unit);

        protected void IncreaseCount(Color color, int count)
        {
            if (Pattern == color)
            {
                var newValue = CurCount + count;
                CurCount = newValue > GoalCount ? GoalCount : newValue;
            }
        }

        public bool CheckStatus(RemoveUnit unit)
        {
            IncreaseCount(unit);
            
            if (CurCount < GoalCount) return false;

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