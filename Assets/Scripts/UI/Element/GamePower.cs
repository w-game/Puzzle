using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GamePower : ElementBase
    {
        public enum EventKeys
        {
            Show,
            DecreasePower,
            OnDecreasePowerEnd,
            Close
        }
        [SerializeField] private TextMeshProUGUI powerTxt;
        [SerializeField] private Transform moveUnit;

        private Vector3 _originPos;

        private Action _onDecreasePowerEnd;
        private void Awake()
        {
            AddEvent(EventKeys.Show, () => Show(null));
            AddEvent(EventKeys.Close, Close);
            AddEvent<Transform>(EventKeys.DecreasePower, DecreasePower);
            AddEvent<Action>(EventKeys.OnDecreasePowerEnd, SetOnDecreasePowerEnd);

            _originPos = transform.position;
            powerTxt.text = $"{GameManager.User.Power}";
        }

        private void DecreasePower(Transform trans)
        {
            powerTxt.text = $"{GameManager.User.Power}";
            Show(() =>
            {
                var sequence = DOTween.Sequence();
                sequence.Append(moveUnit.DOMove(trans.position, 0.6f));
                sequence.Join(moveUnit.DOScale(0.5f, 0.6f));
                sequence.Append(moveUnit.DOScale(0f, 0.2f));
                sequence.AppendCallback(() => _onDecreasePowerEnd?.Invoke());
                sequence.Append(transform.DOMove(_originPos, 0.2f));
                sequence.AppendCallback(() =>
                {
                    moveUnit.localPosition = Vector3.zero;
                    moveUnit.localScale = Vector3.one;
                });
            });
        }

        private void SetOnDecreasePowerEnd(Action callback)
        {
            _onDecreasePowerEnd = callback;
        }

        private void Show(TweenCallback callback)
        {
            powerTxt.text = $"{GameManager.User.Power}";
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(_originPos + Vector3.down * 300, 0.2f));
            sequence.AppendCallback(callback);
        }

        private void Close()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(_originPos, 0.2f));
        }
    }
}