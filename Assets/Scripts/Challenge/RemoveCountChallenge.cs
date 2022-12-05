using UnityEngine;

namespace GameSystem
{
    public class RemoveCountChallenge : Challenge
    {
        public int RemoveIndex { get; set; }
        
        protected override void OnRefresh()
        {
            MaxCount = Random.Range(0, 20);
            RemoveIndex = Random.Range(0, GameBoard.BlockLabels.Count);
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