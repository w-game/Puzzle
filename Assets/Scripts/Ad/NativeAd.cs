using System;
using Common;
using UnityEngine;

namespace Ad
{
    public class NativeAd : AdBase
    {
        public AndroidJavaObject NativeAdJavaObject { get; set; }

        public override void LoadAd()
        {

        }

        public override void ShowAd(Action<bool> callback)
        {
            if (AdManager.Instance.NativeAdSwitch)
            {
                SLog.D("Native Ad", "Native Ad请求展示");
                LoadAd();
            }
        }

        public override void CloseAd()
        {
            NativeAdJavaObject?.Call("destroy");
        }
    }
}