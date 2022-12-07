using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ByteDance.Union
{
    public abstract class GMAdSlotBase<T> where T : GMAdSlotBase<T>.Builder
    {
        public string UnitID;


        public bool Muted; //视频是否静音，true 静音；false 开声音
        public float Volume;
        public bool UseSurfaceView;
        public string TestSlotId; //测试代码位id;
        public int DownloadType; // 控制下载APP前是否弹出合规弹窗

        //3010添加
        public bool BidNotify; //bidding类型广告，竞价成功或者失败后是否通知对应的adn

        //3400添加
        public int NetWorkNum; //网络配置的广告加载数量1-3个
        public string ScenarioId;
#if UNITY_ANDROID
        public AndroidJavaObject AndroidSlot { get; protected set; }
        public const string SlotClassPackage = "com.bytedance.msdk.api.v2.slot";
#endif
        protected GMAdSlotBase(Builder builder)
        {
            Muted = builder.GETMuted();
            Volume = builder.GETVolume();
            UseSurfaceView = builder.GETUseSurfaceView();
            TestSlotId = builder.GETTestSlotId();
            DownloadType = builder.GETDownloadType();
            BidNotify = builder.GETBidNotify();
            UnitID = builder.GetUnitId();
            ScenarioId = builder.GETScenarioId();
        }

        public abstract class Builder
        {
            private bool _mMuted; //视频是否静音，true 静音；false 开声音
            private float _mVolume;
            private bool _mUseSurfaceView;
            private string _mTestSlotId; //测试代码位id;
            private ABUDownloadType _mDownloadType = ABUDownloadType.DOWNLOAD_TYPE_NO_POPUP; // 控制下载APP前是否弹出合规弹窗
            private bool _mBidNotify; //bidding类型广告，竞价成功或者失败后是否通知对应的adn
            private string _unitID;
            private string _ScenarioId;

#if UNITY_ANDROID
            protected AndroidSlotHelper _androidSlotHelper;
#endif
#if UNITY_ANDROID && !UNITY_EDITOR
            protected Builder(AndroidSlotHelper helper)
            {
                _androidSlotHelper = helper;
            }
#endif

            protected Builder()
            {
            }

            /// <summary>
            /// Sets the code ID.
            /// </summary>
            public T SetCodeId(string codeId)
            {
                _unitID = codeId;
                return this as T;
            }

            public string GetUnitId()
            {
                return _unitID;
            }

            [Obsolete]
            public T SetDownloadType(ABUDownloadType downloadType)
            {
                _mDownloadType = downloadType;
#if UNITY_ANDROID
                _androidSlotHelper?.SetDownloadType((int) downloadType);
#endif
                return this as T;
            }

            public int GETDownloadType()
            {
                return (int) _mDownloadType;
            }


            public T SetMuted(bool muted)
            {
                this._mMuted = muted;
#if UNITY_ANDROID
                _androidSlotHelper?.SetMuted(muted);
#endif
                return this as T;
            }

            public bool GETMuted()
            {
                return _mMuted;
            }


            public T SetVolume(float volume)
            {
                _mVolume = volume;
#if UNITY_ANDROID
                _androidSlotHelper?.SetVolume(volume);
#endif
                return this as T;
            }

            public float GETVolume()
            {
                return _mVolume;
            }

            public T SetUseSurfaceView(bool useSurfaceView)
            {
                _mUseSurfaceView = useSurfaceView;
#if UNITY_ANDROID
                _androidSlotHelper?.SetUseSurfaceView(useSurfaceView);
#endif
                return this as T;
            }

            public bool GETUseSurfaceView()
            {
                return _mUseSurfaceView;
            }

            [Obsolete]
            public T SetTestSlotId(string testSlotId)
            {
                _mTestSlotId = testSlotId;
#if UNITY_ANDROID
                _androidSlotHelper?.SetTestSlotId(testSlotId);
#endif
                return this as T;
            }

            public string GETTestSlotId()
            {
                return _mTestSlotId;
            }

            public T SetBidNotify(bool bidNotify)
            {
                _mBidNotify = bidNotify;
#if UNITY_ANDROID
                _androidSlotHelper?.SetBidNotify(bidNotify);
#endif
                return this as T;
            }

            public bool GETBidNotify()
            {
                return _mBidNotify;
            }    
            
            public T setScenarioId(string scenarioId) {
          
                _ScenarioId = scenarioId;
#if UNITY_ANDROID
                _androidSlotHelper?.SetScenarioId(scenarioId);
#endif
                return this as T;
            }

            public string GETScenarioId()
            {
                return _ScenarioId;
            }

#if UNITY_ANDROID && !UNITY_EDITOR
            public AndroidJavaObject GetAndroidSlot(string slotClassName)
            {
                return _androidSlotHelper?.Build(slotClassName); 
            }
#endif
        }
    }
}