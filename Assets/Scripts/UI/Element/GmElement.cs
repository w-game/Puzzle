using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GmElement : ElementBase
    {
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private Button sureBtn;
        public void Init(Action<string> onSure)
        {
            sureBtn.onClick.AddListener(() => onSure?.Invoke(inputField.text));
        }
    }
}