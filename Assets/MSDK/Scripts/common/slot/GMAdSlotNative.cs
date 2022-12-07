namespace ByteDance.Union
{
    public class GMAdSlotNative : GMAdSlotBase<GMAdSlotNative.Builder>
    {
        public int Width { get; }
        public int Height { get; }
        public int ADCount { get; }
        public ABUAdStyleType AdStyleType { get; }

        private GMAdSlotNative(Builder builder) : base(builder)
        {
            Width = builder.Width;
            Height = builder.Height;
            ADCount = builder.ADCount;
            AdStyleType = builder.AdStyleType;
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotNative");
#endif
        }


        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            //宽高，生效开屏
            internal int Width { get; private set; } = 640;
            internal int Height { get; private set; } = 320;

            internal int ADCount { get; private set; } = 1;
            internal ABUAdStyleType AdStyleType = ABUAdStyleType.TypeNativeAD; //优先以平台设置为准

            internal string UserID = "";

            // internal AndroidJavaObject AdmobNativeAdOptions{ get; private set; };
            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotNative$Builder")
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
            public Builder SetAdCount(int mAdCount) {
                this.ADCount = mAdCount;
#if UNITY_ANDROID
                _androidSlotHelper?.SetAdCount(mAdCount);
#endif
                return this;
            }

            public Builder SetUserID(string userID) {
                UserID = userID;
#if UNITY_ANDROID
                _androidSlotHelper?.SetUserID(userID);
#endif
                return this;
            }

            public Builder SetAdStyleType(ABUAdStyleType adStyleType) {
                AdStyleType = adStyleType;
#if UNITY_ANDROID
                _androidSlotHelper?.SetAdStyleType((int)adStyleType);
#endif
                return this;
            }

            public GMAdSlotNative Build()
            {
                return new GMAdSlotNative(this);
            }
        }
    }
}