using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class TopStack : ViewStack
    {
        [SerializeField] private CanvasGroup splash;
        [SerializeField] private AddBlockTip addBlockTip;

        public AddBlockTip AddBlockTip => addBlockTip;
        public void CloseSplash()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1f);
            sequence.Append(splash.DOFade(0, 0.2f));
            sequence.AppendCallback(() =>
            {
                splash.gameObject.SetActive(false);
            });
        }
    }
}