using System;
using System.Collections.Generic;
using ByteDance.Union;
using Common;
using Newtonsoft.Json;
using UnityEngine;

namespace Ad
{
    public class RewardAd : AdBase
    {
        public const string Tag = "Reward Ad";
        public bool LoadSuccess { get; set; }
        public override void LoadAd()
        {
            var dict = new Dictionary<string, string>();
            dict.Add("key1", "value1");
            dict.Add("key2", "value2");
            dict.Add("key3", "value3");
            var dictJson = JsonConvert.SerializeObject(dict);

            var adSlot = new GMAdSlotRewardVideo.Builder()
                .SetCodeId(AdIds.RewardAd)
                // .SetRewardName("金币") // 奖励的名称
                // .SetRewardAmount(3) // 奖励的数量
                .SetUserID("user123") // 用户id,必传参数   只对穿山甲adn有效
                .setScenarioId("1233211223")
#if UNITY_IOS // 扩展信息这里安卓和iOS传值方式不同
                .SetCustomData(dict)
#else
                .SetCustomData(new Dictionary<string, string>()
                {
                    { "pangle","950602537"},
                })
#endif
                .Build();
            ABURewardVideoAd.LoadRewardVideoAd(adSlot, new RewardVideoAdListener(this));
        }

        public override void ShowAd(Action<bool> callback)
        {
            if (GameManager.IsDebug)
            {
                callback?.Invoke(true);
                return;
            }
            
            // 为保障播放流畅，建议在视频加载完成后展示
            if (!LoadSuccess || !ABURewardVideoAd.isReady())
            {
                SLog.D(Tag, "请先加载广告或等广告加载完成");
                return;
            }
            // ritScene信息
            // 设置已定义的场景
            Dictionary<string, object> ritSceneMap = new Dictionary<string, object>();
            // ABUShowExtroInfoKeySceneType设置为非BURitSceneType_custom即可
            ritSceneMap.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneType, ABURitSceneType.ABURitSceneType_game_finish_rewards);
            // 设置自定义的场景
            Dictionary<string, object> ritSceneMap_custom = new Dictionary<string, object>();
            // ABUShowExtroInfoKeySceneType设置为BURitSceneType_custom即可
            ritSceneMap_custom.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneType, ABURitSceneType.BURitSceneType_custom);
            // 同时请务必设置
            ritSceneMap_custom.Add(ABUConstantHelper.ABUShowExtroInfoKeySceneDescription, "custom info");
            // 普通展示方式
            ABURewardVideoAd.ShowRewardVideoAd(new RewardAdInteractionListener(this) { Callback = callback });
            // 带scene的展示方式
            //ABURewardVideoAd.ShowRewardVideoAdWithRitScene(new RewardAdInteractionListener(this), ritSceneMap);
            LoadSuccess = false;
        }

        public override void CloseAd()
        {
            
        }
    }
    
    public sealed class RewardVideoAdListener : ABURewardVideoAdCallback
    {
        private RewardAd _rewardAd;

        public RewardVideoAdListener(RewardAd rewardAd)
        {
            _rewardAd = rewardAd;
        }

        public void OnError(int code, string message)
        {
            var errMsg = "OnRewardVideoAdLoadError-- code : " + code + "--message : " + message;
            SLog.E(RewardAd.Tag, errMsg);
        }

        public void OnRewardVideoAdLoad(object ad)
        {
            SLog.D(RewardAd.Tag, "OnRewardVideoAdLoad");
        }

        public void OnRewardVideoAdCached()
        {
            SLog.D(RewardAd.Tag, "OnRewardVideoCached");
            _rewardAd.LoadSuccess = true;
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
            _rewardAd.LoadSuccess = false;
            string ecpm = ABURewardVideoAd.GetPreEcpm();
            string ritID = ABURewardVideoAd.GetAdNetworkRitId();
            string adnName = ABURewardVideoAd.GetAdRitInfoAdnName();
            SLog.D(RewardAd.Tag, $"ecpm: {ecpm}, ritId: {ritID}, adnName: {adnName}");

            _rewardAd.LoadAd();
            UIManager.Instance.ShowToast(ToastType.Info, GameManager.Language.GetRewardTip);
            Callback?.Invoke(true);
        }

        public void OnViewRenderFail(int code, string message)
        {
            var s = "code : " + code + "--message = " + message;
            Log.D("<Unity Log>..." + s);
            Callback?.Invoke(false);
        }

        public void OnAdVideoBarClick()
        {
            SLog.D(RewardAd.Tag, "expressRewardAd bar click");
        }

        public void OnAdClose()
        {
            SLog.D(RewardAd.Tag, "expressRewardAd close");
            _rewardAd.LoadSuccess = false;
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