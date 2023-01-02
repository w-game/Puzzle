using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class Loading : ElementBase
    {
        private void Awake()
        {
            Play();
        }

        public void Play()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DORotate(new Vector3(0, 0, -120), 0.5f).SetEase(Ease.Linear));
            sequence.SetLoops(-1, LoopType.Incremental);
        }
    }
}