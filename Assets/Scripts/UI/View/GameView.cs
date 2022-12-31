using DG.Tweening;
using TMPro;
using UI;
using UI.Popup;
using UnityEngine;
using UnityEngine.UI;

public abstract class GameView : ViewBase
{
    public enum EventKeys
    {
        ModifyStartBtnStatus,
        CheckCombo,
        RefreshView,
        RefreshGoal,
        RefreshRoundCount
    }
    
    [SerializeField] protected PuzzleGame puzzleGame;
    [SerializeField] private GameObject loadingMask;
    [SerializeField] private Button startBtn;
    [SerializeField] private Button homeBtn;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI comboTxt;
    [SerializeField] private LayoutElement topElementAdapt;
    [SerializeField] protected LevelGoalElement goalElement;
    [SerializeField] protected TextMeshProUGUI roundCount;

    private bool _switch = true;

    private const float ComboTime = 10f;
    private float _curComboTime;
    private int _comboCount;

    public override void OnCreate(params object[] objects)
    {
        puzzleGame.OnGameInit += () =>
        {
            loadingMask.SetActive(false);
            if (GameManager.User.NewPlayerGuide)
            {
                UIManager.Instance.PushPop<PopNewPlayerGuideData>(puzzleGame);
            }
            else
            {
                puzzleGame.StartGame();
            }
        };

        startBtn.onClick.AddListener(StartGame);
        homeBtn.onClick.AddListener(OnHomeBtnClicked);

        AddEvent<bool>(EventKeys.ModifyStartBtnStatus, status => _switch = status);
        AddEvent(EventKeys.CheckCombo, CheckCombo);
        AddEvent(EventKeys.RefreshView, Refresh);
        AddEvent(EventKeys.RefreshGoal, goalElement.RefreshGoal);
        AddEvent<int>(EventKeys.RefreshRoundCount, RefreshRoundCount);
        Refresh();
        puzzleGame.Init();
    }

    public override void ScreenAdapt(Rect rect)
    {
        topElementAdapt.minHeight += rect.y;
    }

    private void StartGame()
    {
        if (_switch)
        {
            puzzleGame.NextRound();
            var sequence = DOTween.Sequence();
            sequence.Append(startBtn.transform.DOLocalMove(Vector3.up * 30, 0.1f).SetEase(Ease.OutQuad));
            sequence.Append(startBtn.transform.DOLocalMove(Vector3.zero, 0.1f).SetEase(Ease.InQuad));
        }
    }

    private void OnHomeBtnClicked()
    {
        UIManager.Instance.ShowCheckBox(GameManager.Language.BackToHomeCheckDes, () =>
        {
            puzzleGame.EndGame();
            CloseView();
        });
    }

    protected virtual void Refresh()
    {
        scoreTxt.text = $"{puzzleGame.Score}";
    }

    private void CheckCombo()
    {
        _comboCount++;
        _curComboTime = ComboTime;
        comboTxt.text = $"Combo x{_comboCount}";
        comboTxt.gameObject.SetActive(true);
        var sequence = DOTween.Sequence();
        sequence.Append(comboTxt.transform.DOScale(1.2f, 0.2f));
        sequence.Join(comboTxt.DOFade(1, 0.2f));
        sequence.Append(comboTxt.transform.DOScale(1f, 0.2f));
        sequence.AppendInterval(3f);
        sequence.Append(comboTxt.DOFade(0.5f, 0.2f));
    }

    private void Update()
    {
        _curComboTime -= Time.deltaTime;
        if (_curComboTime <= 0)
        {
            _comboCount = 0;
            // comboTxt.gameObject.SetActive(false);
            comboTxt.text = $"Combo x{_comboCount}";
        }
    }

    private void RefreshRoundCount(int count)
    {
        roundCount.text = $"{count}";
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
                // gameBoard.DoFastDown();
            }
        }
    }
}