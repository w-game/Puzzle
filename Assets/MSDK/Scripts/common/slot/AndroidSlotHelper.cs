using System.Collections.Generic;
using UnityEngine;

namespace ByteDance.Union
{
    public class AndroidSlotHelper
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private AndroidJavaObject androidBuilder;


        public AndroidSlotHelper(string builderClassName)
        {
            androidBuilder = new AndroidJavaObject(builderClassName);
        }
#endif

        public AndroidSlotHelper()
        {
        }

        public void SetDownloadType(int downloadType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setDownloadType", downloadType);
#endif
        }


        public void SetMuted(bool muted)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setMuted", muted);
#endif
        }


        public void SetVolume(float volume)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setVolume", volume);
#endif
        }

        public void SetUseSurfaceView(bool useSurfaceView)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setUseSurfaceView", useSurfaceView);
#endif
        }

        public void SetTestSlotId(string testSlotId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setTestSlotId", testSlotId);
#endif
        }

        public void SetBidNotify(bool bidNotify)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setBidNotify", bidNotify);
#endif
        }


        public void SetUserID(string userID)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setUserID", userID);
#endif
        }

        public void SetImageAdSize(int width, int height)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setImageAdSize", width, height);
#endif
        }

        public void SetBannerSize(int bannerSize)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setBannerSize", bannerSize);
#endif
        }


        public void SetAllowShowCloseBtn(bool isAllowShowCloseBtn)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setAllowShowCloseBtn", isAllowShowCloseBtn);
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        public AndroidJavaObject Build(string androidSlotClassName)
        {
            return androidBuilder == null ? null : androidBuilder.Call<AndroidJavaObject>("build");
        }
#endif
        public void SetAdCount(int adCount)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setAdCount", adCount);
#endif
        }

        public void SetRewardName(string rewardName)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setRewardName", rewardName);
#endif
        }

        public void setRewardAmount(int rewardAmount)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setRewardAmount", rewardAmount);
#endif
        }

        public void SetCustomData(Dictionary<string, string> customData)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            var map = new AndroidJavaObject("java.util.HashMap");
            foreach(var pair in customData){
                map.Call<string>("put", pair.Key, pair.Value);
            }
            androidBuilder?.Call<AndroidJavaObject>("setCustomData", map);
#endif
        }

        public void SetOrientation(int orientation)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setOrientation", orientation);
#endif
        }

        public void SetAdStyleType(int adStyleType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setAdStyleType", adStyleType);
#endif
        }

        public void SetSplashShakeButton(bool splashShakeButton)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setSplashShakeButton", splashShakeButton);
#endif
        }

        public void SetSplashButtonType(int splashButtonType)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setSplashButtonType", splashButtonType);
#endif
        }

        public void SetSplashPreLoad(bool splashPreLoad)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setSplashPreLoad", splashPreLoad);
#endif
        }

        public void SetTimeOut(int timeOut)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setTimeOut", timeOut);
#endif
        }

        public void SetScenarioId(string scenarioId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            androidBuilder?.Call<AndroidJavaObject>("setScenarioId", scenarioId);
#endif
        }
    }
}