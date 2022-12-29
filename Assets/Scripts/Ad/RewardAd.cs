using System;

namespace Ad
{
    public class RewardAd : AdBase
    {
        public const string Tag = "Reward Ad";
        public bool LoadSuccess { get; set; }
        public override void LoadAd()
        {

        }

        public override void ShowAd(Action<bool> callback)
        {
            if (GameManager.IsDebug)
            {
                callback?.Invoke(true);
                return;
            }
        }

        public override void CloseAd()
        {
            
        }
    }
}