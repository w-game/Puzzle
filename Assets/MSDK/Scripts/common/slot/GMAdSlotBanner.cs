using System;

namespace ByteDance.Union
{
    public class GMAdSlotBanner : GMAdSlotBase<GMAdSlotBanner.Builder>
    {
        public int Width;
        public int Height;
        public int BannerSize;
        public bool AllowShowCloseBtn;
        public string UserID;

        private GMAdSlotBanner(Builder builder) : base(builder)
        {
            Width = builder.GetImageAdWidth();
            Height = builder.GetImageAdHeight();
            BannerSize = builder.GetBannerSize();
            AllowShowCloseBtn = builder.GetAllowShowCloseBtn();
            UserID = builder.GetUserID();
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotBanner");
#endif
        }

        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            //宽高，生效开屏
            private int _width = 640;
            private int _height = 320;
            private int _bannerSize = GMAdSize.BANNER_300_250;
            private bool _allowShowCloseBtn = false;
            private string _userID = "";

            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotBanner$Builder")
#endif
            )
            {
            }


            public Builder SetUserID(string userID)
            {
                _userID = userID;
#if UNITY_ANDROID
                _androidSlotHelper?.SetUserID(userID);
#endif
                return this;
            }

            public string GetUserID()
            {
                return _userID;
            }

            public Builder SetImageAdSize(int width, int height)
            {
                _width = width;
                _height = height;
#if UNITY_ANDROID
                _androidSlotHelper?.SetImageAdSize(width, height);
#endif
                return this;
            }

            public int GetImageAdWidth()
            {
                return _width;
            }

            public int GetImageAdHeight()
            {
                return _height;
            }

            public Builder SetBannerSize(int bannerSize)
            {
                _bannerSize = bannerSize;
#if UNITY_ANDROID
                _androidSlotHelper?.SetBannerSize(bannerSize);
#endif
                return this;
            }

            public int GetBannerSize()
            {
                return _bannerSize;
            }

            public Builder SetAllowShowCloseBtn(bool isAllowShowCloseBtn)
            {
                _allowShowCloseBtn = isAllowShowCloseBtn;
#if UNITY_ANDROID
                _androidSlotHelper?.SetAllowShowCloseBtn(isAllowShowCloseBtn);
#endif
                return this;
            }

            public bool GetAllowShowCloseBtn()
            {
                return _allowShowCloseBtn;
            }

            public GMAdSlotBanner Build()
            {
                return new GMAdSlotBanner(this);
            }
        }
    }
}