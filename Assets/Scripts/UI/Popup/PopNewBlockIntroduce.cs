using GameMode.LevelGame;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopNewBlockIntroduceData : ViewData
    {
        public override string ViewName => "PopNewBlockIntroduce";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }
    
    public class PopNewBlockIntroduce : PopupBase
    {
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI blockName;
        [SerializeField] private TextMeshProUGUI des;
        [SerializeField] private Button btn;
        
        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI sureBtnTxt;

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.NewSpecialBlockTitle;
            sureBtnTxt.text = language.SureText;
        }

        public override void OnCreate(params object[] objects)
        {
            var config = objects[0] as LevelNewBlockConfig;
            var infoConfig = config.info[GameManager.Language.GetType().Name];

            blockName.text = infoConfig.blockName;
            des.text = infoConfig.des;

            if (!string.IsNullOrEmpty(config.path))
            {
                icon.SetImage(config.path);
            }

            ColorUtility.TryParseHtmlString(config.color, out var color);
            icon.color = color;

            btn.onClick.AddListener(CloseView);
        }
    }
}