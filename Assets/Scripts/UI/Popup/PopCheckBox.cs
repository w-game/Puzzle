using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopCheckBoxData : ViewData
    {
        public override string ViewName => "PopCheckBox";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }

    public class PopCheckBox : PopupBase
    {
        [SerializeField] private TextMeshProUGUI des;
        [SerializeField] private Button sureBtn;
        [SerializeField] private Button cancelBtn;
        public override void OnCreate(params object[] objects)
        {
            des.text = $"{objects[0]}";
            UnityAction sureAction = objects[1] as UnityAction;
            UnityAction cancelAction = objects[2] as UnityAction;
            
            sureBtn.onClick.AddListener(() =>
            {
                sureAction?.Invoke();
                CloseView();
            });
            if (cancelAction != null)
            {
                cancelBtn.onClick.AddListener(() =>
                {
                    cancelAction();
                    CloseView();
                });
            }
            else
            {
                cancelBtn.onClick.AddListener(CloseView);
            }
        }
    }
}