using Common;
using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopLevelGameOverData : ViewData
    {
        public override string ViewName => "PopLevelGameOver";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }
    
    public class PopLevelGameOver : PopupBase
    {
        [SerializeField] private Button restartBtn;
        [SerializeField] private Button backBtn;

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI des;
        [SerializeField] private TextMeshProUGUI restartBtnTxt;
        [SerializeField] private TextMeshProUGUI backToHomeBtnTxt;
        public override void OnCreate(params object[] objects)
        {
            restartBtn.onClick.AddListener(Restart);
            backBtn.onClick.AddListener(BackToHome);
            
            ShowNativeAd();
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.LevelFailTitle;
            des.text = language.LevelFailDes;
            restartBtnTxt.text = language.Restart;
            backToHomeBtnTxt.text = language.BackToHomeText;
        }

        private void Restart()
        {
            GameManager.Instance.CheckPower(PuzzleGame.PowerCost.Level, () =>
            {
                UIManager.Instance.DecreasePower(restartBtn.transform, () =>
                {
                    PuzzleGame.Cur.StartGame();
                    CloseView();
                });
            });
        }

        private void BackToHome()
        {
            CloseView();
            UIManager.Instance.PopMain<LevelGameViewData>();
        }
    }
}