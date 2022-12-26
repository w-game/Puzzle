using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopGMData : ViewData
    {
        public override string ViewName => "PopGM";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
    }

    public class PopGM : PopupBase
    {
        [SerializeField] private GmElement level;
        [SerializeField] private GmElement power;
        [SerializeField] private Button doneBtn;
        public override void OnCreate(params object[] objects)
        {
            level.Init(data =>
            {
                if (int.TryParse(data, out var d))
                {
                    GameManager.User.GameLevel = d - 1;
                }
            });
            power.Init(data =>
            {
                if (int.TryParse(data, out var d))
                {
                    GameManager.User.IncreasePower(d);
                }
            });
            doneBtn.onClick.AddListener(CloseView);
        }
    }
}