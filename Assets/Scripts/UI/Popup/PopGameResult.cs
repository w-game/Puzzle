using Common;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PopGameResultData : ViewData
{
    public override string ViewName => "PopGameResult";

    public override ViewType ViewType => ViewType.Popup;

    public override bool Mask => true;
    public override bool AnimaSwitch => true;
}

public class PopGameResult : PopupBase
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
        AdManager.Instance.RewardAd.ShowAd(result =>
        {
            if (result)
            {
                GameBoard.Instance.Revive();
                CloseView();
            }
        });
    }
}