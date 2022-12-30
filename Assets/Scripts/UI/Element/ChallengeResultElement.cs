using System;
using DG.Tweening;
using GameMode.EndlessGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ChallengeResultElement : ElementBase
    {
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI tip;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private LayoutElement layoutElement;

        private RectTransform _rectTransform;

        private Sequence _sequence;
        private void Awake()
        {
            transform.localPosition = Vector3.zero;
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetData(ChallengeResult result)
        {
            if (result is Reward)
            {
                title.text = GameManager.Language.RewardLabel;
                title.color = new Color(91f / 255, 171f / 255, 60f / 255);
            } else if (result is Punishment)
            {
                title.text = GameManager.Language.PunishmentLabel;
                title.color = new Color(194f / 255, 67f / 255, 71f / 255);
            }
            tip.text = result.Tip;

            var txtTrans = tip.GetComponent<RectTransform>();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            if (txtTrans.rect.width > 600f)
            {
                layoutElement.preferredWidth = 600f;
            }

            _sequence = DOTween.Sequence();
            _sequence.Append(transform.DOLocalMove(Vector3.down * 100f, 0.5f));
            _sequence.Join(canvasGroup.DOFade(1, 0.5f));
            _sequence.AppendInterval(3f);
            _sequence.Append(canvasGroup.DOFade(0, 0.5f));
            _sequence.Join(transform.DOLocalMove(Vector3.zero, 0.5f));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}