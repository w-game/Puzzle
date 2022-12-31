using System;
using ByteDance.Union;
using Common;

namespace Ad
{
    public class RewardAd : AdBase
    {
        public const string Tag = "Reward Ad";

        private bool IsLoaded { get; set; }
        public override void LoadAd(Action<bool> callback)
        {
            var adSlot = new GMAdSlotRewardVideo.Builder()
                .SetCodeId(AdIds.RewardAd)
                .SetUserID("user123") // 用户id,必传参数   只对穿山甲adn有效
                .setScenarioId("1233211223")
                .Build();
            ABURewardVideoAd.LoadRewardVideoAd(adSlot, new RewardVideoAdListener(this)
            {
                OnLoadEnd = result =>
                {
                    UIManager.Instance.SetTopMask(false);
                    if (result)
                    {
                        IsLoaded = true;
                        ShowAd(callback);
                    }
                    else
                    {
                        UIManager.Instance.ShowToast(ToastType.Info, GameManager.Language.AdLoadFail);
                        callback?.Invoke(false);
                    }
                }
            });
        }

        public override void ShowAd(Action<bool> callback)
        {
            if (GameManager.IsDebug)
            {
                callback?.Invoke(true);
                return;
            }
            
            if (!IsLoaded || !ABURewardVideoAd.isReady())
            {
                UIManager.Instance.SetTopMask(true);
                LoadAd(callback);
                SLog.D(Tag, "请先加载广告或等广告加载完成");
                return;
            }
            
            // Dictionary<string, object> ritSceneMap = new Dictionary<string, object>();
            // ritSceneMap.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneType, ABURitSceneType.ABURitSceneType_game_finish_rewards);
            // Dictionary<string, object> ritSceneMap_custom = new Dictionary<string, object>();
            // ritSceneMap_custom.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneType, ABURitSceneType.BURitSceneType_custom);
            // ritSceneMap_custom.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneDescription, "custom info");
            // 普通展示方式
            ABURewardVideoAd.ShowRewardVideoAd(new RewardAdInteractionListener(this) { Callback = callback });
            IsLoaded = false;
        }

        public override void CloseAd()
        {
            
        }
    }
    
    public sealed class RewardVideoAdListener : ABURewardVideoAdCallback
    {
        private RewardAd _rewardAd;

        public Action<bool> OnLoadEnd;
        public RewardVideoAdListener(RewardAd rewardAd)
        {
            _rewardAd = rewardAd;
        }

        public void OnError(int code, string message)
        {
            var errMsg = "OnRewardVideoAdLoadError-- code : " + code + "--message : " + message;
            SLog.E(RewardAd.Tag, errMsg);
            OnLoadEnd?.Invoke(false);
        }

        public void OnRewardVideoAdLoad(object ad)
        {
            SLog.D(RewardAd.Tag, "OnRewardVideoAdLoad");
            OnLoadEnd?.Invoke(true);
        }

        public void OnRewardVideoAdCached()
        {
            SLog.D(RewardAd.Tag, "OnRewardVideoCached");
        }
    }

    public sealed class RewardAdInteractionListener : ABURewardAdInteractionCallback
    {
        private RewardAd _rewardAd;
        public Action<bool> Callback { get; set; }

        public RewardAdInteractionListener(RewardAd rewardAd)
        {
            _rewardAd = rewardAd;
        }

        public void OnAdShow()
        {
            SLog.D(RewardAd.Tag, "Reward Ad Showed");
            string ecpm = ABURewardVideoAd.GetPreEcpm();
            string ritID = ABURewardVideoAd.GetAdNetworkRitId();
            string adnName = ABURewardVideoAd.GetAdRitInfoAdnName();
            SLog.D(RewardAd.Tag, $"ecpm: {ecpm}, ritId: {ritID}, adnName: {adnName}");

            UIManager.Instance.ShowToast(ToastType.Info, GameManager.Language.GetRewardTip);
            Callback?.Invoke(true);
        }

        public void OnViewRenderFail(int code, string message)
        {
            var s = "code : " + code + "--message = " + message;
            Log.D("<Unity Log>..." + s);
        }

        public void OnAdVideoBarClick()
        {
            SLog.D(RewardAd.Tag, "expressRewardAd bar click");
        }

        public void OnAdClose()
        {
            SLog.D(RewardAd.Tag, "expressRewardAd close");
        }

        public void OnVideoComplete()
        {
            SLog.D(RewardAd.Tag, "expressRewardAd complete");
        }

        public void OnVideoError(int errCode, string errMsg)
        {
            string logs = " < Unity Log > ..." + "play error code:" + errCode + ",errMsg:" + errMsg;
            SLog.E(RewardAd.Tag, logs);
        }

        public void OnRewardVerify(bool rewardVerify)
        {
            var message = "verify:" + rewardVerify;
            SLog.D(RewardAd.Tag, message);
            Callback?.Invoke(false);
        }

        public void OnSkippedVideo()
        {
            var message = "expressrewardAd OnSkippedVideo for Android";
            SLog.D(RewardAd.Tag, message);
        }

        /// <summary>
        /// Ons the other rit  in waterfall occur filll error.Call back after show.
        /// fillFailMessageInfo:Json string whose outer layer is an array,and the array elements are dictionaries.
        /// The keys of Internal dictionary are the following:
        /// 1."mediation_rit": 广告代码位
        /// 2.@"adn_name": 属于哪家广告adn
        /// 3."error_message": 错误信息
        /// 4."error_code": 错误码
        /// </summary>
        public void OnWaterfallRitFillFail(string fillFailMessageInfo)
        {
            SLog.D(RewardAd.Tag, $"fillFailMessageInfo: {fillFailMessageInfo}");
        }

        /// <summary>
        /// Fail to show ad.Now only for iOS.
        /// errcode 错误码
        /// errorMsg 错误描述
        /// </summary>
        public void OnAdShowFailed(int errcode, string errorMsg)
        {
            SLog.D(RewardAd.Tag, $"OnAdShowFailed Errcode: {errcode}, errMsg: {errorMsg}");
        }

        public void OnRewardVerify(bool rewardVerify, ABUAdapterRewardAdInfo rewardInfo)
        {
           SLog.D(RewardAd.Tag, "InterstitialFullAd OnRewardVerify"
               + ", rewardName : " + rewardInfo.rewardName// 发放奖励的名称
               + ", rewardAmount : " + rewardInfo.rewardAmount// 发放奖励的金额
               + ", tradeId : " + rewardInfo.tradeId// 交易的唯一标识
               + ", verify : " + rewardInfo.verify// 是否验证通过
               + ", verifyByGroMoreS2S : " + rewardInfo.verifyByGroMoreS2S// 是否是通过GroMore的S2S的验证
               + ", adnName : " + rewardInfo.adnName// 验证奖励发放的媒体名称，官方支持的ADN名称详见`ABUAdnType`注释部分，自定义ADN名称同平台配置
               + ", reason : " + rewardInfo.reason// 验证失败的原因
               + ", errorCode : " + rewardInfo.errorCode// 无法完成验证的错误码
               + ", errorMsg : " + rewardInfo.errorMsg// 无法完成验证的错误原因，包括网络错误、服务端无响应、服务端无法验证等
               + ", rewardType : " + rewardInfo.rewardType// 奖励类型，0:基础奖励 1:进阶奖励-互动 2:进阶奖励-超过30s的视频播放完成  目前支持返回该字段的adn：csj
               + ", rewardPropose : " + rewardInfo.rewardPropose);// 建议奖励百分比， 基础奖励为1，进阶奖励为0.0 ~ 1.0，开发者自行换算  目前支持返回该字段的adn：csj
        }

    }
}