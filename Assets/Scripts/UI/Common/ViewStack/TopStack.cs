using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class TopStack : ViewStack
    {
        [SerializeField] private CanvasGroup splash;

        public void CloseSplash()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1.5f);
            sequence.Append(splash.DOFade(0, 0.2f));
        }
    }
}