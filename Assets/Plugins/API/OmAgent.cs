// Copyright 2020 ADTIMING TECHNOLOGY COMPANY LIMITED
// Licensed under the GNU Lesser General Public License Version 3
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface OmAgent{
    //******************* SDK Init *******************//ß
    void init(string appkey);
    void init(string appkey, string host);
    void init(string appkey, string host, string channel);
    bool isInitialized();
    void setIap(float count,string currency);
    void debug(bool isDebug);
    void setGDPRConsent(bool consent);
    void setAgeRestricted(bool restricted);
    void setUserAge(int age);
    void setUserGender(string gender);
    void setUSPrivacyLimit(bool value);
    bool getGDPRConsent();
    void sendAFConversionData(string conversionData);
    void sendAFDeepLinkData(string conversionData);
    void setIronSourceMediationMode(bool mode);

    //******************* RewardedVideo API *******************//
    void showRewardedVideo();
    void showRewardedVideo(string scene);
    void showRewardedVideo(string scene, string extraParams);
    bool isRewardedVideoReady();
    //******************* Interstitial API *******************//
    void showInterstitial();
    void showInterstitial(string scene);
    bool isInterstitialReady();
    //****************** Banner API ********************//
    void loadBanner(string placementId, AdSize size, BannerPostion position);
    void destroyBanner(string placementId);
    void displayBanner(string placementId);
    void hideBanner(string placementId);
    //****************** CrossPromotion API ********************//
    void showPromotionAd(int width, int height, float scaleX, float scaleY, float angle);
    void showPromotionAd(string scene, int width, int height, float scaleX, float scaleY, float angle);
    void hidePromotionAd();
    bool isPromotionAdReady();
    //****************** SplashAd API ********************//
    void loadSplashAd(string placementId);
    bool isSplashAdReady(string placementId);
    void showSplashAd(string placementId);

    void setCustomTag(string key, string value);
    void setCustomTags(string key, string[] values);
    void removeCustomTag(string key);
    string getCustomTags();
    void setUserId(string userId);
    string getUserId();
}

public enum AdSize
{
    BANNER = 0,
    MEDIUM_RECTANGLE = 1,
    LEADERBOARD = 2,
    SMART = 3
}

public enum BannerPostion
{
    BOTTOM = 0,
    TOP = 1
}
