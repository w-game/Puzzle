using Ad;
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

        public void CheckClose()
        {
            if (GameManager.User.PrivacyPolicy)
            {
                UIManager.Instance.PushTop<PopPrivacyPolicyData>();
            }
            else
            {
                AdManager.Instance.Init();
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