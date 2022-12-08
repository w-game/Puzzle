using Common;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PopGameResultData : ViewData
{
    public override string ViewName => "PopGameResult";

    public override ViewType ViewType => ViewType.Popup;

    public override bool Mask => true;
}

public class PopGameResult : ViewBase
{
    [SerializeField] private Button restart;
    [SerializeField] private Button revive;
    public override void OnCreate(params object[] objects)
    {
        restart.onClick.AddListener(Restart);
        revive.onClick.AddListener(Revive);
    }

    private void Restart()
    {
        GameBoard.Instance.RefreshBoard();
        EventCenter.Invoke("Restart");
        CloseView();
    }

    private void Revive()
    {
        GameBoard.Instance.Revive();
        AdManager.Instance.RewardAd.ShowAd();
        CloseView();
    }
}