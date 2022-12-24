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
        public override void OnCreate(params object[] objects)
        {
            var config = objects[0] as LevelNewBlockConfig;

            blockName.text = config.blockName;
            des.text = config.des;

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