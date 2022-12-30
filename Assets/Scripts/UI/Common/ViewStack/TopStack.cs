using Ad;
using Common;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class TopStack : ViewStack
    {
        [SerializeField] private CanvasGroup splash;
        [SerializeField] private AddBlockTip addBlockTip;
        [FormerlySerializedAs("toast")] [SerializeField] private ToastElement toastElement;

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
            AdManager.Instance.Init();
        }

        public void ShowToast(ToastType type, string msg)
        {
            toastElement.ShowToast(new SToast
            {
                Type = type,
                msg = msg
            });
        }
    }
}