using ByteDance.Union;

namespace Common
{
    public class AdManager : Singleton<AdManager>
    {
        public RewardAd RewardAd { get; } = new();
        
        public override void Init()
        {
            ABUUserConfig userConfig = new();
            userConfig.logEnable = true;
            ABUAdSDK.setupMSDK("5354735", "msdk demo", userConfig);
            
            PreloadAd();
        }

        private void PreloadAd()
        {
            RewardAd.LoadAd();
        }
    }
}