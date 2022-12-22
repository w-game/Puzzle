using Ad;
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

        public override void OnCreate(params object[] objects)
        {
            receivePowerBtn.onClick.AddListener(ReceivePower);
            sureBtn.onClick.AddListener(OnSureBtnClicked);
            
            ShowNativeAd();
            EventCenter.Invoke(GamePower.EventKeys.Show);
        }

        private void OnSureBtnClicked()
        {
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
                }
            });
        }
    }
}