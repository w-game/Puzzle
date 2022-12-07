namespace ByteDance.Union
{
    public class GMAdSlotInterstitial : GMAdSlotBase<GMAdSlotInterstitial.Builder>
    {
        public int Width { get; }
        public int Height { get; }

        private GMAdSlotInterstitial(Builder builder) : base(builder)
        {
            Width = builder.Width;
            Height = builder.Height;
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotInterstitial");
#endif
        }


        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            //宽高，生效开屏
            internal int Width { get; private set; } = 640;
            internal int Height { get; private set; } = 320;

            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotInterstitial$Builder")
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
            public GMAdSlotInterstitial Build()
            {
                return new GMAdSlotInterstitial(this);
            }
        }
    }
}