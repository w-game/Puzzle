using System;
using ByteDance.Union;

namespace Ad
{
    public class BannerAd : AdBase
    {
        public override void LoadAd()
        {
            string unitID = "";
            var slot = new GMAdSlotBanner.Builder()
                .SetCodeId(unitID)
                .SetImageAdSize(320, 50)
                .setScenarioId("1233211223")
                .Build();
            // ABUBannerAd.LoadBannerAd(slot, new BannerAdCallback(this), new BannerAdInteractionCallback(this));
        }

        public override void ShowAd(Action<bool> callback)
        {
            
        }
    }
}