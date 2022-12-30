using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Common
{
    public enum ToastType
    {
        Info,
        Warning,
        Error
    }
    public class SToast
    {
        public ToastType Type;
        public string msg;
    }
    public class ToastElement : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI type;
        [SerializeField] private TextMeshProUGUI text;

        private List<SToast> _queue = new();

        private Vector3 _originPos;
        private void Awake()
        {
            _originPos = transform.position;
        }

        public void ShowToast(SToast toast)
        {
            if (_queue.Count == 0)
            {
                _queue.Add(toast);
                DoShow();
            }
            else
            {
                _queue.Add(toast);
            }
        }

        private void DoShow()
        {
            var toast = _queue[0];
            switch (toast.Type)
            {
                case ToastType.Info:
                    type.text = GameManager.Language.ToastInfoType;
                    type.color = new Color(91f / 255, 171f / 255, 60f / 255);
                    break;
                case ToastType.Warning:
                    type.text = GameManager.Language.ToastWarningType;
                    type.color = new Color(247f / 255, 217f / 255, 103f / 255);
                    break;
                case ToastType.Error:
                    type.text = GameManager.Language.ToastErrorType;
                    type.color = new Color(194f / 255, 67f / 255, 71f / 255);
                    break;
            }
            text.text = toast.msg;
            
            Sequence sequence = DOTween.Sequence();
            sequence.Append(transform.DOMove(_originPos + Vector3.down * 120f, 0.5f));
            sequence.Join(canvasGroup.DOFade(1, 0.5f));
            sequence.AppendInterval(3f);
            sequence.Append(transform.DOMove(_originPos, 0.5f));
            sequence.Join(canvasGroup.DOFade(0, 0.5f));
            sequence.AppendCallback(() =>
            {
                _queue.RemoveAt(0);
                if (_queue.Count != 0)
                {
                    DoShow();
                }
            });
        }
    }
}