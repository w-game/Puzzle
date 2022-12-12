using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Common
{
    public class SToast : MonoBehaviour
    {
        [SerializeField] private Transform content;
        [SerializeField] private TextMeshProUGUI text;

        public void ShowToast(string msg)
        {
            text.text = msg;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(content.DOScale(1, 0.2f));
            sequence.AppendInterval(3f);
            sequence.Append(content.DOScale(0, 0.2f));
        }
    }
}