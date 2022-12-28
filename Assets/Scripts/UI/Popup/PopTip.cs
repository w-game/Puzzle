using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopTipData : ViewData
    {
        public override string ViewName => "PopTip";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }
    
    public class PopTip : PopupBase
    {
        [SerializeField] private TextMeshProUGUI des;
        [SerializeField] private Button sureBtn;
        
        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [SerializeField] private TextMeshProUGUI sureBtnTxt;
        public override void OnCreate(params object[] objects)
        {
            des.text = $"{objects[0]}";
            sureBtn.onClick.AddListener(objects[1] as UnityAction);
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.TipTitle;
            sureBtnTxt.text = language.SureText;
        }
    }
}