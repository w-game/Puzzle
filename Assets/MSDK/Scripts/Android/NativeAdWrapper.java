package com.bytedance.ad.sdk.mediation;

import android.app.Activity;
import android.content.Context;
import android.support.annotation.NonNull;

import com.bytedance.msdk.api.AdError;
import com.bytedance.msdk.api.AdSlot;
import com.bytedance.msdk.api.GMAdEcpmInfo;
import com.bytedance.msdk.api.TTAdConstant;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAd;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdListener;
import com.bytedance.msdk.api.fullVideo.TTFullVideoAdLoadCallback;
import com.bytedance.msdk.api.nativeAd.TTNativeAd;
import com.bytedance.msdk.api.nativeAd.TTNativeAdLoadCallback;
import com.bytedance.msdk.api.nativeAd.TTUnifiedNativeAd;
import com.bytedance.msdk.api.v2.ad.fullvideo.GMFullVideoAd;
import com.bytedance.msdk.api.v2.ad.nativeAd.GMNativeAd;
import com.bytedance.msdk.api.v2.ad.nativeAd.GMNativeAdLoadCallback;
import com.bytedance.msdk.api.v2.ad.nativeAd.GMUnifiedNativeAd;
import com.bytedance.msdk.api.v2.slot.GMAdSlotFullVideo;
import com.bytedance.msdk.api.v2.slot.GMAdSlotNative;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

/**
 * created by jijiachun on 2022/10/24
 */
public class NativeAdWrapper {
    TTUnifiedNativeAd ttUnifiedNativeAd;
    GMUnifiedNativeAd gmUnifiedNativeAd;

    NativeAdWrapper(Context context, String adUnitId, boolean newApi) {
        if (newApi) {
            gmUnifiedNativeAd = new GMUnifiedNativeAd(context, adUnitId);
        } else {
            ttUnifiedNativeAd = new TTUnifiedNativeAd(context, adUnitId);
        }
    }

    public void loadAd(AdSlot adSlot, GMAdSlotNative gmAdSlotNative, GMNativeAdLoadCallback callback) {
        if (adSlot != null && ttUnifiedNativeAd != null) {
            ttUnifiedNativeAd.loadAd(adSlot, new TTNativeAdLoadCallback() {
                @Override
                public void onAdLoaded(@NonNull List<TTNativeAd> list) {
                    if (callback != null) {
                        List<GMNativeAd> result = new ArrayList<>(list);
                        callback.onAdLoaded(result);
                    }

                }

                @Override
                public void onAdLoadedFial(@NonNull AdError adError) {
                    if (callback != null) {
                        callback.onAdLoadedFail(adError);
                    }
                }
            });
            return;
        }
        if (gmAdSlotNative != null && gmUnifiedNativeAd != null) {
            gmUnifiedNativeAd.loadAd(gmAdSlotNative, callback);
        }
    }


    @Override
    public int hashCode() {
        if (ttUnifiedNativeAd != null) {
            return ttUnifiedNativeAd.hashCode();
        }
        if (gmUnifiedNativeAd != null) {
            return gmUnifiedNativeAd.hashCode();
        }
        return super.hashCode();
    }
}
