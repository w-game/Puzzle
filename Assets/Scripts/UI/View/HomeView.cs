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
    [SerializeField] private Button StartGameBtn;
    public override void OnCreate(params object[] objects)
    {
        StartGameBtn.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        UIManager.Instance.PushMain<GameViewData>();
    }
}