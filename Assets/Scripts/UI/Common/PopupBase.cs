using System;
using Ad;
using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class PopupBase : ViewBase
    {
        [SerializeField] protected Transform panel;

        private bool _adShowed;
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
            AdManager.Instance.NativeAd.ShowAd(null);
            _adShowed = true;
            // AdManager.Instance.BannerAd.ShowAd(null);
        }

        protected override void CloseView()
        {
            base.CloseView();
            if (_adShowed)
            {
                AdManager.Instance.NativeAd.CloseAd();
                _adShowed = false;
            }
        }
    }
}