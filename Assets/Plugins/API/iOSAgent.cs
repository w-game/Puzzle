// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3
﻿#if UNITY_IPHONE || UNITY_IOS
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;



public class iOSAgent : OmAgent
{
    //******************* SDK Init *******************//

    [DllImport("__Internal")]
    private static extern void OmInitWithAppKey(string appKey);

    [DllImport("__Internal")]
    private static extern void OmInitWithAppKeyAndHost(string appKey, string host);

    [DllImport("__Internal")]
    private static extern bool OmInitialized();

    [DllImport("__Internal")]
    private static extern void OmSetLogEnable(bool logEnable);
	
	[DllImport("__Internal")]
	private static extern void OmSetGDPRConsent(bool consent);

    [DllImport("__Internal")]
    private static extern bool OmGetGDPRConsent();

    [DllImport("__Internal")]
    private static extern void OmSetIap(float count, string currency);
	
	[DllImport("__Internal")]
	private static extern void OmSetUserAge(int age);
	
	[DllImport("__Internal")]
	private static extern void OmSetUserGender(string gender);
	
	[DllImport("__Internal")]
	private static extern void OmSetUSPrivacyLimit(bool value);

    [DllImport("__Internal")]
    private static extern void OmSetAgeRestricted(bool restricted);

    [DllImport("__Internal")]
    private static extern void OmSendAFConversionData(string conversionData);

    [DllImport("__Internal")]
    private static extern void OmSendAFDeepLinkData(string attributionData);

    [DllImport("__Internal")]
    private static extern void OmSetIronSourceMediationMode(bool mode);

    [DllImport("__Internal")]
    private static extern void OmSetCustomTag(string key, string value);

    [DllImport("__Internal")]
    private static extern void OmSetCustomTags(string key, string[] values, int length);

    [DllImport("__Internal")]
    private static extern void OmRemoveCustomTag(string key);

    [DllImport("__Internal")]
    private static extern string OmGetCustomTags();

    [DllImport("__Internal")]
    private static extern void OmSetUserId(string userId);

    [DllImport("__Internal")]
    private static extern string OmGetUserId();

    //******************* Interstitial API *******************//

    [DllImport("__Internal")]
    private static extern bool OmInterstitialIsReady();

    [DllImport("__Internal")]
    private static extern void OmShowInterstitial();

    [DllImport("__Internal")]
    private static extern void OmShowInterstitialWithScene(string scene);


    //******************* RewardedVideo API *******************//

    [DllImport("__Internal")]
    private static extern bool OmRewardedVideoIsReady();

    [DllImport("__Internal")]
    private static extern void OmShowRewardedVideo();

    [DllImport("__Internal")]
    private static extern void OmShowRewardedVideoWithScene(string scene);

    [DllImport("__Internal")]
    private static extern void OmShowRewardedVideoWithExtraParams(string scene, string extraParams);


    //******************* Banner API *******************//
	
    [DllImport("__Internal")]
    private static extern void OmLoadBanner(int bannerType, int position, string placementId);

    [DllImport("__Internal")]
    private static extern void OmDestroyBanner(string placementId);

    [DllImport("__Internal")]
    private static extern void OmDisplayBanner(string placementId);

    [DllImport("__Internal")]
    private static extern void OmHideBanner(string placementId);

    //******************* CrossPromotion *******************//
    
    [DllImport("__Internal")]
    private static extern void OmShowPromotionAd(string scene, int width, int height, float scaleX, float scaleY, float angle);

    [DllImport("__Internal")]
    private static extern void OmHidePromotionAd();

    [DllImport("__Internal")]
    private static extern bool OmIsPromotionAdReady();


    //******************* SplashAd *******************//

    [DllImport("__Internal")]
    private static extern void OmLoadSplashAd(string placementId);

    [DllImport("__Internal")]
    private static extern bool OmIsSplashAdReady(string placementId);

    [DllImport("__Internal")]
    private static extern void OmShowSplashAd(string placementId);


    #region OmAgent implementation

    public void init(string appkey)
    {
        OmUtils.printLogI("init with key: " + appkey);
        OmInitWithAppKey(appkey);
    }

    public void init(string appkey, string host)
    {
        OmUtils.printLogI("init with key: " + appkey);
        OmInitWithAppKeyAndHost(appkey,host);
    }
    public void init(string appkey, string host, string channel)
    {
        OmUtils.printLogI("init with key: " + appkey);
        OmInitWithAppKeyAndHost(appkey,host);
    }

    public bool isInitialized()
    {
        return OmInitialized();
    }

    public void debug(bool isDebug)
    {
        OmUtils.isDebug = isDebug;
        OmSetLogEnable(isDebug);
    }

    public void setIap(float count, string currency)
    {
        OmUtils.printLogI("set iap");
        OmSetIap(count, currency);
    }

    public void setGDPRConsent(bool consent)
	{
		OmUtils.printLogI("set GDPR consent "+ consent);
		OmSetGDPRConsent(consent);
	}

    public bool getGDPRConsent()
    {
        return OmGetGDPRConsent();
    }

    public void setAgeRestricted(bool restricted)
	{
        OmUtils.printLogI("set user age restricted " + restricted);
        OmSetAgeRestricted(restricted);
    }
	
    public void setUserAge(int age) 
	{
		OmUtils.printLogI("set user age "+ age);
		OmSetUserAge(age);
	}
	
    public void setUserGender(string gender)
	{
		OmUtils.printLogI("set user gender "+ gender);
		OmSetUserGender(gender);
	}
	
    public void setUSPrivacyLimit(bool value) 
	{
		OmUtils.printLogI("set user privacy limit "+ value);
		OmSetUSPrivacyLimit(value);
	}

    public void sendAFConversionData(string conversionData)
    {
        OmUtils.printLogI("send conversionData" + conversionData);
        OmSendAFConversionData(conversionData);
    }

    public void sendAFDeepLinkData(string conversionData)
    {
        OmUtils.printLogI("send attributionData" + conversionData);
        OmSendAFDeepLinkData(conversionData);
    }

    public void setIronSourceMediationMode(bool mode){
        OmSetIronSourceMediationMode(mode);
    }

    public void setCustomTag(string key, string value){
        OmUtils.printLogI("set custom tag key" + key + "value" + value);
        OmSetCustomTag(key, value);
    }

    public void setCustomTags(string key, string[] values){
        OmUtils.printLogI("set custom tag key" + key + "value" + values);
        OmSetCustomTags(key, values, values.Length);
    }

    public void removeCustomTag(string key)
    {
        OmUtils.printLogI("remove custom tag");
        OmRemoveCustomTag(key);
    }

    public string getCustomTags()
    {
        OmUtils.printLogI("get custom tag");
        string tags = OmGetCustomTags();
        return tags;
    }

    public void setUserId(string userId)
    {
        OmUtils.printLogI("set user id");
        OmSetUserId(userId);
    }


    public string getUserId()
    {
        OmUtils.printLogI("get user id");
        string userId = OmGetUserId();
        return userId;
    }

    public bool isInterstitialReady()
    {
        bool isReady = false;

        OmUtils.printLogI("isInterstitialReady");
        isReady = OmInterstitialIsReady();

        return isReady;
    }

    public void showInterstitial()
    {
        OmUtils.printLogI("show interstitial");
        if (OmInterstitialIsReady())
        {
            OmShowInterstitial();
        }
    }

    public void showInterstitial(string scene)
    {

        OmUtils.printLogI("show interstitial");

        if (scene == null || scene.Length == 0)
        {
            if (OmInterstitialIsReady())
            {
                OmShowInterstitialWithScene("");
            }
        }
        else
        {
            if (OmInterstitialIsReady())
            {
                OmShowInterstitialWithScene(scene);
            }
        }
    }

  

    public bool isRewardedVideoReady()
    {
        bool isReady = false;
        OmUtils.printLogI("isRewardedVideoReady");
        isReady = OmRewardedVideoIsReady();
        return isReady;
    }

    public void showRewardedVideo()
    {
        OmUtils.printLogI("show rewardedVideo");
        if (OmRewardedVideoIsReady())
        {
            OmShowRewardedVideo();
        }
    }

    public void showRewardedVideo(string scene)
    {

        OmUtils.printLogI("show rewardedVideo with scene");

        if (scene == null || scene.Length == 0)
        {
            if (OmRewardedVideoIsReady())
            {
                OmShowRewardedVideoWithScene("");
            }
        }
        else
        {
            if (OmRewardedVideoIsReady())
            {
                OmShowRewardedVideoWithScene(scene);
            }
        }
    }

    public void showRewardedVideo(string scene, string extraParams)
    {

        OmUtils.printLogI("show rewardedVideo with extraParams");

        if (scene == null || scene.Length == 0)
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (OmRewardedVideoIsReady())
                {
                    OmShowRewardedVideoWithExtraParams("", "");
                }
            }
            else
            {
                if (OmRewardedVideoIsReady())
                {
                    OmShowRewardedVideoWithExtraParams("", extraParams);
                }
            }
        }
        else
        {
            if (extraParams == null || extraParams.Length == 0)
            {
                if (OmRewardedVideoIsReady())
                {
                    OmShowRewardedVideoWithExtraParams(scene, "");
                }
            }
            else
            {
                if (OmRewardedVideoIsReady())
                {
                    OmShowRewardedVideoWithExtraParams(scene, extraParams);
                }
            }
        }
    }

    //****************** CrossPromotion ********************//
    public void showPromotionAd(int width, int height, float scaleX, float scaleY, float angle) {
        OmShowPromotionAd("",width,height,scaleX,scaleY,angle);
    }

    public void showPromotionAd(string scene, int width, int height, float scaleX, float scaleY, float angle) {
        OmShowPromotionAd(scene,width,height,scaleX,scaleY,angle);
    }

    public void hidePromotionAd() {
        OmHidePromotionAd();
    }

    public bool isPromotionAdReady() {
        return OmIsPromotionAdReady();
    }
	
	//****************** Banner API ********************//
	public void loadBanner(string placementId,AdSize size,BannerPostion position) {
		OmLoadBanner((int)size,(int)position,placementId);
	}
	
	public void destroyBanner(string placementId) {
		OmDestroyBanner(placementId);
	}
	
	public void displayBanner(string placementId) {
		OmDisplayBanner(placementId);
	}
	
	public void hideBanner(string placementId) {
		OmHideBanner(placementId);	
	}

    //****************** SplashAd API ********************//
    public void loadSplashAd(string placementId)
    {
        OmLoadSplashAd(placementId);
    }

    public bool isSplashAdReady(string placementId)
    {
        return OmIsSplashAdReady(placementId);
    }

    public void showSplashAd(string placementId)
    {
        OmShowSplashAd(placementId);
    }

    #endregion
}

#endif
