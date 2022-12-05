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
    public override void OnCreate(params object[] objects)
    {
        restart.onClick.AddListener(Restart);
    }

    public void Restart()
    {
        GameBoard.Instance.RefreshBoard();
        EventCenter.Invoke("Restart");
        CloseView();
    }
}