using ByteDance.Union;
using Common;

namespace Ad
{
    public class AdManager : Singleton<AdManager>
    {
        public RewardAd RewardAd { get; } = new();
        public NativeAd NativeAd { get; } = new();
        public BannerAd BannerAd { get; } = new();
        
        public override void Init()
        {
            ABUUserConfig userConfig = new();
            userConfig.logEnable = false;
            ABUAdSDK.setupMSDK("5354735", "msdk demo", userConfig);
            
            PreloadAd();
        }

        private void PreloadAd()
        {
            RewardAd.LoadAd();
            // NativeAd.LoadAd();
        }
    }
}