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

        public override void OnCreate(params object[] objects)
        {
            restartBtn.onClick.AddListener(Restart);
            backBtn.onClick.AddListener(BackToHome);
        }

        private void Restart()
        {
            GameManager.Instance.GameMode.Restart();
        }

        private void BackToHome()
        {
            CloseView();
            UIManager.Instance.PopMain<LevelGameViewData>();
        }
    }
}