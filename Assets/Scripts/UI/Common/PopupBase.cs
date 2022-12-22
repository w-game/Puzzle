using System;
using Ad;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PopupBase : ViewBase
    {
        [SerializeField] private Transform panel;
        public override void DoOpenAnima()
        {
            panel.localScale = Vector3.zero;
            panel.DOScale(1, 0.2f);
        }

        public override void DoCloseAnima(Action onComplete = null)
        {
            var anima = panel.DOScale(0, 0.2f);
            anima.onComplete += () => onComplete?.Invoke();
        }

        protected void ShowNativeAd()
        {
            // AdManager.Instance.NativeAd.ShowAd(null);
            AdManager.Instance.BannerAd.ShowAd(null);
        }
    }
}