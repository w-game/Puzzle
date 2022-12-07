package com.bytedance.ad.sdk.mediation;

import android.app.Activity;

import com.bytedance.msdk.api.AdSlot;
import com.bytedance.msdk.api.GMAdEcpmInfo;
import com.bytedance.msdk.api.TTAdConstant;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdListener;
import com.bytedance.msdk.api.interstitial.TTInterstitialAd;
import com.bytedance.msdk.api.interstitial.TTInterstitialAdListener;
import com.bytedance.msdk.api.interstitial.TTInterstitialAdLoadCallback;
import com.bytedance.msdk.api.v2.ad.interstitial.GMInterstitialAd;
import com.bytedance.msdk.api.v2.slot.GMAdSlotInterstitial;

import java.util.Map;

/**
 * created by jijiachun on 2022/10/24
 */
public class InterstitialAdWrapper {
    TTInterstitialAd ttInterstitialAd;
    GMInterstitialAd gmInterstitialAd;

    InterstitialAdWrapper(Activity context, String adUnitId, boolean newApi) {
        if (newApi) {
            gmInterstitialAd = new GMInterstitialAd(context, adUnitId);
        } else {
            ttInterstitialAd = new TTInterstitialAd(context, adUnitId);
        }
    }

    public void loadAd(AdSlot adSlot, GMAdSlotInterstitial gmAdSlotInterstitial, final TTInterstitialAdLoadCallback callback) {
        if (adSlot != null && ttInterstitialAd != null) {
            ttInterstitialAd.loadAd(adSlot, callback);
            return;
        }
        if (gmAdSlotInterstitial != null && gmInterstitialAd != null) {
            gmInterstitialAd.loadAd(gmAdSlotInterstitial, callback);
        }
    }

    public void showAd(Activity activity) {
        if (ttInterstitialAd != null) {
            ttInterstitialAd.showAd(activity);
            return;
        }
        if (gmInterstitialAd != null) {
            gmInterstitialAd.showAd(activity);
        }
    }

    public void setTTAdInterstitialListener(TTInterstitialAdListener listener) {
        if (ttInterstitialAd != null) {
            ttInterstitialAd.setTTAdInterstitialListener(listener);
            return;
        }
        if (gmInterstitialAd != null) {
            gmInterstitialAd.setAdInterstitialListener(listener);
        }
    }

    public int getAdNetworkPlatformId() {
        if (ttInterstitialAd != null) {
            return ttInterstitialAd.getAdNetworkPlatformId();
        }
        if (gmInterstitialAd != null) {
            return gmInterstitialAd.getAdNetworkPlatformId();
        }
        return 0;
    }

    public String getAdNetworkRitId() {
        if (ttInterstitialAd != null) {
            return ttInterstitialAd.getAdNetworkRitId();
        }
        if (gmInterstitialAd != null) {
            return gmInterstitialAd.getAdNetworkRitId();
        }
        return "";
    }

    public GMAdEcpmInfo getShowEcpm() {
        if (ttInterstitialAd != null) {
            return ttInterstitialAd.getShowEcpm();
        }
        if (gmInterstitialAd != null) {
            return gmInterstitialAd.getShowEcpm();
        }
        return null;
    }

    public String getPreEcpm() {
        if (ttInterstitialAd != null) {
            return ttInterstitialAd.getPreEcpm();
        }
        if (gmInterstitialAd != null) {
            return gmInterstitialAd.getPreEcpm();
        }
        return "";
    }

    @Override
    public int hashCode() {
        if (ttInterstitialAd != null) {
            return ttInterstitialAd.hashCode();
        }
        if (gmInterstitialAd != null) {
            return gmInterstitialAd.hashCode();
        }
        return super.hashCode();
    }

}
