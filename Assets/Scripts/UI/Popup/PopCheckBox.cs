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
        
        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI sureBtnTxt;
        [SerializeField] private TextMeshProUGUI cancelBtnTxt;
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

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.CheckBoxTitle;
            sureBtnTxt.text = language.Confirm;
            cancelBtnTxt.text = language.CancelText;
        }
    }
}