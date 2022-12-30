using TMPro;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopPassLevelData : ViewData
    {
        public override string ViewName => "PopPassLevel";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }
    
    public class PopPassLevel : PopupBase
    {
        [SerializeField] private TextMeshProUGUI scoreTxt;
        [SerializeField] private Button nextLevelBtn;
        [SerializeField] private Button homeBtn;

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI nextLevelBtnTxt;
        [SerializeField] private TextMeshProUGUI backToHomeBtnTxt;

        public override void OnCreate(params object[] objects)
        {
            scoreTxt.text = $"{objects[0]}";
            nextLevelBtn.onClick.AddListener(NextLevel);
            homeBtn.onClick.AddListener(BackToHome);
            
            ShowNativeAd();
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.LevelSuccessTitle;
            nextLevelBtnTxt.text = language.NextLevelText;
            backToHomeBtnTxt.text = language.BackToHomeText;
        }

        private void NextLevel()
        {
            GameManager.Instance.CheckPower(PuzzleGame.PowerCost.Level, () =>
            {
                UIManager.Instance.DecreasePower(nextLevelBtn.transform, () =>
                {
                    var levelGameMode = PuzzleGame.Cur as LevelGameMode;
                    levelGameMode.InitLevel();
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