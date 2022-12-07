package com.bytedance.ad.sdk.mediation;

import android.app.Activity;
import android.view.ViewGroup;

import com.bytedance.msdk.api.AdSlot;
import com.bytedance.msdk.api.GMAdEcpmInfo;
import com.bytedance.msdk.api.TTNetworkRequestInfo;
import com.bytedance.msdk.api.interstitial.TTInterstitialAd;
import com.bytedance.msdk.api.interstitial.TTInterstitialAdListener;
import com.bytedance.msdk.api.interstitial.TTInterstitialAdLoadCallback;
import com.bytedance.msdk.api.splash.TTSplashAd;
import com.bytedance.msdk.api.splash.TTSplashAdListener;
import com.bytedance.msdk.api.splash.TTSplashAdLoadCallback;
import com.bytedance.msdk.api.v2.GMNetworkRequestInfo;
import com.bytedance.msdk.api.v2.ad.interstitial.GMInterstitialAd;
import com.bytedance.msdk.api.v2.ad.splash.GMSplashAd;
import com.bytedance.msdk.api.v2.slot.GMAdSlotInterstitial;
import com.bytedance.msdk.api.v2.slot.GMAdSlotSplash;

/**
 * created by jijiachun on 2022/10/24
 */
public class SplashAdWrapper {
    TTSplashAd ttSplashAd;
    GMSplashAd gmSplashAd;

    SplashAdWrapper(Activity context, String adUnitId, boolean newApi) {
        if (newApi) {
            gmSplashAd = new GMSplashAd(context, adUnitId);
        } else {
            ttSplashAd = new TTSplashAd(context, adUnitId);
        }
    }

    public void loadAd(AdSlot adSlot, GMAdSlotSplash gmAdSlotSplash, GMNetworkRequestInfo ttNetworkRequestInfo, TTSplashAdLoadCallback callback, int timeOut) {
        if (adSlot != null && ttSplashAd != null) {
            ttSplashAd.loadAd(adSlot, ttNetworkRequestInfo, callback, timeOut);
            return;
        }
        if (gmAdSlotSplash != null && gmSplashAd != null) {
            gmSplashAd.loadAd(gmAdSlotSplash, ttNetworkRequestInfo, callback);
        }
    }

    public void showAd(ViewGroup viewGroup) {
        if (ttSplashAd != null) {
            ttSplashAd.showAd(viewGroup);
            return;
        }
        if (gmSplashAd != null) {
            gmSplashAd.showAd(viewGroup);
        }
    }

    public void setTTAdSplashListener(TTSplashAdListener listener) {
        if (ttSplashAd != null) {
            ttSplashAd.setTTAdSplashListener(listener);
            return;
        }
        if (gmSplashAd != null) {
            gmSplashAd.setAdSplashListener(listener);
        }
    }

    public int getAdNetworkPlatformId() {
        if (ttSplashAd != null) {
            return ttSplashAd.getAdNetworkPlatformId();
        }
        if (gmSplashAd != null) {
            return gmSplashAd.getAdNetworkPlatformId();
        }
        return 0;
    }

    public String getAdNetworkRitId() {
        if (ttSplashAd != null) {
            return ttSplashAd.getAdNetworkRitId();
        }
        if (gmSplashAd != null) {
            return gmSplashAd.getAdNetworkRitId();
        }
        return "";
    }

    public GMAdEcpmInfo getShowEcpm() {
        if (ttSplashAd != null) {
            return ttSplashAd.getShowEcpm();
        }
        if (gmSplashAd != null) {
            return gmSplashAd.getShowEcpm();
        }
        return null;
    }

    public String getPreEcpm() {
        if (ttSplashAd != null) {
            return ttSplashAd.getPreEcpm();
        }
        if (gmSplashAd != null) {
            return gmSplashAd.getPreEcpm();
        }
        return "";
    }

    @Override
    public int hashCode() {
        if (ttSplashAd != null) {
            return ttSplashAd.hashCode();
        }
        if (gmSplashAd != null) {
            return gmSplashAd.hashCode();
        }
        return super.hashCode();
    }

}
