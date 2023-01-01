using System.Collections.Generic;
using System.Linq;
using Common;
using UnityEngine;

namespace GameMode.EndlessGame
{
    public class ChallengeFactory : SSingleton<ChallengeFactory>
    {
        private Dictionary<Reward, float> Rewards { get; } = new();
        private Dictionary<Punishment, float> Punishments { get; } = new();

        private float _totalRewardWeight;
        private float _totalPunishmentWeight;
        public ChallengeFactory()
        {
            InitRewards();
            InitPunishments();
        }

        private void InitRewards()
        {
            Rewards.Add(new ClearSingleColorAllBlocks(), 1f);
            Rewards.Add(new ClearBottomRowAllBlocks(), 1f);
            Rewards.Add(new PowerReward(), 0.1f);
            foreach (var weight in Rewards.Values)
            {
                _totalRewardWeight += weight;
            }
        }

        private void InitPunishments()
        {
            // Punishments.Add(new GenerateNewRowAtBottom(), 1f);
            Punishments.Add(new GenerateRandomNormalBlockAtTop(), 1f);
            foreach (var weight in Punishments.Values)
            {
                _totalPunishmentWeight += weight;
            }
        }

        public Reward RandomReward()
        {
            var minWeight = Random.Range(0, _totalRewardWeight);
            var weight = 0f;
            foreach (var reward in Rewards.Keys)
            {
                weight += Rewards[reward];
                if (weight >= minWeight)
                {
                    return reward;
                }
            }

            return Rewards.Keys.First();
        }

        public Punishment RandomPunishment()
        {
            var minWeight = Random.Range(0, _totalPunishmentWeight);
            var weight = 0f;
            foreach (var punishment in Punishments.Keys)
            {
                weight += Punishments[punishment];
                if (weight >= minWeight)
                {
                    return punishment;
                }
            }

            return Punishments.Keys.First();
        }
    }
}