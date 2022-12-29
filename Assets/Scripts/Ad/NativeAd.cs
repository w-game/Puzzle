using Common;
using UnityEngine.Events;

namespace Ad
{
    public class NativeAd : AdBase
    {
        public override void InitAd()
        {
            OmEvents.onPromotionAdShowedEvent += msg =>
            {
                SLog.D("Native Ad", $"Native Ad展示成功！\n{msg}");
            };

            OmEvents.onPromotionAdShowFailedEvent += msg =>
            {
                SLog.D("Native Ad", $"Native Ad展示失败！\n{msg}");
            };
        }

        public override void LoadAd()
        {
            
        }

        public override void ShowAd(UnityAction<bool> callback)
        {
            if (AdManager.Instance.NativeAdSwitch)
            {
                SLog.D("Native Ad", "Native Ad请求展示");
                LoadAd();
            }
        }

        public override void CloseAd()
        {
            Om.Agent.hidePromotionAd();
        }
    }
}