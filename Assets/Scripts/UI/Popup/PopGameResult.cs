using Ad;
using TMPro;
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

    [Header("本地化")]
    [SerializeField] private TextMeshProUGUI title;
    [Space]
    [SerializeField] private TextMeshProUGUI restartBtnTxt;
    [SerializeField] private TextMeshProUGUI reviveBtnTxt;
    public override void OnCreate(params object[] objects)
    {
        restart.onClick.AddListener(Restart);
        revive.onClick.AddListener(Revive);
    }

    public override void Localization()
    {
        var language = GameManager.Language;
        title.text = language.GameOverText;
        restartBtnTxt.text = language.Restart;
        reviveBtnTxt.text = language.Revive;
    }

    private void Restart()
    {
        GameManager.Instance.CheckPower(PuzzleGame.PowerCost.Endless, () =>
        {
            UIManager.Instance.DecreasePower(restart.transform, () =>
            {
                PuzzleGame.Cur.StartGame();
                CloseView();
            });
        });
    }

    private void Revive()
    {
        AdManager.Instance.RewardAd.ShowAd(result =>
        {
            if (result)
            {
                var game = PuzzleGame.Cur as EndlessGameMode;
                game?.Revive();
                
                CloseView();
            }
        });
    }
}