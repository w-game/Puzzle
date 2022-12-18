using UI;
using UnityEngine;
using UnityEngine.UI;

public class PopNewPlayerGuideData : ViewData
{
    public override string ViewName => "PopNewPlayerGuide";
    public override ViewType ViewType => ViewType.Popup;
    public override bool Mask => true;
    public override bool AnimaSwitch => true;
}

public class PopNewPlayerGuide : PopupBase
{
    // [SerializeField] private Image gif;
    // [SerializeField] private TextMeshProUGUI tip;
    // [SerializeField] private Button nextTipBtn;
    // [SerializeField] private Button preTipBtn;
    [SerializeField] private Button endTipBtn;

    // [SerializeField] private List<PlayingTipData> playingTipDatas;

    // private int _index = -1;
    public override void OnCreate(params object[] objects)
    {
        // preTipBtn.onClick.AddListener(PreTip);
        // nextTipBtn.onClick.AddListener(NextTip);
        endTipBtn.onClick.AddListener(EndTip);
        
        // RefreshBtns();
        // NextTip();
    }

    // private void PreTip()
    // {
    //     _index--;
    //     RefreshBtns();
    //     ShowTip();
    // }
    //
    // private void NextTip()
    // {
    //     _index++;
    //     RefreshBtns();
    //     ShowTip();
    // }
    //
    // private void RefreshBtns()
    // {
    //     preTipBtn.interactable = _index != 0;
    //     nextTipBtn.interactable = _index != playingTipDatas.Count - 1;
    // }

    // private void ShowTip()
    // {
    //     var playingTipData = playingTipDatas[_index];
    //     // gif.sprite = playingTipData.gif;
    //     tip.text = playingTipData.tip;
    // }

    private void EndTip()
    {
        CloseView();
        GameManager.User.SetOldPlayer();
        GameManager.Instance.GameMode?.Restart();
    }
}

// [Serializable]
// public struct PlayingTipData
// {
//     public Sprite gif;
//     public string tip;
// }