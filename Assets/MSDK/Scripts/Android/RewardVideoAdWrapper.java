package com.bytedance.ad.sdk.mediation;

import android.app.Activity;

import com.bytedance.msdk.api.AdSlot;
import com.bytedance.msdk.api.GMAdEcpmInfo;
import com.bytedance.msdk.api.TTAdConstant;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAd;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdListener;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdLoadCallback;
import com.bytedance.msdk.api.reward.TTRewardAd;
import com.bytedance.msdk.api.reward.TTRewardedAdListener;
import com.bytedance.msdk.api.reward.TTRewardedAdLoadCallback;
import com.bytedance.msdk.api.v2.ad.fullvideo.GMFullVideoAd;
import com.bytedance.msdk.api.v2.ad.reward.GMRewardAd;
import com.bytedance.msdk.api.v2.slot.GMAdSlotFullVideo;
import com.bytedance.msdk.api.v2.slot.GMAdSlotRewardVideo;

import java.util.Map;

/**
 * created by jijiachun on 2022/10/24
 */
public class RewardVideoAdWrapper {
    TTRewardAd ttRewardAd ;
    GMRewardAd gmRewardAd;

    RewardVideoAdWrapper(Activity context, String adUnitId, boolean newApi) {
        if (newApi) {
            gmRewardAd = new GMRewardAd(context, adUnitId);
        } else {
            ttRewardAd = new TTRewardAd(context, adUnitId);
        }
    }

    public void loadRewardAd(AdSlot adSlot, GMAdSlotRewardVideo gmAdSlotRewardVideo, final TTRewardedAdLoadCallback callback) {
        if (adSlot != null && ttRewardAd != null) {
            ttRewardAd.loadRewardAd(adSlot, callback);
            return;
        }
        if (gmAdSlotRewardVideo != null && gmRewardAd != null) {
            gmRewardAd.loadAd(gmAdSlotRewardVideo, callback);
        }
    }


    @Override
    public int hashCode() {
        if (ttRewardAd != null) {
            return ttRewardAd.hashCode();
        }
        if (gmRewardAd != null) {
            return gmRewardAd.hashCode();
        }
        return super.hashCode();
    }

    public void showRewardAd(Activity activity, Map<TTAdConstant.GroMoreExtraKey, Object> mExtras, final TTRewardedAdListener rewardedAdListener) {
        if (ttRewardAd != null) {
            ttRewardAd.setRewardAdListener(rewardedAdListener);
            ttRewardAd.showRewardAd(activity, mExtras);
            return;
        }
        if (gmRewardAd != null) {
            gmRewardAd.setRewardAdListener(rewardedAdListener);
            gmRewardAd.showRewardAd(activity, mExtras);
        }
    }

    public int getAdNetworkPlatformId() {
        if (ttRewardAd != null) {
            return ttRewardAd.getAdNetworkPlatformId();
        }
        if (gmRewardAd != null) {
            return gmRewardAd.getAdNetworkPlatformId();
        }
        return 0;
    }

    public String getAdNetworkRitId() {
        if (ttRewardAd != null) {
            return ttRewardAd.getAdNetworkRitId();
        }
        if (gmRewardAd != null) {
            return gmRewardAd.getAdNetworkRitId();
        }
        return "";
    }

    public GMAdEcpmInfo getShowEcpm() {
        if (ttRewardAd != null) {
            return ttRewardAd.getShowEcpm();
        }
        if (gmRewardAd != null) {
            return gmRewardAd.getShowEcpm();
        }
        return null;
    }

    public String getPreEcpm() {
        if (ttRewardAd != null) {
            return ttRewardAd.getPreEcpm();
        }
        if (gmRewardAd != null) {
            return gmRewardAd.getPreEcpm();
        }
        return "";
    }
}
