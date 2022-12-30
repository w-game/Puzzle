using System;
using TMPro;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

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
        public enum LevelEventKeys
        {
            SetGoal,
            OnLevelPass,
            OnGameOver,
            RefreshTool
        }
        
        [SerializeField] private TextMeshProUGUI levelTxt;
        [SerializeField] private LevelTime levelTime;
        [SerializeField] private Button clearControlPanelBtn;
        [SerializeField] private TextMeshProUGUI clearControlPanelCount;
        [SerializeField] private Button clearSlotsBtn;
        [SerializeField] private TextMeshProUGUI clearSlotsCount;

        [Header("本地化")]
        [SerializeField] private TextMeshProUGUI clearAllSlotsPropTxt;
        [SerializeField] private TextMeshProUGUI refreshControlBarTxt;
        
        private LevelGameMode _levelGameMode;
        public override void OnCreate(params object[] objects)
        {
            _levelGameMode = puzzleGame as LevelGameMode;
            
            clearControlPanelBtn.onClick.AddListener(ClearControlPanel);
            clearSlotsBtn.onClick.AddListener(ClearSlots);
            
            RefreshTool();
            
            AddEvent(LevelEventKeys.SetGoal, SetGoal);
            AddEvent<int>(LevelEventKeys.OnLevelPass, OnLevelPass);
            AddEvent(LevelEventKeys.OnGameOver, OnGameOver);
            AddEvent(LevelEventKeys.RefreshTool, RefreshTool);
            
            base.OnCreate(objects);
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            clearAllSlotsPropTxt.text = language.ToolClearSlotsName;
            refreshControlBarTxt.text = language.ToolClearControlName;
        }

        private void ClearControlPanel()
        {
            _levelGameMode.CheckRefreshControl();
        }
        
        private void ClearSlots()
        {
            _levelGameMode.CheckClearSlots();
        }

        private void RefreshTool()
        {
            clearSlotsCount.text = $"{GameManager.User.ClearSlotsCount}";
            clearControlPanelCount.text = $"{GameManager.User.ClearControlPanelCount}";
        }

        private void SetGoal()
        {
            goalElement.SetGoal(_levelGameMode.CurLevel.Goals);
            levelTxt.text = String.Format(GameManager.Language.Level, _levelGameMode.CurLevel.LevelIndex + 1);
            // levelTime.SetTime(_levelGameMode.CurLevel.MaxTime, OnGameOver);
        }

        private void OnLevelPass(int score)
        {
            // levelTime.Stop();
            UIManager.Instance.PushPop<PopPassLevelData>(score);
        }

        private void OnGameOver()
        {
            // levelTime.Stop();
            UIManager.Instance.PushPop<PopLevelGameOverData>();
        }
    }
}