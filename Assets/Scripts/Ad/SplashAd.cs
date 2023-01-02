using System;
using ByteDance.Union;
using Common;

namespace Ad
{
    public class SplashAd : AdBase
    {
        public const string Tag = "Splash Ad";

        private bool _isAdReady;
        public override void LoadAd(Action<bool> callback)
        {
            var adUnit = new GMAdSlotSplash.Builder()
                .SetCodeId("102244690")
                .SetSplashButtonType(ABUSplashButtonType.ABUSplashButtonTypeDownloadBar)
                .setScenarioId("1233211223")
                .Build();
            ABUSplashAd.LoadSplashAd(adUnit, new SplashAdListener
            {
                AdLoadResult = result =>
                {
                    if (result)
                    {
                        _isAdReady = true;
                        ShowAd(callback);
                    }
                    else
                    {
                        callback?.Invoke(false);
                    }
                }
            }, 3);
        }

        public override void ShowAd(Action<bool> callback)
        {
#if UNITY_EDITOR
            callback?.Invoke(false);
            return;
#endif
            if (_isAdReady)
            {
                ABUSplashAd.ShowSplashAd(new SplashAdCallback()
                {
                    Callback = callback
                });
                _isAdReady = false;
            }
            else
            {
                LoadAd(callback);
            }
        }

        public override void CloseAd()
        {
            
        }
    }

    public class SplashAdListener : ABUSplashAdListener
    {
        public Action<bool> AdLoadResult;
        public void OnError(int code, string message)
        {
            SLog.D(SplashAd.Tag, $"code: {code}\n{message}");
            AdLoadResult?.Invoke(false);
        }

        public void OnSplashAdLoad(ABUSplashAd ad)
        {
            SLog.D(SplashAd.Tag, $"code: on splash ad loaded");
            AdLoadResult?.Invoke(true);
        }

        public void OnAdLoadTimeout()
        {
            SLog.D(SplashAd.Tag, $"code: on splash ad load time out");
            AdLoadResult?.Invoke(false);
        }
    }
    
    public class SplashAdCallback : ABUSplashAdInteractionListener
    {
        public Action<bool> Callback;
        public void OnAdClicked()
        {
            SLog.D(SplashAd.Tag, $"on ad clicked");
        }

        public void OnAdShow()
        {
            SLog.D(SplashAd.Tag, $"on ad showed");
            Callback?.Invoke(true);
        }

        public void OnAdSkip()
        {
            SLog.D(SplashAd.Tag, $"on ad skip");
        }

        public void OnAdTimeOver()
        {
            SLog.D(SplashAd.Tag, $"on ad time over");
        }

        public void OnAdClose()
        {
            SLog.D(SplashAd.Tag, $"on ad close");
        }

        public void OnWaterfallRitFillFail(string fillFailMessageInfo)
        {
            SLog.D(SplashAd.Tag, $"{fillFailMessageInfo}");
        }

        public void OnAdShowFailed(int errcode, string errorMsg)
        {
            SLog.D(SplashAd.Tag, $"errcode: {errcode}\n{errorMsg}");
            Callback?.Invoke(false);
        }
    }
}