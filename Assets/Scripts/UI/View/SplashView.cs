using System.Collections.Generic;
using Ad;
using Common;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI.View
{
    public class SplashView : ViewBase
    {
        [SerializeField] private TextMeshProUGUI version;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            version.text = $"Version {Application.version}";
            CheckClose();
        }

        private void CheckClose()
        {
            SEvent.TrackEvent("#new_user", new Dictionary<string, object>()
            {
                {"#new_user", GameManager.User.IsNewPlayer}
            });
            if (GameManager.User.IsNewPlayer)
            {
                GameManager.User.IsNewPlayer = false;
                DoClose();
            }
            else
            {
                AdManager.Instance.SplashAd.ShowAd(result =>
                {
                    DoClose();
                });
            }
        }

        private void DoClose()
        {
            var sequence = DOTween.Sequence();
            sequence.AppendInterval(1f);
            sequence.Append(canvasGroup.DOFade(0, 0.2f));
            sequence.AppendCallback(() =>
            {
                gameObject.SetActive(false);
                OnClose();
            });
        }

        public override void OnClose()
        {
            
        }
    }
}