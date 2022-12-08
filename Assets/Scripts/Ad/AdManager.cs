using ByteDance.Union;

namespace Common
{
    public class AdManager : Singleton<AdManager>
    {
        private RewardAd _rewardAd;
        
        public override void Init()
        {
            ABUUserConfig userConfig = new();
            userConfig.logEnable = true;
            ABUAdSDK.setupMSDK("5354735", "msdk demo", userConfig);
        }

        public void LoadRewardAd()
        {
            _rewardAd.LoadAd();
        }
    }
}