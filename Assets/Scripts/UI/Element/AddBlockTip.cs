using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AddBlockTip : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private Image block;

        private Vector2 _originPos;
        private void Awake()
        {
            _originPos = content.localPosition;
        }

        public void ShowTip(Color color)
        {
            block.color = color;
            content.DOLocalMove(Vector3.zero, 0.2f);

            var sequence = DOTween.Sequence();
            sequence.AppendInterval(2f);
            sequence.AppendCallback(() =>
            {
                content.DOLocalMove(_originPos, 0.2f);
            });
        }
    }
}