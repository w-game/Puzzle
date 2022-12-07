namespace ByteDance.Union
{
    public class GMAdSlotDraw : GMAdSlotBase<GMAdSlotDraw.Builder>
    {
        public int Width { get; }
        public int Height { get; }
        public int ADCount { get; }

        private GMAdSlotDraw(Builder builder) : base(builder)
        {
            Width = builder.Width;
            Height = builder.Height;
            ADCount = builder.ADCount;
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotDraw");
#endif
        }


        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            //宽高，生效开屏
            internal int Width { get; private set; } = 640;
            internal int Height { get; private set; } = 320;
            internal int ADCount { get; private set; } = 1;

            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotDraw$Builder")
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

            public Builder SetAdCount(int adCount)
            {
                if (adCount < 1)
                {
                    this.ADCount = 1;
                }
                else if (adCount > 3)
                {
                    this.ADCount = 3;
                }
                else
                {
                    this.ADCount = adCount;
                }
#if UNITY_ANDROID
                _androidSlotHelper?.SetAdCount(adCount);
#endif
                return this;
            }
            public GMAdSlotDraw Build()
            {
                return new GMAdSlotDraw(this);
            }
        }
    }
}