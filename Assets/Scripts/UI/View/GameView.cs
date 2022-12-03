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
    [SerializeField] private TextMeshProUGUI scoreTxt;

    public override void OnCreate(params object[] objects)
    {
        gameBoard.Init(() => loadingMask.SetActive(false));

        startBtn.onClick.AddListener(StartGame);

        UIEvent.Add("Restart", () =>
        {
            startBtn.gameObject.SetActive(true);
        });

        UIEvent.Add("RefreshScore", () => scoreTxt.text = $"Score: {gameBoard.Score}");
    }

    private void StartGame()
    {
        gameBoard.GenerateNewRow();
        startBtn.gameObject.SetActive(false);
    }

    private Vector3 _originPos;
    private void Update()
    {
        FastDown();
    }

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