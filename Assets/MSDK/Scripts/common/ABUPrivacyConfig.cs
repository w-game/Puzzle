﻿//------------------------------------------------------------------------------
// Copyright (c) 2018-2019 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

using System;

namespace ByteDance.Union
{
    /// <summary>
    /// The slot of a advertisement.
    /// </summary>
    using UnityEngine;
    using System.Collections.Generic;

    [Serializable]
    public sealed class ABUPrivacyConfig
    {
        public bool limitPersonalAds; // 双端               //是否限制个性化广告,默认为不限制。官方维护版本中只适用于CSJ、Ks、Sigmob、百度、GDT。
        public bool canUseLocation; // 双端               //是否在adn中使用位置,官方维护版本中只适用于CSJ。  
        public double longitude; // 双端               //当canUseLocation为false时，使用传入的经度的值。默认值是0.0。官方维护版本中只适用于CSJ。
        public double latitude; // 双端               //当canUseLocation为false时，使用传入的纬度的值。默认值是0.0。官方维护版本中只适用于CSJ。
        public bool limitProgrammaticAds; // 双端               //是否限制程序化广告，默认为不限制。官方维护版本中只适用于CSJ、Ks、Sigmob、百度、GDT。
        public bool notAdult; // 双端               //是成人或者儿童，true-儿童, false-成人，默认为true(成人)。官方维护版本中只适用于sigmob。
        public bool limitCAID; // 仅对iOS生效         //是否禁止CAID，默认为false。官方维护版本中只适用于百度。
        public bool canUsePhoneState; // 仅对Android生效
        public bool canUseWifiState; // 仅对Android生效
        public bool canUseWriteExternal; // 仅对Android生效
        public bool CanGetAppList = true; // 仅对Android生效
        /// <summary>
        /// Json String
        /// </summary>
        public string AppList; // 仅对Android生效
        public string DevImei; // 仅对Android生效
       /// <summary>
       ///json string
       /// </summary>
        public string DevImeis; // 仅对Android生效   
        public bool CanUseOaid = true; // 仅对Android生效
        public string DevOaid; // 仅对Android生效
        public bool CanUseAndroidId = true; // 仅对Android生效  
        public string AndroidId; // 仅对Android生效
        public bool CanUseMacAddress = true; // 仅对Android生效   
        public string MacAddress; // 仅对Android生效

        private static ABUPrivacyConfig _privacyConfig = null;

        public static ABUPrivacyConfig CreateSharedAdConfig()
        {
            if (_privacyConfig == null)
            {
                _privacyConfig = new ABUPrivacyConfig();
            }

            return _privacyConfig;
        }

        public AndroidJavaObject getCurrentAndroidObject()
        {
            return null;
        }

        public ABUPrivacyConfig getPrivacyConfig()
        {
            return null;
        }

        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        /// <summary>
        /// The builder used to build an Ad slot.
        /// </summary>
        public class Builder
        {
            /// <summary>
            /// Sets limit CAIDID.
            /// </summary>
            public Builder SetLimitCAID(bool limitCAID)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().limitCAID = limitCAID;
                return this;
            }

            /// <summary>
            /// Sets limit PersonalAds.
            /// </summary>
            public Builder SetLimitPersonalAds(bool limitPersonalAds)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().limitPersonalAds = limitPersonalAds;
                return this;
            }

            /// <summary>
            /// Sets canUseLocation.
            /// </summary>
            public Builder SetCanUseLocation(bool canUseLocation)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().canUseLocation = canUseLocation;
                return this;
            }

            /// <summary>
            /// Sets longitude.
            /// </summary>
            public Builder SetLongitude(double longitude)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().longitude = longitude;
                return this;
            }

            /// <summary>
            /// Sets latitude.
            /// </summary>
            public Builder SetLatitude(double latitude)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().latitude = latitude;
                return this;
            }

            /// <summary>
            /// Sets limitProgrammaticAds.
            /// </summary>
            public Builder SetLimitProgrammaticAds(bool limitProgrammaticAds)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().limitProgrammaticAds = limitProgrammaticAds;
                return this;
            }

            /// <summary>
            /// Sets notAdult.
            /// </summary>
            public Builder SetNotAdult(bool notAdult)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().notAdult = notAdult;
                return this;
            }

            /// <summary>
            /// Sets canUsePhoneState.
            /// </summary>
            public Builder SetCanUsePhoneState(bool canUsePhoneState)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().canUsePhoneState = canUsePhoneState;
                return this;
            }

            /// <summary>
            /// Sets canUseWifiState.
            /// </summary>
            public Builder SetCanUseWifiState(bool canUseWifiState)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().canUseWifiState = canUseWifiState;
                return this;
            }

            /// <summary>
            /// Sets canUseWriteExternal.
            /// </summary>
            public Builder SetCanUseWriteExternal(bool canUseWriteExternal)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().canUseWriteExternal = canUseWriteExternal;
                return this;
            }

            public Builder SetCanGetAppList(bool canGetAppList)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().CanGetAppList = canGetAppList;
                return this;
            }

            /// <summary>
            /// set app list string 
            /// </summary>
            /// <param name="appList"> json string</param>
            /// <returns></returns>
            public Builder SetAppList(string appList)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().AppList = appList;
                return this;
            }

            public Builder SetDevImei(string devImei)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().DevImei = devImei;
                return this;
            }

            public Builder SetDevImeis(string devImeis)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().DevImeis = devImeis;
                return this;
            }

            public Builder SetCanUseOaid(bool canUseOaid)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().CanUseOaid = canUseOaid;
                return this;
            }

            public Builder SetDevOaid(string oaid)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().DevOaid = oaid;
                return this;
            }

            public Builder SetCanUseAndroidId(bool canUseAndroidId)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().CanUseAndroidId = canUseAndroidId;
                return this;
            }

            public Builder SetAndroidId(string androidId)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().AndroidId = androidId;
                return this;
            }

            public Builder SetCanUseMacAddress(bool canUseMacAddress)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().CanUseMacAddress = canUseMacAddress;
                return this;
            }

            public Builder SetMacAddress(string macAddress)
            {
                ABUPrivacyConfig.CreateSharedAdConfig().MacAddress = macAddress;
                return this;
            }

            /// <summary>
            /// Build the Ad slot.
            /// </summary>
            public ABUPrivacyConfig Build()
            {
                return ABUPrivacyConfig.CreateSharedAdConfig();
            }
        }
    }
}