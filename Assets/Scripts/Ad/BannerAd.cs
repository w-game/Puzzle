using Common;
using UnityEngine.Events;

namespace Ad
{
    public class BannerAd : AdBase
    {
        public const string Tag = "Banner Ad";

        public override void InitAd()
        {
            OmEvents.onBannerLoadSuccessEvent += msg =>
            {
                SLog.D(Tag, $"横幅广告读取成功！\n{msg}");
                ShowAd(null);
            };
            
            OmEvents.onBannerLoadFailedEvent += (m1, m2) =>
            {
                SLog.D(Tag, $"横幅广告读取失败！\n{m1}\n{m2}");
            };
        }

        public override void LoadAd()
        {
            Om.Agent.loadBanner("13057", AdSize.BANNER, BannerPostion.BOTTOM);
        }

        public override void ShowAd(UnityAction<bool> callback)
        {
            Om.Agent.displayBanner("13057");
        }

        public override void CloseAd()
        {
            Om.Agent.hideBanner("13057");
        }
    }
}