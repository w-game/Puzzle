using Common;
using UnityEngine;

namespace Ad
{
    public class AdManager : Singleton<AdManager>
    {
        public RewardAd RewardAd { get; } = new();
        public NativeAd NativeAd { get; } = new();
        public BannerAd BannerAd { get; } = new();

        public bool NativeAdSwitch
        {
            get => PlayerPrefs.GetInt("NATIVE_AD_SWITCH", 1) == 1;
            set => PlayerPrefs.SetInt("NATIVE_AD_SWITCH", value ? 1 : 0);
        }
        
        public override void Init()
        {
            SLog.D("Ads","开始初始化广告SDK");
            OmEvents.onSdkInitSuccessEvent += OnInitSuccess;
            OmEvents.onSdkInitFailedEvent += OnInitFailed;
            string appId = "YivoA4Zs7qGtA62X7WBd4Q8PRj8nNmzw";
#if UNITY_IOS && !UNITY_EDITOR
            appId = "cS8YQtKcRKM3uYl4egXg7pdLiN5mR3d5";
#endif
            SLog.D("Ads",$"{Om.Agent.GetType()} [{appId}]");
            Om.Agent.init(appId);
        }

        private void OnInitSuccess()
        {
            NativeAd.InitAd();
            RewardAd.InitAd();
            BannerAd.InitAd();
            BannerAd.LoadAd();
            SLog.D("Ads", "广告SDK初始化完成！");
        }
        
        private void OnInitFailed(string msg)
        {
            SLog.D("Ads", $"广告SDK初始化失败！\n{msg}");
        }

        private void PreloadAd()
        {
            RewardAd.LoadAd();
        }
    }
}