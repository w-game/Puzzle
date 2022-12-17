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

        public override void OnCreate(params object[] objects)
        {
            scoreTxt.text = $"{objects[0]}";
            nextLevelBtn.onClick.AddListener(NextLevel);
            homeBtn.onClick.AddListener(BackToHome);
        }

        private void NextLevel()
        {
            CloseView();
            var levelGameMode = GameManager.Instance.GameMode as LevelGameMode;
            levelGameMode.NextLevel();
        }

        private void BackToHome()
        {
            CloseView();
            UIManager.Instance.PopMain<LevelGameViewData>();
        }
    }
}