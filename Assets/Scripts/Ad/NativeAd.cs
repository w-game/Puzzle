using System;
using System.Collections.Generic;
using ByteDance.Union;
using Common;
using UnityEngine;

namespace Ad
{
    public class NativeAd : AdBase
    {
        // public AndroidJavaObject NativeAdInstance { get; set; }
        public ABUNativeAd NativeAdInstance { get; set; }

        public override void LoadAd()
        {
            var adUnit = new GMAdSlotNative.Builder()
                .SetCodeId(AdIds.NativeAd)
                .SetMuted(false)   // 设置静音，支持静音的三方adn有效，仅gdt有效
                .SetImageAcceptedSize(300 * 3, 400 * 3) // iOS端尺寸设置需考虑到不同iPhone的scale
                .SetAdCount(1)
                .setScenarioId("1233211223")
                .Build();
            ABUNativeAd.LoadNativeAd(adUnit, new NativeAdListener(this));
        }

        public override void ShowAd(Action<bool> callback)
        {
            if (NativeAdInstance != null)
            {
                NativeAdInstance.SetAdInteractionListener(new NativeAdCallback(this));
                NativeAdInstance.ShowNativeAd(0, 0, 0);
            }
            else
            {
                LoadAd();
            }
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
            // Debug.Log("OnFeedAdLoad size " + size);
            if (size > 0)
            {
                // _nativeAd.NativeAdInstance = list.Call<AndroidJavaObject>("get", 0);
                _nativeAd.NativeAdInstance = nativeAds[0];
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
            _nativeAd.LoadAd();
            SLog.D("Native Ad", "原生广告展示成功");
        }

        public void OnAdDismiss(int index)
        {
        }

        public void OnRenderFail(string msg, int code)
        {
        }

        public void OnRenderSuccess(float width, float height)
        {
        }
    }
}