using Ad;
using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopGameToolNotEnoughData : ViewData
    {
        public override string ViewName => "PopGameToolNotEnough";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }

    public class PopGameToolNotEnough : PopupBase
    {
        [SerializeField] private Button getToolBtn;
        [SerializeField] private Button cancelBtn;
        [SerializeField] private TextMeshProUGUI des;

        [Header("本地化")] 
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI getToolBtnTxt;
        [SerializeField] private TextMeshProUGUI cancelBtnTxt;
        
        private string _toolId;
        public override void OnCreate(params object[] objects)
        {
            _toolId = objects[0] as string;
            des.text = string.Format(GameManager.Language.ToolNotEnoughDes, objects[1]);
            getToolBtn.onClick.AddListener(GetTool);
            cancelBtn.onClick.AddListener(CloseView);
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.ToolNotEnoughTitle;
            getToolBtnTxt.text = language.GetToolBtnText;
            cancelBtnTxt.text = language.CancelText;
        }

        private void GetTool()
        {
            AdManager.Instance.RewardAd.ShowAd(result =>
            {
                if (result)
                {
                    var count = PlayerPrefs.GetInt(_toolId, 3);
                    count++;
                    PlayerPrefs.SetInt(_toolId, count);
                    EventCenter.Invoke(LevelGameView.LevelEventKeys.RefreshTool);
                    CloseView();
                }
            });
        }
    }
}