using System;
using System.Collections.Generic;
using ByteDance.Union;
using Common;
using UnityEngine;

namespace Ad
{
    public class NativeAd : AdBase
    {
        public AndroidJavaObject NativeAdJavaObject { get; set; }
        public ABUNativeAd NativeAdInstance { get; set; }

        public override void LoadAd(Action<bool> callback)
        {
            var adUnit = new GMAdSlotNative.Builder()
                .SetCodeId(AdIds.NativeAd)
                .SetMuted(false)
                .SetImageAcceptedSize(400, 300)
                .SetAdCount(1)
                .setScenarioId("1233211223")
                .SetUseSurfaceView(true)
                .Build();
            ABUNativeAd.LoadNativeAd(adUnit, new NativeAdListener(this));
        }

        public override void ShowAd(Action<bool> callback)
        {
            if (AdManager.Instance.NativeAdSwitch)
            {
                SLog.D("Native Ad", "Native Ad请求展示");
                LoadAd(callback);
            }
        }

        public override void CloseAd()
        {
            NativeAdJavaObject?.Call("destroy");
        }
    }
    
    public class NativeAdListener : ABUFeedAdListener
    {
        private NativeAd _nativeAd;
        public NativeAdListener(NativeAd nativeAd)
        {
            _nativeAd = nativeAd;
        }
        
        public void OnError(int code, string message)
        {
            SLog.E("Native Ad", $"code: {code}\n{message}");
        }

        public void OnFeedAdLoad(AndroidJavaObject list, List<ABUNativeAd> nativeAds)
        {
            var size = list.Call<int>("size");
            SLog.D("Native Ad", "On Native Ad");
            if (size > 0)
            {
                _nativeAd.NativeAdJavaObject = list.Call<AndroidJavaObject>("get", 0);
                _nativeAd.NativeAdInstance = nativeAds[0];
                _nativeAd.NativeAdInstance.SetAdInteractionListener(new NativeAdCallback(_nativeAd));
                _nativeAd.NativeAdInstance.ShowNativeAd(0, 0, 0);
            }
        }
    }

    public class NativeAdCallback : ABUNativeAdInteractionCallback
    {
        private NativeAd _nativeAd;
        public NativeAdCallback(NativeAd nativeAd)
        {
            _nativeAd = nativeAd;
        }

        public void OnAdClicked(int index)
        {
        }

        public void OnAdShow(int index)
        {
            SLog.D("Native Ad", "原生广告展示成功");
        }

        public void OnAdDismiss(int index)
        {
        }

        public void OnRenderFail(string msg, int code)
        {
            SLog.D("Native Ad", "OnRenderFail");
        }

        public void OnRenderSuccess(float width, float height)
        {
            SLog.D("Native Ad", "OnRenderSuccess");
        }
    }
}