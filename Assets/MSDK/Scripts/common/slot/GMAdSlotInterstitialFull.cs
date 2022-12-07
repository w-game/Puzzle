using System.Collections.Generic;

namespace ByteDance.Union
{
    public class GMAdSlotInterstitialFull : GMAdSlotBase<GMAdSlotInterstitialFull.Builder>
    {
        public int Width { get; }
        public int Height { get; }
        public string UserID { get; }
        public int Orientation { get; }
        public string RewardName { get; }
        public int RewardAmount { get; }
        public Dictionary<string, string> CustomData { get; }

        private GMAdSlotInterstitialFull(Builder builder) : base(builder)
        {
            Width = builder.Width;
            Height = builder.Height;
            UserID = builder.UserID;
            Orientation = builder.Orientation;
            RewardName = builder.RewardName;
            RewardAmount = builder.RewardAmount;
            CustomData = builder.CustomData;
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotInterstitialFull");
#endif
        }


        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            //宽高，生效开屏
            internal int Width { get; private set; } = 640;
            internal int Height { get; private set; } = 320;

            internal string UserID { get; private set; }
            internal int Orientation { get; private set; }
            internal string RewardName { get; private set; } = "";
            internal int RewardAmount { get; private set; } = 0;
            internal Dictionary<string, string> CustomData { get; private set; }


            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotInterstitialFull$Builder")
#endif
            )
            {
            }

            public Builder SetImageAcceptedSize(int width, int height)
            {
                this.Width = width;
                this.Height = height;
#if UNITY_ANDROID
                _androidSlotHelper?.SetImageAdSize(width, height);
#endif
                return this;
            }

            public Builder SetRewardName(string rewardName)
            {
                this.RewardName = rewardName;
#if UNITY_ANDROID
                _androidSlotHelper?.SetRewardName(rewardName);
#endif
                return this;
            }

            public Builder SetRewardAmount(int rewardAmount)
            {
                this.RewardAmount = rewardAmount;
#if UNITY_ANDROID
                _androidSlotHelper?.setRewardAmount(rewardAmount);
#endif
                return this;
            }

            public Builder SetCustomData(Dictionary<string, string> customData)
            {
                this.CustomData = customData;
#if UNITY_ANDROID
                _androidSlotHelper?.SetCustomData(CustomData);
#endif
                return this;
            }

            public Builder SetUserID(string userID)
            {
                this.UserID = userID;
#if UNITY_ANDROID
                _androidSlotHelper?.SetUserID(userID);
#endif
                return this;
            }

            public Builder SetOrientation(ABUAdOrientationType orientation)
            {
                Orientation = (int) orientation;

#if UNITY_ANDROID
                _androidSlotHelper?.SetOrientation(Orientation);
#endif
                return this;
            }


            public GMAdSlotInterstitialFull Build()
            {
                return new GMAdSlotInterstitialFull(this);
            }
        }
    }
}