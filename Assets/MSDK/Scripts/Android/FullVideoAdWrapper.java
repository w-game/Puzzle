package com.bytedance.ad.sdk.mediation;

import android.app.Activity;
import android.support.annotation.NonNull;

import com.bytedance.msdk.api.AdError;
import com.bytedance.msdk.api.AdSlot;
import com.bytedance.msdk.api.GMAdEcpmInfo;
import com.bytedance.msdk.api.TTAdConstant;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAd;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdListener;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdLoadCallback;
import com.bytedance.msdk.api.reward.RewardItem;
import com.bytedance.msdk.api.v2.ad.fullvideo.GMFullVideoAd;
import com.bytedance.msdk.api.v2.ad.fullvideo.GMFullVideoAdListener;
import com.bytedance.msdk.api.v2.ad.fullvideo.GMFullVideoAdLoadCallback;
import com.bytedance.msdk.api.v2.slot.GMAdSlotFullVideo;

import java.util.Map;
import java.util.Objects;

import bykvm_19do.bykvm_19do.bykvm_if122.bykvm_19do.bykvm_19do.g;

/**
 * created by jijiachun on 2022/10/24
 */
public class FullVideoAdWrapper {
    TTFullVideoAd ttFullVideoAd;
    GMFullVideoAd gmFullVideoAd;

    FullVideoAdWrapper(Activity context, String adUnitId, boolean newApi) {
        if (newApi) {
            gmFullVideoAd = new GMFullVideoAd(context, adUnitId);
        } else {
            ttFullVideoAd = new TTFullVideoAd(context, adUnitId);
        }
    }

    public void loadFullAd(AdSlot adSlot, GMAdSlotFullVideo gmAdSlotFullVideo, final TTFullVideoAdLoadCallback callback) {
        if (adSlot != null && ttFullVideoAd != null) {
            ttFullVideoAd.loadFullAd(adSlot, callback);
            return;
        }
        if (gmAdSlotFullVideo != null && gmFullVideoAd != null) {
            gmFullVideoAd.loadAd(gmAdSlotFullVideo, callback);
        }
    }


    @Override
    public int hashCode() {
        if (ttFullVideoAd != null) {
            return ttFullVideoAd.hashCode();
        }
        if (gmFullVideoAd != null) {
            return gmFullVideoAd.hashCode();
        }
        return super.hashCode();
    }

    public void showFullAd(Activity activity, Map<TTAdConstant.GroMoreExtraKey, Object> mExtras, final TTFullVideoAdListener ttFullVideoAdListener) {
        if (ttFullVideoAd != null) {
            ttFullVideoAd.setFullVideoAdListener(ttFullVideoAdListener);
            ttFullVideoAd.showFullAd(activity, mExtras);
            return;
        }
        if (gmFullVideoAd != null) {
            gmFullVideoAd.setFullVideoAdListener(ttFullVideoAdListener);
            gmFullVideoAd.showFullAd(activity, mExtras);
        }
    }

    public int getAdNetworkPlatformId() {
        if (ttFullVideoAd != null) {
            return ttFullVideoAd.getAdNetworkPlatformId();
        }
        if (gmFullVideoAd != null) {
            return gmFullVideoAd.getAdNetworkPlatformId();
        }
        return 0;
    }

    public String getAdNetworkRitId() {
        if (ttFullVideoAd != null) {
            return ttFullVideoAd.getAdNetworkRitId();
        }
        if (gmFullVideoAd != null) {
            return gmFullVideoAd.getAdNetworkRitId();
        }
        return "";
    }

    public GMAdEcpmInfo getShowEcpm() {
        if (ttFullVideoAd != null) {
            return ttFullVideoAd.getShowEcpm();
        }
        if (gmFullVideoAd != null) {
            return gmFullVideoAd.getShowEcpm();
        }
        return null;
    }

    public String getPreEcpm() {
        if (ttFullVideoAd != null) {
            return ttFullVideoAd.getPreEcpm();
        }
        if (gmFullVideoAd != null) {
            return gmFullVideoAd.getPreEcpm();
        }
        return "";
    }
}
