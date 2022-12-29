using Common;
using UnityEngine.Events;

namespace Ad
{
    public class RewardAd : AdBase
    {
        public const string Tag = "Reward Ad";
        public bool LoadSuccess { get; set; }
        
        public override void InitAd()
        {
            OmEvents.onRewardedVideoShowedEvent += msg =>
            {
                SLog.D(Tag, $"激励视频广告展示成功！\n{msg}");
            };

            OmEvents.onRewardedVideoRewardedEvent += msg =>
            {
                SLog.D(Tag, $"激励视频广告奖励成功！\n{msg}");
                AdCallback?.Invoke(true);
            };

            OmEvents.onRewardedVideoShowFailedEvent += msg =>
            {
                SLog.E(Tag, $"激励视频广告展示失败！\n{msg}");
                AdCallback?.Invoke(false);
            };
        }

        public override void LoadAd()
        {
            
        }

        public override void ShowAd(UnityAction<bool> callback)
        {
            // if (GameManager.IsDebug)
            // {
            //     callback?.Invoke(true);
            //     return;
            // }

            if (Om.Agent.isRewardedVideoReady())
            {
                AdCallback = callback;
                Om.Agent.showRewardedVideo();
            }
            else
            {
                SLog.E(Tag, "激励视频没有准备好！");
            }
        }

        public override void CloseAd()
        {
            
        }
    }
}