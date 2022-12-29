using System;
using Common;

namespace Ad
{
    public class BannerAd : AdBase
    {
        public const string Tag = "Banner Ad";

        public override void LoadAd()
        {

        }

        public override void ShowAd(Action<bool> callback)
        {
            LoadAd();
        }

        public override void CloseAd()
        {
            
        }
    }
}