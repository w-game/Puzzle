using System;
using System.Collections.Generic;

namespace ByteDance.Union
{
    public class GMAdSlotSplash : GMAdSlotBase<GMAdSlotSplash.Builder>
    {
        internal string UserID { get; private set; }
        internal int Width { get; private set; }
        internal int Height { get; private set; }
        internal bool SplashPreLoad { get; private set; }
        internal int TimeOut { get; private set; }
        internal ABUSplashButtonType SplashButtonType { get; private set; } // 设置开屏点击区域
        internal bool ForceLoadBottom { get; private set; } //强制加载开屏兜底
        internal bool SplashShakeButton { get; private set; } //开屏摇一摇开关

        private GMAdSlotSplash(Builder builder) : base(builder)
        {
            UserID = builder.UserID;
            Width = builder.Width;
            Height = builder.Height;
            SplashPreLoad = builder.SplashPreLoad;
            SplashButtonType = builder.SplashButtonType;
            ForceLoadBottom = builder.ForceLoadBottom;
            SplashShakeButton = builder.SplashShakeButton;
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidSlot = builder.GetAndroidSlot($"{SlotClassPackage}.GMAdSlotSplash");
#endif
        }


        public new class Builder : GMAdSlotBase<Builder>.Builder
        {
            internal string UserID { get; private set; }
            internal int Width { get; private set; }
            internal int Height { get; private set; }
            internal bool SplashPreLoad { get; private set; }
            internal int TimeOut { get; private set; }
            internal ABUSplashButtonType SplashButtonType { get; private set; } // 设置开屏点击区域
            internal bool ForceLoadBottom { get; private set; } //强制加载开屏兜底
            internal bool SplashShakeButton { get; private set; } //开屏摇一摇开关

            public Builder() : base(
#if UNITY_ANDROID && !UNITY_EDITOR
                new AndroidSlotHelper($"{SlotClassPackage}.GMAdSlotSplash$Builder")
#endif
            )
            {
            }

            public Builder SetSplashShakeButton(bool splashShakeButton)
            {
                this.SplashShakeButton = splashShakeButton;
#if UNITY_ANDROID
                _androidSlotHelper?.SetSplashShakeButton(splashShakeButton);
#endif
                return this;
            }

            public Builder SetSplashButtonType(ABUSplashButtonType splashButtonType)
            {
                this.SplashButtonType = splashButtonType;
// 安卓已废弃，不再透传
// #if UNITY_ANDROID
//                 _androidSlotHelper?.SetSplashButtonType((int)splashButtonType);
// #endif
                return this;
            }


            public Builder SetImageAdSize(int width, int height)
            {
                this.Width = width;
                this.Height = height;
#if UNITY_ANDROID
                _androidSlotHelper?.SetImageAdSize(width, height);
#endif
                return this;
            }

           

            public Builder SetSplashPreLoad(bool splashPreLoad)
            {
                SplashPreLoad = splashPreLoad;
#if UNITY_ANDROID
                _androidSlotHelper?.SetSplashPreLoad(splashPreLoad);
#endif
                return this;
            }

            

            public Builder SetTimeOut(int timeOut)
            {
                this.TimeOut = timeOut;
#if UNITY_ANDROID
                _androidSlotHelper?.SetTimeOut(timeOut);
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


            public GMAdSlotSplash Build()
            {
                return new GMAdSlotSplash(this);
            }
        }
    }
}