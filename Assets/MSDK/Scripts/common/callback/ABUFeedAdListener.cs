//------------------------------------------------------------------------------
// Copyright (c) 2018-2019 Beijing Bytedance Technology Co., Ltd.
// All Right Reserved.
// Unauthorized copying of this file, via any medium is strictly prohibited.
// Proprietary and confidential.
//------------------------------------------------------------------------------

using UnityEngine;

namespace ByteDance.Union
{
    using System.Collections.Generic;

    /// <summary>
    /// The listener for feed Ad.
    /// </summary>
    public interface ABUFeedAdListener
    {
        /// <summary>
        /// Invoke when load Ad error.
        /// </summary>
        void OnError(int code, string message);

        /// <summary>
        /// Invoke when the Ad load success.
        /// </summary>
        void OnFeedAdLoad(AndroidJavaObject list, List<ABUNativeAd> nativeAds);
    }
}
