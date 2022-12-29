    using System;
    using System.Linq;
    using UnityEngine;

    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

public class OmEvents : MonoBehaviour
    {

        void Awake()
        {
            gameObject.name = "OmEvents";
            DontDestroyOnLoad(gameObject);
        }

        private static event Action _onSdkInitSuccessEvent;
        public static event Action onSdkInitSuccessEvent
        {
            add
            {
                if (_onSdkInitSuccessEvent == null || !_onSdkInitSuccessEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitSuccessEvent += value;
                }
            }

            remove
            {
                if (_onSdkInitSuccessEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitSuccessEvent -= value;
                }
            }
        }

        public void onSdkInitSuccess() {
            if (_onSdkInitSuccessEvent != null)
            {
                _onSdkInitSuccessEvent();
            }
        }

        private static event Action<string> _onSdkInitFailedEvent;
        public static event Action<string> onSdkInitFailedEvent
        {
            add
            {
                if (_onSdkInitFailedEvent == null || !_onSdkInitFailedEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitFailedEvent += value;
                }
            }

            remove
            {
                if (_onSdkInitFailedEvent.GetInvocationList().Contains(value))
                {
                    _onSdkInitFailedEvent -= value;
                }
            }
        }

        public void onSdkInitFailed(string error)
        {
            if (_onSdkInitFailedEvent != null)
            {
                _onSdkInitFailedEvent(error);
            }
        }

        private static event Action<bool> _onRewardedVideoAvailabilityChangedEvent;

        public static event Action<bool> onRewardedVideoAvailabilityChangedEvent
        {
            add
            {
                if (_onRewardedVideoAvailabilityChangedEvent == null || !_onRewardedVideoAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoAvailabilityChangedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoAvailabilityChangedEvent -= value;
                }
            }
        }

        public void onRewardedVideoAvailabilityChanged(string available)
        {
            if (_onRewardedVideoAvailabilityChangedEvent != null)
            {
                bool isAvailable = (available == "true") ? true : false;
                _onRewardedVideoAvailabilityChangedEvent(isAvailable);
            }
        }

        private static event Action<string> _onRewardedVideoShowedEvent;

        public static event Action<string> onRewardedVideoShowedEvent
        {
            add
            {
                if (_onRewardedVideoShowedEvent == null || !_onRewardedVideoShowedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoShowedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowedEvent -= value;
                }
            }
        }

        public void onRewardedVideoShowed(string scene)
        {
            if (_onRewardedVideoShowedEvent != null)
            {
                _onRewardedVideoShowedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoShowFailedEvent;

        public static event Action<string> onRewardedVideoShowFailedEvent
        {
            add
            {
                if (_onRewardedVideoShowFailedEvent == null || !_onRewardedVideoShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowFailedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoShowFailedEvent -= value;
                }
            }
        }

        public void onRewardedVideoShowFailed(string error)
        {
            if (_onRewardedVideoShowFailedEvent != null)
            {
                _onRewardedVideoShowFailedEvent(error);
            }
        }

        private static event Action<string> _onRewardedVideoClosedEvent;

        public static event Action<string> onRewardedVideoClosedEvent
        {
            add
            {
                if (_onRewardedVideoClosedEvent == null || !_onRewardedVideoClosedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClosedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoClosedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClosedEvent -= value;
                }
            }
        }

        public void onRewardedVideoClosed(string scene)
        {
            if (_onRewardedVideoClosedEvent != null)
            {
                _onRewardedVideoClosedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoStartedEvent;

        public static event Action<string> onRewardedVideoStartedEvent
        {
            add
            {
                if (_onRewardedVideoStartedEvent == null || !_onRewardedVideoStartedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoStartedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoStartedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoStartedEvent -= value;
                }
            }
        }

        public void onRewardedVideoStarted(string scene)
        {
            if (_onRewardedVideoStartedEvent != null)
            {
                _onRewardedVideoStartedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoEndedEvent;

        public static event Action<string> onRewardedVideoEndedEvent
        {
            add
            {
                if (_onRewardedVideoEndedEvent == null || !_onRewardedVideoEndedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoEndedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoEndedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoEndedEvent -= value;
                }
            }
        }

        public void onRewardedVideoEnded(string scene)
        {
            if (_onRewardedVideoEndedEvent != null)
            {
                _onRewardedVideoEndedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoRewardedEvent;

        public static event Action<string> onRewardedVideoRewardedEvent
        {
            add
            {
                if (_onRewardedVideoRewardedEvent == null || !_onRewardedVideoRewardedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoRewardedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoRewardedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoRewardedEvent -= value;
                }
            }
        }

        public void onRewardedVideoRewarded(string scene)
        {
            if (_onRewardedVideoRewardedEvent != null)
            {
                _onRewardedVideoRewardedEvent(scene);
            }
        }

        private static event Action<string> _onRewardedVideoClickedEvent;

        public static event Action<string> onRewardedVideoClickedEvent
        {
            add
            {
                if (_onRewardedVideoClickedEvent == null || !_onRewardedVideoClickedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClickedEvent += value;
                }
            }

            remove
            {
                if (_onRewardedVideoClickedEvent.GetInvocationList().Contains(value))
                {
                    _onRewardedVideoClickedEvent -= value;
                }
            }
        }
    
        public void onRewardedVideoClicked(string scene)
        {
            if (_onRewardedVideoClickedEvent != null)
            {
                _onRewardedVideoClickedEvent(scene);
            }
        }


        private static event Action<bool> _onInterstitialAvailabilityChangedEvent;

        public static event Action<bool> onInterstitialAvailabilityChangedEvent
        {
            add
            {
                if (_onInterstitialAvailabilityChangedEvent == null || !_onInterstitialAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialAvailabilityChangedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialAvailabilityChangedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialAvailabilityChangedEvent -= value;
                }
            }
        }

        public void onInterstitialAvailabilityChanged(string available) {
            if (_onInterstitialAvailabilityChangedEvent != null) {
                bool isAvailable = (available == "true") ? true : false;
                _onInterstitialAvailabilityChangedEvent(isAvailable);
            }
        }

        private static event Action<string> _onInterstitialShowedEvent;

        public static event Action<string> onInterstitialShowedEvent
        {
            add
            {
                if (_onInterstitialShowedEvent == null || !_onInterstitialShowedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialShowedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowedEvent -= value;
                }
            }
        }

        public void onInterstitialShowed(string scene)
        {
            if (_onInterstitialShowedEvent != null)
            {
                _onInterstitialShowedEvent(scene);
            }
        }

        private static event Action<string> _onInterstitialClosedEvent;

        public static event Action<string> onInterstitialClosedEvent
        {
            add
            {
                if (_onInterstitialClosedEvent == null || !_onInterstitialClosedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClosedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialClosedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClosedEvent -= value;
                }
            }
        }

        public void onInterstitialClosed(string scene)
        {
            if (_onInterstitialClosedEvent != null)
            {
                _onInterstitialClosedEvent(scene);
            }
        }

        private static event Action<string> _onInterstitialShowFailedEvent;

        public static event Action<string> onInterstitialShowFailedEvent
        {
            add
            {
                if (_onInterstitialShowFailedEvent == null || !_onInterstitialShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowFailedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialShowFailedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialShowFailedEvent -= value;
                }
            }
        }

        public void onInterstitialShowFailed(string error)
        {
            if (_onInterstitialShowFailedEvent != null)
            {
                _onInterstitialShowFailedEvent(error);
            }
        }

        private static event Action<string> _onInterstitialClickedEvent;

        public static event Action<string> onInterstitialClickedEvent
        {
            add
            {
                if (_onInterstitialClickedEvent == null || !_onInterstitialClickedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClickedEvent += value;
                }
            }

            remove
            {
                if (_onInterstitialClickedEvent.GetInvocationList().Contains(value))
                {
                    _onInterstitialClickedEvent -= value;
                }
            }
        }

        public void onInterstitialClicked(string scene)
        {
            if (_onInterstitialClickedEvent != null)
            {
                _onInterstitialClickedEvent(scene);
            }
        }

    private static event Action<bool> _onPromotionAdAvailabilityChangedEvent;

    public static event Action<bool> onPromotionAdAvailabilityChangedEvent
    {
        add
        {
            if (_onPromotionAdAvailabilityChangedEvent == null || !_onPromotionAdAvailabilityChangedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdAvailabilityChangedEvent += value;
            }
        }

        remove
        {
            if (_onPromotionAdAvailabilityChangedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdAvailabilityChangedEvent -= value;
            }
        }
    }

    public void onPromotionAdAvailabilityChanged(string available)
    {
        if (_onPromotionAdAvailabilityChangedEvent != null)
        {
            bool isAvailable = (available == "true") ? true : false;
            _onPromotionAdAvailabilityChangedEvent(isAvailable);
        }
    }

    private static event Action<string> _onPromotionAdShowedEvent;

    public static event Action<string> onPromotionAdShowedEvent
    {
        add
        {
            if (_onPromotionAdShowedEvent == null || !_onPromotionAdShowedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdShowedEvent += value;
            }
        }

        remove
        {
            if (_onPromotionAdShowedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdShowedEvent -= value;
            }
        }
    }

    public void onPromotionAdShowed(string scene)
    {
        if (_onPromotionAdShowedEvent != null)
        {
            _onPromotionAdShowedEvent(scene);
        }
    }

    private static event Action<string> _onPromotionAdHiddenEvent;

    public static event Action<string> onPromotionAdHiddenEvent
    {
        add
        {
            if (_onPromotionAdHiddenEvent == null || !_onPromotionAdHiddenEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdHiddenEvent += value;
            }
        }

        remove
        {
            if (_onPromotionAdHiddenEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdHiddenEvent -= value;
            }
        }
    }

    public void onPromotionAdHidden(string scene)
    {
        if (_onPromotionAdHiddenEvent != null)
        {
            _onPromotionAdHiddenEvent(scene);
        }
    }

    private static event Action<string> _onPromotionAdShowFailedEvent;

    public static event Action<string> onPromotionAdShowFailedEvent
    {
        add
        {
            if (_onPromotionAdShowFailedEvent == null || !_onPromotionAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdShowFailedEvent += value;
            }
        }

        remove
        {
            if (_onPromotionAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdShowFailedEvent -= value;
            }
        }
    }

    public void onPromotionAdShowFailed(string error)
    {
        if (_onPromotionAdShowFailedEvent != null)
        {
            _onPromotionAdShowFailedEvent(error);
        }
    }

    private static event Action<string> _onPromotionAdClickedEvent;

    public static event Action<string> onPromotionAdClickedEvent
    {
        add
        {
            if (_onPromotionAdClickedEvent == null || !_onPromotionAdClickedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdClickedEvent += value;
            }
        }

        remove
        {
            if (_onPromotionAdClickedEvent.GetInvocationList().Contains(value))
            {
                _onPromotionAdClickedEvent -= value;
            }
        }
    }

    public void onPromotionAdClicked(string scene)
    {
        if (_onPromotionAdClickedEvent != null)
        {
            _onPromotionAdClickedEvent(scene);
        }
    }

    //************ BannerAds callback
    private static event Action<string> _onBannerLoadSuccessEvent;

    public static event Action<string> onBannerLoadSuccessEvent {
        add
        {
            if (_onBannerLoadSuccessEvent == null || !_onBannerLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadSuccessEvent += value;
            }
        }

        remove
        {
            if (_onBannerLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadSuccessEvent -= value;
            }
        }
    }

    public void onBannerLoadSuccess(string placementId) {
        if (_onBannerLoadSuccessEvent != null) {
            _onBannerLoadSuccessEvent(placementId);
        }
    }

    private static event Action<string, string> _onBannerLoadFailedEvent;

    public static event Action<string, string> onBannerLoadFailedEvent
    {
        add
        {
            if (_onBannerLoadFailedEvent == null || !_onBannerLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadFailedEvent += value;
            }
        }

        remove
        {
            if (_onBannerLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onBannerLoadFailedEvent -= value;
            }
        }
    }

    public void onBannerLoadFailed(string args)
    {
        if (_onBannerLoadFailedEvent != null && !String.IsNullOrEmpty(args))
        {
            List<object> argList = OmJSON.Json.Deserialize(args) as List<object>;
            string placementId = argList[0].ToString();
            string errorMsg = argList[1].ToString();
            _onBannerLoadFailedEvent(placementId, errorMsg);
        }
    }

    private static event Action<string> _onBannerClickedEvent;

    public static event Action<string> onBannerClickedEvent
    {
        add
        {
            if (_onBannerClickedEvent == null || !_onBannerClickedEvent.GetInvocationList().Contains(value))
            {
                _onBannerClickedEvent += value;
            }
        }

        remove
        {
            if (_onBannerClickedEvent.GetInvocationList().Contains(value))
            {
                _onBannerClickedEvent -= value;
            }
        }
    }

    public void onBannerClicked(string placementId)
    {
        if (_onBannerClickedEvent != null)
        {
            _onBannerClickedEvent(placementId);
        }
    }

    //************ SplashAd callback

    private static event Action<string> _onSplashAdLoadSuccessEvent;

    public static event Action<string> onSplashAdLoadSuccessEvent
    {
        add
        {
            if (_onSplashAdLoadSuccessEvent == null || !_onSplashAdLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdLoadSuccessEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdLoadSuccessEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdLoadSuccessEvent -= value;
            }
        }
    }

    public void onSplashAdLoadSuccess(string placementId)
    {
        if (_onSplashAdLoadSuccessEvent != null)
        {
            _onSplashAdLoadSuccessEvent(placementId);
        }
    }

    private static event Action<string> _onSplashAdLoadFailedEvent;

    public static event Action<string> onSplashAdLoadFailedEvent
    {
        add
        {
            if (_onSplashAdLoadFailedEvent == null || !_onSplashAdLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdLoadFailedEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdLoadFailedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdLoadFailedEvent -= value;
            }
        }
    }

    public void onSplashAdLoadFailed(string placementId)
    {
        if (_onSplashAdLoadFailedEvent != null)
        {
            _onSplashAdLoadFailedEvent(placementId);
        }
    }

    private static event Action<string> _onSplashAdShowedEvent;

    public static event Action<string> onSplashAdShowedEvent
    {
        add
        {
            if (_onSplashAdShowedEvent == null || !_onSplashAdShowedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdShowedEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdShowedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdShowedEvent -= value;
            }
        }
    }

    public void onSplashAdShowed(string placementId)
    {
        if (_onSplashAdShowedEvent != null)
        {
            _onSplashAdShowedEvent(placementId);
        }
    }

    private static event Action<string> _onSplashAdShowFailedEvent;

    public static event Action<string> onSplashAdShowFailedEvent
    {
        add
        {
            if (_onSplashAdShowFailedEvent == null || !_onSplashAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdShowFailedEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdShowFailedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdShowFailedEvent -= value;
            }
        }
    }

    public void onSplashAdShowFailed(string placementId)
    {
        if (_onSplashAdShowFailedEvent != null)
        {
            _onSplashAdShowFailedEvent(placementId);
        }
    }


    private static event Action<string> _onSplashAdClickEvent;

    public static event Action<string> onSplashAdClickEvent
    {
        add
        {
            if (_onSplashAdClickEvent == null || !_onSplashAdClickEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdClickEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdClickEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdClickEvent -= value;
            }
        }
    }

    public void onSplashAdClick(string placementId)
    {
        if (_onSplashAdClickEvent != null)
        {
            _onSplashAdClickEvent(placementId);
        }
    }


    private static event Action<string> _onSplashAdClosedEvent;

    public static event Action<string> onSplashAdClosedEvent
    {
        add
        {
            if (_onSplashAdClosedEvent == null || !_onSplashAdClosedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdClosedEvent += value;
            }
        }

        remove
        {
            if (_onSplashAdClosedEvent.GetInvocationList().Contains(value))
            {
                _onSplashAdClosedEvent -= value;
            }
        }
    }

    public void onSplashAdClosed(string placementId)
    {
        if (_onSplashAdClosedEvent != null)
        {
            _onSplashAdClosedEvent(placementId);
        }
    }


    private static event Action<OmImpressionData> _onImpressionData;

    public static event Action<OmImpressionData> onImpressionDataCallback
    {
        add
        {
            if (_onImpressionData == null || !_onImpressionData.GetInvocationList().Contains(value))
            {
                _onImpressionData += value;
            }
        }

        remove
        {
            if (_onImpressionData.GetInvocationList().Contains(value))
            {
                _onImpressionData -= value;
            }
        }
    }

    public void onImpressionData(string dataJson)
    {
        if (_onImpressionData != null)
        {
            OmImpressionData impressionData = new OmImpressionData(dataJson);
            _onImpressionData(impressionData);
        }
    }



    private static event Action<string> _onImpressionDataError;

    public static event Action<string> onImpressionDataErrorCallback
    {
        add
        {
            if (_onImpressionDataError == null || !_onImpressionDataError.GetInvocationList().Contains(value))
            {
                _onImpressionDataError += value;
            }
        }

        remove
        {
            if (_onImpressionDataError.GetInvocationList().Contains(value))
            {
                _onImpressionDataError -= value;
            }
        }
    }

    public void onImpressionDataError(string error)
    {
        if (_onImpressionDataError != null)
        {
            _onImpressionDataError(error);
        }
    }

}