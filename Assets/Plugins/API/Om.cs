// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3
 using System;
using UnityEngine;

public class Om : OmAgent
{
    private OmAgent _platformAgent;
    private static Om _instance;
    private const string UNITY_PLUGIN_VERSION = "2.1.3";


    private Om()
    {
#if (UNITY_IPHONE || UNITY_IOS)
        _platformAgent = new iOSAgent();
#elif UNITY_ANDROID
        _platformAgent = new AndroidAgent();
#endif
        var type = typeof(OmEvents);
        var mgr = new GameObject("OmEvents", type).GetComponent<OmEvents>();
    }

    #region OmAgent implementation
    public static Om Agent
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Om();
            }
            return _instance;
        }
    }

    public void init(string appkey)
    {
        _platformAgent.init(appkey);
    }

    public void init(string appkey, string host)
    {
        _platformAgent.init(appkey,host);
    }

    public void init(string appkey, string host, string channel)
    {
        _platformAgent.init(appkey, host, channel);
    }

    public bool isInitialized()
    {
        return _platformAgent.isInitialized();
    }

    public void setIap(float count, string currency) {
        _platformAgent.setIap(count,currency);
    }

    public void setGDPRConsent(bool consent)
    {
        _platformAgent.setGDPRConsent(consent);
    }

    public void setAgeRestricted(bool restricted)
    {
        _platformAgent.setAgeRestricted(restricted);
    }

    public void setUserAge(int age)
    {
        _platformAgent.setUserAge(age);
    }

    public void setUserGender(string gender)
    {
        _platformAgent.setUserGender(gender);
    }

    public void setUSPrivacyLimit(bool value)
    {
        _platformAgent.setUSPrivacyLimit(value);
    }

    public bool getGDPRConsent()
    {
        return _platformAgent.getGDPRConsent();
    }

    public void sendAFConversionData(string conversionData) 
    {
        _platformAgent.sendAFConversionData(conversionData);
    }

    public void sendAFDeepLinkData(string conversionData) 
    {
        _platformAgent.sendAFDeepLinkData(conversionData);

    }

    public void setIronSourceMediationMode(bool mode) 
    {
        _platformAgent.setIronSourceMediationMode(mode);
    }

    public void showRewardedVideo()
    {
        _platformAgent.showRewardedVideo();
    }

    public void showRewardedVideo(string scene)
    {
        _platformAgent.showRewardedVideo(scene);
    }

    public void showRewardedVideo(string scene, string extraParams)
    {
        _platformAgent.showRewardedVideo(scene, extraParams);
    }

    public bool isRewardedVideoReady()
    {
        return _platformAgent.isRewardedVideoReady();
    }

    public void showInterstitial()
    {
        _platformAgent.showInterstitial();
    }

    public void showInterstitial(string scene)
    {
        _platformAgent.showInterstitial(scene);
    }

    public bool isInterstitialReady()
    {
        return _platformAgent.isInterstitialReady();
    }

    public string getVersion()
    {
        return UNITY_PLUGIN_VERSION;
    }

    public void debug(bool isDebug)
    {
        _platformAgent.debug(isDebug);
    }

    public void loadBanner(string placementId, AdSize size, BannerPostion position)
    {
        _platformAgent.loadBanner(placementId, size, position);
    }

    public void destroyBanner(string placementId)
    {
        _platformAgent.destroyBanner(placementId);
    }

    public void displayBanner(string placementId)
    {
        _platformAgent.displayBanner(placementId);
    }

    public void hideBanner(string placementId)
    {
        _platformAgent.hideBanner(placementId);
    }

    public void showPromotionAd(int widht, int height, float scaleX, float scaleY, float angle)
    {
        _platformAgent.showPromotionAd(widht, height, scaleX, scaleY, angle);
    }

    public void showPromotionAd(string scene, int widht, int height, float scaleX, float scaleY, float angle)
    {
        _platformAgent.showPromotionAd(scene, widht, height, scaleX, scaleY, angle);
    }

    public void hidePromotionAd()
    {
        _platformAgent.hidePromotionAd();
    }

    public bool isPromotionAdReady()
    {
        return _platformAgent.isPromotionAdReady();
    }

    public void loadSplashAd(string placementId) {
        _platformAgent.loadSplashAd(placementId);
    }

    public bool isSplashAdReady(string placementId) {
        return _platformAgent.isSplashAdReady(placementId);
    }

    public void showSplashAd(string placementId) {
        _platformAgent.showSplashAd(placementId);
    }

    public void setCustomTag(string key, string value)
    {
        _platformAgent.setCustomTag(key, value);
    }

    public void setCustomTags(string key, string[] values)
    {
        _platformAgent.setCustomTags(key, values);
    }

    public void removeCustomTag(string key)
    {
        _platformAgent.removeCustomTag(key);
    }

    public string getCustomTags()
    {
        return _platformAgent.getCustomTags();
    }

    public void setUserId(string userId)
    {
        _platformAgent.setUserId(userId);
    }

    public string getUserId()
    {
        return _platformAgent.getUserId();
    }

    #endregion
}