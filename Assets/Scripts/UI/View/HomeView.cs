using TMPro;
using UI;
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
    [SerializeField] private Button startGameBtn;
    [SerializeField] private TextMeshProUGUI maxScore;
    [SerializeField] private TextMeshProUGUI allRemove;
    public override void OnCreate(params object[] objects)
    {
        startGameBtn.onClick.AddListener(StartGame);
        
        RefreshView();
        EventCenter.Add("RefreshView", RefreshView);
    }

    private void StartGame()
    {
        UIManager.Instance.PushMain<GameViewData>();
    }

    private void RefreshView()
    {
        maxScore.text = GameManager.User.MaxScore.ToString();
        allRemove.text = GameManager.User.AllRemoveCount.ToString();
    }
}