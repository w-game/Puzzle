using System.Collections.Generic;
using Common;
using UnityEngine;

namespace GameSystem
{
    public class ChallengeSystem
    {
        private List<Challenge> _challenges = new();

        private Challenge _curChallenge;
        public ChallengeSystem()
        {
            InitChallenge(new RemoveCountChallenge());
            
            EventCenter.AddFromNonUI("OnChallengeComplete", OnChallengeComplete);
        }

        private void InitChallenge(Challenge challenge)
        {
            challenge.Init();
            _challenges.Add(challenge);
        }
        
        public void CheckStartChallenge()
        {
            if (_curChallenge != null)
            {
                SLog.D("Challenge System", "已经存在一个挑战，无法再次生成挑战");
                return;
            }

            var index = Random.Range(0, _challenges.Count);
            _curChallenge = _challenges[index];
            _curChallenge.Refresh();
        }

        public void CheckProcess(RemoveUnit removeUnit)
        {
            _curChallenge?.CheckProcess(removeUnit);
        }

        private void OnChallengeComplete()
        {
            _curChallenge.Complete();
            _curChallenge = null;
        }
    }
}