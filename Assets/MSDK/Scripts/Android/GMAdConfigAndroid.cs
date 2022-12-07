using UnityEngine;

#if UNITY_ANDROID

namespace ByteDance.Union
{
    public class GMAdConfigAndroid
    {
        private const string GMAdConfigBuilderName = "com.bytedance.msdk.api.v2.GMAdConfig$Builder";

        public static AndroidJavaObject CreateGMAdConfig(string appId, string appName,ABUUserConfig config)
        {
            var androidConfig = new AndroidJavaObject(GMAdConfigBuilderName)
                .Call<AndroidJavaObject>("setAppId",appId)
                .Call<AndroidJavaObject>("setDebug", config.logEnable)
                .Call<AndroidJavaObject>("setAppName", appName)
                .Call<AndroidJavaObject>("build");
            return androidConfig;
        }
    }
}
#endif