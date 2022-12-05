using UnityEngine;

namespace GameSystem
{
    public class RemoveCountChallenge : Challenge
    {
        public Color RemoveIndex { get; set; }
        
        protected override void OnRefresh()
        {
            MaxCount = Random.Range(0, 20);
        }

        public override void CheckProcess(RemoveUnit removeUnit)
        {
            if (removeUnit.BlockIndex != RemoveIndex) return;

            CurCount += removeUnit.Slots.Count;
            if (CurCount > MaxCount) CurCount = MaxCount;

            if (CurCount == MaxCount)
            {
                EventCenter.Invoke("OnChallengeComplete");
            }
        }

        protected override void OnComplete()
        {
            
        }
    }
}