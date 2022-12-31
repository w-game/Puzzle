using ByteDance.Union;
using Common;
using UnityEngine;

namespace Ad
{
    public class AdManager : Singleton<AdManager>
    {
        public RewardAd RewardAd { get; } = new();
        public NativeAd NativeAd { get; } = new();
        public BannerAd BannerAd { get; } = new();
        public SplashAd SplashAd { get; } = new();

        public bool NativeAdSwitch
        {
            get => PlayerPrefs.GetInt("NATIVE_AD_SWITCH", 1) == 1;
            set => PlayerPrefs.SetInt("NATIVE_AD_SWITCH", value ? 1 : 0);
        }
        
        public override void Init()
        {
            ABUUserConfig userConfig = new();
            userConfig.logEnable = false;
            ABUAdSDK.setupMSDK("5354735", "msdk demo", userConfig);
        }
    }
}