package com.bytedance.ad.sdk.mediation;

import android.location.Location;
import android.text.TextUtils;

import com.bytedance.msdk.api.v2.GMLocation;
import com.bytedance.msdk.api.v2.GMPrivacyConfig;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

/**
 * created by jijiachun on 2022/10/24
 */
public class CustomGMPrivacyConfig extends GMPrivacyConfig {
    public boolean limitPersonalAds;
    public boolean canUseLocation;
    public double longitude;
    public double latitude;
    public boolean limitProgrammaticAds;
    public boolean notAdult;
    public boolean canUsePhoneState;
    public boolean canUseWifiState;
    public boolean canUseWriteExternal;
    public boolean canGetAppList;
    public List<String> appList;
    public String devImei;
    public List<String> devImeis;
    public boolean canUseOaid;
    public String devOaid;
    public boolean canUseAndroidId;
    public String androidId;
    public boolean canUseMacAddress;
    public String macAddress;

    public static CustomGMPrivacyConfig creatConfigObject(String configJsonString) {
        if (TextUtils.isEmpty(configJsonString)) {
            return null;
        }
        JSONObject configJson = null;
        try {
            configJson = new JSONObject(configJsonString);
        } catch (JSONException e) {
            e.printStackTrace();
        }
        if (configJson == null) {
            return null;
        }
        CustomGMPrivacyConfig config = new CustomGMPrivacyConfig();
        config.canUseLocation = configJson.optBoolean("canUseLocation");
        config.canUsePhoneState = configJson.optBoolean("canUsePhoneState");
        config.canUseWifiState = configJson.optBoolean("canUseWifiState");
        config.canUseWriteExternal = configJson.optBoolean("canUseWriteExternal");
        config.limitPersonalAds = configJson.optBoolean("limitPersonalAds");
        config.notAdult = configJson.optBoolean("notAdult");
        config.limitProgrammaticAds = configJson.optBoolean("limitProgrammaticAds");
        config.latitude = configJson.optDouble("latitude");
        config.longitude = configJson.optDouble("longitude");
        config.canGetAppList = configJson.optBoolean("CanGetAppList");
        String appList = configJson.optString("AppList");
        if (!TextUtils.isEmpty(appList)) {
            try {
                JSONArray appListJson = new JSONArray(appList);
                List<String> list = new ArrayList<>(appListJson.length());
                for (int i = 0; i < appListJson.length(); i++) {
                    list.add(appListJson.optString(i));
                }
                config.appList = list;
            } catch (Throwable ignored) {
            }
        }
        config.devImei = configJson.optString("DevImei");
        String DevImeisJsonString = configJson.optString("DevImeis");
        if (!TextUtils.isEmpty(DevImeisJsonString)) {
            try {
                JSONArray DevImeisJson = new JSONArray(DevImeisJsonString);
                List<String> list = new ArrayList<>(DevImeisJson.length());
                for (int i = 0; i < DevImeisJson.length(); i++) {
                    list.add(DevImeisJson.optString(i));
                }
                config.devImeis = list;
            } catch (Throwable ignored) {
            }
        }
        config.canUseOaid = configJson.optBoolean("CanUseOaid");
        config.devOaid = configJson.optString("DevOaid");
        config.canUseAndroidId = configJson.optBoolean("CanUseAndroidId");
        config.androidId = configJson.optString("AndroidId");
        config.canUseMacAddress = configJson.optBoolean("CanUseMacAddress");
        config.macAddress = configJson.optString("MacAddress");
        return config;
    }

    public boolean appList() {
        return canGetAppList;
    }


    public String getAndroidId() {
        return androidId;
    }

    public List<String> getAppList() {
        return appList;
    }

    public String getDevImei() {
        return devImei;
    }

    public List<String> getDevImeis() {
        return devImeis;
    }

    public String getDevOaid() {
        return devOaid;
    }

    public String getMacAddress() {
        return macAddress;
    }

    public GMLocation getTTLocation() {
        return new GMLocation(latitude, longitude);
    }

    @Deprecated
    public boolean isAdult() {
        return !notAdult;
    }

    public boolean isCanUseAndroidId() {
        return canUseAndroidId;
    }

    public boolean isCanUseLocation() {
        return canUseLocation;
    }

    public boolean isCanUseMacAddress() {
        return canUseMacAddress;
    }

    public boolean isCanUseOaid() {
        return canUseOaid;
    }

    public boolean isCanUsePhoneState() {
        return canUsePhoneState;
    }

    public boolean isCanUseWifiState() {
        return canUseWifiState;
    }

    public boolean isCanUseWriteExternal() {
        return canUseWriteExternal;
    }

    public boolean isLimitPersonalAds() {
        return limitPersonalAds;
    }

    public boolean isProgrammaticRecommend() {
        return !limitProgrammaticAds;
    }
}
