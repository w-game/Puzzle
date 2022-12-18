using TMPro;
using UI.Popup;
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
        public enum EventKeys
        {
            SetGoal,
            RefreshGoal,
            OnLevelPass,
            OnGameOver
        }
        
        [SerializeField] private LevelGoalElement levelGoalElement;
        [SerializeField] private TextMeshProUGUI levelTxt;
        [SerializeField] private LevelTime levelTime;

        private LevelGameMode _levelGameMode;
        public override void OnCreate(params object[] objects)
        {
            base.OnCreate(objects);
            _levelGameMode = puzzleGame as LevelGameMode;
            
            AddEvent(EventKeys.SetGoal, SetGoal);
            AddEvent(EventKeys.RefreshGoal, RefreshGoal);
            AddEvent<int>(EventKeys.OnLevelPass, OnLevelPass);
            AddEvent(EventKeys.OnGameOver, OnGameOver);
        }

        private void SetGoal()
        {
            levelGoalElement.SetGoal(_levelGameMode.CurLevel.Goals);
            levelTxt.text = $"第{_levelGameMode.CurLevel.LevelIndex + 1}关";
            levelTime.SetTime(_levelGameMode.CurLevel.MaxTime, OnGameOver);
        }
        
        private void RefreshGoal()
        {
            levelGoalElement.RefreshGoal();
        }

        private void OnLevelPass(int score)
        {
            levelTime.Stop();
            UIManager.Instance.PushPop<PopPassLevelData>(score);
        }

        private void OnGameOver()
        {
            levelTime.Stop();
            UIManager.Instance.PushPop<PopLevelGameOverData>();
            _levelGameMode.LevelFail();
        }
    }
}