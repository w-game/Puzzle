using Common;
using DG.Tweening;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class GameViewData : ViewData
{
    public override string ViewName => "GameView";

    public override ViewType ViewType => ViewType.View;

    public override bool Mask => true;
}

public class GameView : ViewBase
{
    [SerializeField] private GameBoard gameBoard;
    [SerializeField] private GameObject loadingMask;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button homeBtn;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI comboTxt;
    [SerializeField] private TextMeshProUGUI nextBlockScore;

    private bool _switch = true;

    private const float ComboTime = 10f;
    private float _curComboTime;
    private int _comboCount;
    public override void OnCreate(params object[] objects)
    {
        gameBoard.Init(() => loadingMask.SetActive(false));

        startBtn.onClick.AddListener(StartGame);
        homeBtn.onClick.AddListener(OnHomeBtnClicked);

        EventCenter.Add("EnableStartBtn", () => _switch = true);
        EventCenter.Add("CheckCombo", CheckCombo);
        EventCenter.Add("RefreshGameView", Refresh);
        Refresh();
    }

    private void StartGame()
    {
        if (_switch)
        {
            gameBoard.GenerateNewRow();
            _switch = false;
            var sequence = DOTween.Sequence();
            sequence.Append(startBtn.transform.DOLocalMove(Vector3.up * 30, 0.2f).SetEase(Ease.OutQuad));
            sequence.Append(startBtn.transform.DOLocalMove(Vector3.zero, 0.2f).SetEase(Ease.InQuad));
        }
    }

    private void OnHomeBtnClicked()
    {
        gameBoard.Stop();
        CloseView();
        SLog.D("GameView", "OnHomeBtnClicked");
    }

    private void Refresh()
    {
        scoreTxt.text = $"{gameBoard.Score}";
        nextBlockScore.text = $"还差<color=#C24347>{gameBoard.NextBlockScore - gameBoard.Score}</color>分";
    }

    private void CheckCombo()
    {
        _comboCount++;
        _curComboTime = ComboTime;
        comboTxt.text = $"Combo x{_comboCount}";
        comboTxt.gameObject.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Append(comboTxt.transform.DOScale(1.2f, 0.2f));
        sequence.Append(comboTxt.transform.DOScale(1f, 0.2f));
    }

    private void Update()
    {
        _curComboTime -= Time.deltaTime;
        if (_curComboTime <= 0)
        {
            _comboCount = 0;
            comboTxt.gameObject.SetActive(false);
        }
    }

    private Vector3 _originPos;

    private void FastDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _originPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            var targetPos = Input.mousePosition;
            var dir = (targetPos - _originPos).normalized;

            var angle = Vector3.Angle(Vector3.down, dir);

            if (angle <= 30 && Vector3.Distance(targetPos, _originPos) >= 200)
            {
                gameBoard.DoFastDown();
            }
        }
    }
}