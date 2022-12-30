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
    [SerializeField] private Button languageBtn;
    [SerializeField] private Button guideBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private TextMeshProUGUI allRemove;
    
    [Header("本地化")]
    [SerializeField] private TextMeshProUGUI levelModeBtnTxt;
    [SerializeField] private TextMeshProUGUI unlimitedModeBtnTxt;
    public override void OnCreate(params object[] objects)
    {
        levelStartGameBtn.onClick.AddListener(() => StartGame(PuzzleGameMode.Level));
        unlimitedStartGameBtn.onClick.AddListener(() => StartGame(PuzzleGameMode.Unlimited));
        languageBtn.onClick.AddListener(() => UIManager.Instance.PushPop<PopLanguageData>());
        guideBtn.onClick.AddListener(OnGuideBtnClick);
        settingBtn.onClick.AddListener(OnSettingBtnClick);
        
        RefreshView();
        AddEvent(EventKeys.RefreshView, RefreshView);
        UIManager.Instance.CheckCloseSplash();
    }

    public override void Localization()
    {
        var language = GameManager.Language;
        levelModeBtnTxt.text = language.LevelModeStartBtn;
        unlimitedModeBtnTxt.text = language.UnlimitedModeStartBtn;
        allRemove.text = string.Format(GameManager.Language.AllRemoveTxt, GameManager.User.AllRemoveCount);
    }

    private void StartGame(PuzzleGameMode gameMode)
    {
        switch (gameMode)
        {
            case PuzzleGameMode.Level:
                StartGame<LevelGameViewData>(PuzzleGame.PowerCost.Level, levelStartGameBtn.transform);
                break;
            case PuzzleGameMode.Unlimited:
                StartGame<EndlessGameViewData>(PuzzleGame.PowerCost.Endless, unlimitedStartGameBtn.transform);
                break;
        }
    }

    private void StartGame<T>(int power, Transform trans) where T : ViewData, new()
    {
        GameManager.Instance.CheckPower(power, () =>
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
        allRemove.text = string.Format(GameManager.Language.AllRemoveTxt, GameManager.User.AllRemoveCount);
    }
}