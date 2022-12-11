using UnityEngine;

namespace Android
{
    public static class AndroidNative
    {
        public static int GetNotchHeight()
        {
            // var context = GetUnityContext();
            var jo = new AndroidJavaObject("com.shuanger.puzzle.ScreenUtil");
            return jo.Call<int>("getNotchHeight");
        }

        private static AndroidJavaObject GetUnityContext()
        {
            AndroidJavaClass unityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            var unityActivity = unityClass.GetStatic<AndroidJavaObject>("currentActivity");
            return unityActivity;
        }
    }
}