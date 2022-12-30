using System;
using System.Collections.Generic;
using Common.GameMode;
using GameMode.LevelGame;
using UI.View;
using Random = UnityEngine.Random;

namespace GameMode.EndlessGame
{
    public class Challenge : GameEvent
    {
        public List<LevelGoal> Goals { get; } = new();
        public event Action<bool> OnComplete;

        private int _roundCount;
        private int RoundCount
        {
            get => _roundCount;
            set
            {
                _roundCount = value;
                EventCenter.Invoke(GameView.EventKeys.RefreshRoundCount, _roundCount);
            }
        }

        private Reward Reward { get; }
        private Punishment Punishment { get; }
        
        public bool Complete { get; private set; }
        private bool _isPass;

        public Challenge(List<LevelConfig> configs)
        {
            var config = configs[Random.Range(0, configs.Count)];
            PuzzleGame.Cur.GenerateSpecialBlocks(config.blocks, ClearSlotType.NotNormal);
            InitGoal(config);
            RoundCount = config.roundCount;

            Reward = ChallengeFactory.Instance.RandomReward();
            Punishment = ChallengeFactory.Instance.RandomPunishment();
        }

        private void InitGoal(LevelConfig config)
        {
            try
            {
                foreach (var goalConfig in config.goal)
                {
                    var color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    while (Goals.Find(_ => _.Pattern == color) != null)
                    {
                        color = PuzzleGame.BlockColors[Random.Range(0, PuzzleGame.BlockColors.Count)];
                    }
                    
                    var goal = LevelGoalFactory.Instance.GetGoal(goalConfig.type);
                    goal.Init(goalConfig, color);
                    goal.GameEvent = this;
                    Goals.Add(goal);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CheckGoal(RemoveUnit unit)
        {
            if (Complete) return;
            
            var result = true;
            foreach (var goal in Goals)
            {
                if (!goal.CheckStatus(unit))
                {
                    result = false;
                }
            }

            if (result)
            {
                _isPass = true;
                Complete = true;
            }
        }

        public void OnRoundEnd()
        {
            if (RoundCount <= 0)
            {
                _isPass = false;
                Complete = true;
            }
        }

        public void CheckResult()
        {
            if (!Complete) return;

            ChallengeResult result;
            if (_isPass)
            {
                Reward?.Execute();
                result = Reward;
            }
            else
            {
                Punishment?.Execute();
                result = Punishment;
            }
            EventCenter.Invoke(EndlessGameView.EndlessEventKeys.EndChallenge);
            EventCenter.Invoke(EndlessGameView.EndlessEventKeys.ShowChallengeResult, result);
            OnComplete?.Invoke(_isPass);
            ClearChallengeBlocks();
        }

        private void ClearChallengeBlocks()
        {
            PuzzleGame.Cur.ClearSlots(ClearSlotType.NotNormal, true);
            PuzzleGame.Cur.StartCheck(true);
        }

        public void End()
        {
            OnComplete?.Invoke(false);
            EventCenter.Invoke(EndlessGameView.EndlessEventKeys.EndChallenge);
        }

        public void ExecuteRound()
        {
            RoundCount--;
        }
    }
}