using System;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI
{
    public class LanguageElement : ElementBase
    {
        [SerializeField] private TextMeshProUGUI languageName;
        [SerializeField] private SButton selectBtn;
        [SerializeField] private GameObject fill;

        public string LanguageName
        {
            get => languageName.text;
            set => languageName.text = value;
        }
        public ELanguage Type { get; private set; }
        private UnityAction _selectCallback;

        private void Awake()
        {
            UnSelect();
        }

        public void Init(ELanguage type, UnityAction selectCallback)
        {
            Type = type;
            _selectCallback = selectCallback;
            selectBtn.onClick.AddListener(() =>
            {
                GameManager.LanguageType = type;
                Select();
            });
        }

        public void Select()
        {
            fill.SetActive(true);
            _selectCallback?.Invoke();
        }

        public void UnSelect()
        {
            fill.SetActive(false);
        }
    }
}