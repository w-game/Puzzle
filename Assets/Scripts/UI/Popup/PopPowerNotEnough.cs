using Ad;
using Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popup
{
    public class PopPowerNotEnoughData : ViewData
    {
        public override string ViewName => "PopPowerNotEnough";
        public override ViewType ViewType => ViewType.Popup;
        public override bool Mask => true;
        public override bool AnimaSwitch => true;
    }

    public class PopPowerNotEnough : PopupBase
    {
        [SerializeField] private Button receivePowerBtn;
        [SerializeField] private Button sureBtn;

        [Header("本地化")] 
        [SerializeField] private TextMeshProUGUI title;
        [Space]
        [SerializeField] private TextMeshProUGUI des;
        [SerializeField] private TextMeshProUGUI getPowerBtnTxt;
        [SerializeField] private TextMeshProUGUI sureBtnTxt;

        public override void OnCreate(params object[] objects)
        {
            receivePowerBtn.onClick.AddListener(ReceivePower);
            sureBtn.onClick.AddListener(OnSureBtnClicked);
            
            ShowNativeAd();
            EventCenter.Invoke(GamePower.EventKeys.Show);
        }

        public override void Localization()
        {
            var language = GameManager.Language;
            title.text = language.PowerNotEnoughTitle;
            des.text = language.PowerNotEnoughDes;

            getPowerBtnTxt.text = language.GetPowerBtnText;
            sureBtnTxt.text = language.SureText;
        }

        private void OnSureBtnClicked()
        {
            AdManager.Instance.NativeAd.CloseAd();
            UIManager.Instance.BackToHome();
            EventCenter.Invoke(GamePower.EventKeys.Close);
        }

        private void ReceivePower()
        {
            AdManager.Instance.RewardAd.ShowAd(result =>
            {
                if (result)
                {
                    GameManager.User.IncreasePower(5);
                    UIManager.Instance.ShowToast(ToastType.Info, GameManager.Language.GetPowerTip);
                    CloseView();
                    EventCenter.Invoke(GamePower.EventKeys.Close);
                }
            });
        }
    }
}