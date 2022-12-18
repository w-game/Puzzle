using TMPro;
using UnityEngine;

namespace UI.View
{
    public class LevelGameViewData : ViewData
    {
        public override string ViewName => "LevelGameView";
        public override ViewType ViewType => ViewType.View;
        public override bool Mask => false;
    }
    
    public class LevelGameView : GameView
    {
        [SerializeField] private LevelGoalElement levelGoalElement;
        [SerializeField] private TextMeshProUGUI levelTxt;

        private LevelGameMode _levelGameMode;
        public override void OnCreate(params object[] objects)
        {
            base.OnCreate(objects);
            _levelGameMode = puzzleGame as LevelGameMode;
            
            AddEvent("SetGoal", SetGoal);
            AddEvent("RefreshGoal", RefreshGoal);
        }

        private void SetGoal()
        {
            levelGoalElement.SetGoal(_levelGameMode.CurLevel.Goals);
            levelTxt.text = $"第{_levelGameMode.CurLevel.LevelIndex + 1}关";
        }

        private void RefreshGoal()
        {
            levelGoalElement.RefreshGoal();
        }
    }
}