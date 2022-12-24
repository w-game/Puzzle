using System;
using TMPro;
using UI;
using UI.Popup;
using UI.View;
using UnityEngine;
using UnityEngine.UI;

public class HomeViewData : ViewData
{
    public override string ViewName => "HomeView";

    public override ViewType ViewType => ViewType.View;

    public override bool Mask => true;
}

public class HomeView : ViewBase
{
    public enum EventKeys
    {
        RefreshView
    }
    
    [SerializeField] private Button levelStartGameBtn;
    [SerializeField] private Button unlimitedStartGameBtn;
    [SerializeField] private Button guideBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private TextMeshProUGUI allRemove;
    public override void OnCreate(params object[] objects)
    {
        levelStartGameBtn.onClick.AddListener(() => StartGame(PuzzleGameMode.Level));
        unlimitedStartGameBtn.onClick.AddListener(() => StartGame(PuzzleGameMode.Unlimited));
        guideBtn.onClick.AddListener(OnGuideBtnClick);
        settingBtn.onClick.AddListener(OnSettingBtnClick);
        
        RefreshView();
        AddEvent(EventKeys.RefreshView, RefreshView);
        UIManager.Instance.CheckCloseSplash();
    }

    private void StartGame(PuzzleGameMode gameMode)
    {
        switch (gameMode)
        {
            case PuzzleGameMode.Level:
                StartGame<LevelGameViewData>(levelStartGameBtn.transform);
                break;
            case PuzzleGameMode.Unlimited:
                StartGame<UnlimitedGameViewData>(unlimitedStartGameBtn.transform);
                break;
        }
    }

    private void StartGame<T>(Transform trans) where T : ViewData, new()
    {
        GameManager.Instance.CheckPower(PuzzleGame.PowerCost.Level, () =>
        {
            UIManager.Instance.DecreasePower(trans, () =>
            {
                UIManager.Instance.PushMain<T>();
            });
        });
    }

    private void OnGuideBtnClick()
    {
        UIManager.Instance.PushPop<PopNewPlayerGuideData>();
    }

    private void OnSettingBtnClick()
    {
        UIManager.Instance.PushPop<PopSettingData>();
    }

    private void RefreshView()
    {
        maxScore.text = GameManager.User.MaxScore.ToString();
        allRemove.text = $"共完成过{GameManager.User.AllRemoveCount}次全部消除";
    }
}