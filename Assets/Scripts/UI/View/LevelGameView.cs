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
            levelGoalElement.SetGoal(_levelGameMode.CurLevel.Goal);
        }

        private void RefreshGoal()
        {
            levelGoalElement.RefreshGoal();
        }
    }
}