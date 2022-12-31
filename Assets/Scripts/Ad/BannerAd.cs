using System;
using ByteDance.Union;
using Common;

namespace Ad
{
    public class BannerAd : AdBase
    {
        public const string Tag = "Banner Ad";

        public override void LoadAd(Action<bool> callback)
        {
            var slot = new GMAdSlotBanner.Builder()
                .SetCodeId("102232371")
                .SetImageAdSize(600, 500)
                .setScenarioId("1233211223")
                .Build();
            ABUBannerAd.LoadBannerAd(slot, new BannerAdListener(this), new BannerAdCallback(this));
        }

        public override void ShowAd(Action<bool> callback)
        {
            LoadAd(callback);
        }

        public override void CloseAd()
        {
            
        }
    }

    public class BannerAdListener : ABUBannerAdCallback
    {
        private BannerAd _bannerAd;
        public BannerAdListener(BannerAd bannerAd)
        {
            _bannerAd = bannerAd;
        }
        
        public void OnError(int code, string message)
        {
            SLog.D(BannerAd.Tag, $"code: {code}\n{message}");
        }

        public void OnBannerAdLoad(ABUBannerAd ad)
        {
            SLog.D(BannerAd.Tag, $"On Banner Ad load");
        }

        public void OnWaterfallRitFillFail(string fillFailMessageInfo)
        {
            SLog.D(BannerAd.Tag, $"WaterfallRitFillFail: {fillFailMessageInfo}");
        }
    }

    public class BannerAdCallback : ABUBannerAdInteractionCallback
    {
        private BannerAd _bannerAd;
        
        public BannerAdCallback(BannerAd bannerAd)
        {
            _bannerAd = bannerAd;
        }
        
        public void OnAdClicked()
        {
            SLog.D(BannerAd.Tag, $"On Banner Ad clicked");
        }

        public void OnAdShow()
        {
            SLog.D(BannerAd.Tag, $"On Banner Ad show");
        }

        public void OnAdDismiss()
        {
            SLog.D(BannerAd.Tag, $"On Banner Ad dismiss");
        }
    }
}