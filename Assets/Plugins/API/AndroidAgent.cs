// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3
#if UNITY_ANDROID

using System;
using UnityEngine;

public class AndroidAgent : OmAgent
{
    private static String BASE_PATH = "com.openmediation.sdk";
    AndroidJavaClass mOm = null;

    public AndroidAgent()
    {
        try
        {
            mOm = new AndroidJavaClass(BASE_PATH + ".unity.OmBridge");
        }
        catch (Exception e)
        {
            OmUtils.printLogE(BASE_PATH + ".unity.OmBridge not found" + e.Message);
        }
    }

    public void debug(bool isDebug)
    {
        OmUtils.isDebug = isDebug;
        if (mOm != null)
        {
            mOm.CallStatic("Debug", isDebug);
        }
    }

    public void init(string appkey)
    {
        OmUtils.printLogE("init : " + appkey);
        if (mOm != null)
        {
            mOm.CallStatic("init", appkey);
        }
    }

    public void init(string appkey, string host)
    {
        OmUtils.printLogE("init : " + appkey+" host : " + host);
        if (mOm != null)
        {
            mOm.CallStatic("init", appkey, host);
        }
    }

    public void init(string appkey, string host, string channel)
    {
        OmUtils.printLogE("init : " + appkey + " host : " + host + " channel : "+channel);
        if (mOm != null)
        {
            mOm.CallStatic("init", appkey, host, channel);
        }
    }

    public bool isInitialized()
    {
        bool isInit = false;
        if (mOm != null)
        {
            isInit = mOm.CallStatic<bool>("isInit");
        }
        return isInit;
    }

    public void sendAFConversionData(string conversionData)
    {
        if (mOm != null)
        {
            mOm.CallStatic("sendAFConversionData", conversionData);
        }
    }

    public void sendAFDeepLinkData(string conversionData)
    {
        if (mOm != null)
        {
            mOm.CallStatic("sendAFDeepLinkData", conversionData);
        }
    }

    public void setIronSourceMediationMode(bool mode)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setIronSourceMediationMode", mode);
        }
    }

    public void setIap(float count, string currency)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setIAP", count, currency);
        }
    }

    public void setGDPRConsent(bool consent)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setGDPRConsent", consent);
        }
    }

    public void setAgeRestricted(bool restricted)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setAgeRestricted", restricted);
        }
    }

    public void setUserAge(int age)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setUserAge", age);
        }
    }

    public void setUserGender(string gender)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setUserGender", gender);
        }
    }

    public void setUSPrivacyLimit(bool value)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setUSPrivacyLimit", value);
        }
    }

    public bool getGDPRConsent() {
        bool isGDPR = false;
        if (mOm != null) {
            isGDPR = mOm.CallStatic<bool>("getGDPRConsent");
        }
        return isGDPR;
    }

    public bool isInterstitialReady()
    {
        bool isReady = false;
        if (mOm != null)
        {
            isReady = mOm.CallStatic<bool>("isInterstitialReady");
        }
        return isReady;
    }

    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        if (mOm != null)
        {
            isReady = mOm.CallStatic<bool>("isRewardedVideoReady");
        }
        return isReady;
    }


    public void showInterstitial()
    {
        OmUtils.printLogE("showInterstitial");
        if (mOm != null)
        {
            mOm.CallStatic("showInterstitial");
        }
    }

    public void showInterstitial(string scene)
    {
        if (mOm != null)
        {
            mOm.CallStatic("showInterstitial", scene);
        }
    }

    public void showRewardedVideo()
    {
        if (mOm != null)
        {
            mOm.CallStatic("showRewardedVideo");
        }
    }

    public void showRewardedVideo(string scene)
    {
        if (mOm != null)
        {
            mOm.CallStatic("showRewardedVideo", scene);
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setRewardedVideoExtId", scene, extraParams);
            mOm.CallStatic("showRewardedVideo", scene);
        }
    }

    public void loadBanner(string placementId, AdSize size, BannerPostion position)
    {
        if (mOm != null)
        {
            mOm.CallStatic("loadBanner", placementId, (int)size, (int)position);
        }
    }

    public void destroyBanner(string placementId)
    {
        if (mOm != null)
        {
            mOm.CallStatic("destroyBanner", placementId);
        }
    }

    public void displayBanner(string placementId)
    {
        if (mOm != null)
        {
            mOm.CallStatic("displayBanner", placementId);
        }
    }

    public void hideBanner(string placementId)
    {
        if (mOm != null)
        {
            mOm.CallStatic("hideBanner", placementId);
        }
    }

    public void showPromotionAd(int width, int height, float scaleX, float scaleY, float angle)
    {
        if (mOm != null)
        {
            mOm.CallStatic("showPromotionAd", width, height, scaleX, scaleY, angle);
        }
    }

    public void showPromotionAd(string scene, int width, int height, float scaleX, float scaleY, float angle)
    {
        if (mOm != null)
        {
            mOm.CallStatic("showPromotionAd", scene, width, height, scaleX, scaleY, angle);
        }
    }

    public void hidePromotionAd()
    {
        if (mOm != null)
        {
            mOm.CallStatic("hidePromotionAd");
        }
    }

    public bool isPromotionAdReady()
    {
        bool isReady = false;
        if (mOm != null)
        {
            isReady = mOm.CallStatic<bool>("isPromotionAdReady");
        }
        return isReady;
    }

    public void loadSplashAd(string placementId) {
        if (mOm != null)
        {
            mOm.CallStatic("loadSplashAd", placementId);
        }
    }

    public bool isSplashAdReady(string placementId) {
        bool isReady = false;
        if (mOm != null)
        {
            isReady = mOm.CallStatic<bool>("isSplashAdReady", placementId);
        }
        return isReady;
    }

    public void showSplashAd(string placementId) {
        if (mOm != null)
        {
            mOm.CallStatic("showSplashAd", placementId);
        }
    }

    public void setCustomTag(string key, string value)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setCustomTag", key, value);
        }
    }

    public void setCustomTags(string key, string[] values)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setCustomTag", key, values);
        }
    }

    public void removeCustomTag(string key)
    {
        if (mOm != null)
        {
            mOm.CallStatic("removeCustomTag", key);
        }
    }

    public string getCustomTags()
    {
        string tags = null;
        if (mOm != null)
        {
            tags = mOm.CallStatic<string>("getCustomTags");
        }
        return tags;
    }

    public void setUserId(string userId)
    {
        if (mOm != null)
        {
            mOm.CallStatic("setUserId", userId);
        }
    }

    public string getUserId()
    {
        string userId = null;
        if (mOm != null)
        {
            userId = mOm.CallStatic<string>("getUserId");
        }
        return userId;
    }

}

#endif