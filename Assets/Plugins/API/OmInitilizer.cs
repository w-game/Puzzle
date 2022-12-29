using UnityEngine;

public class OmInitilizer
{
#if UNITY_IOS || UNITY_ANDROID
    [RuntimeInitializeOnLoadMethod]
    static void Initilize()
    {
		var projectSettings = Resources.Load("ProjectSettings") as OMProjectSettings;
		if (projectSettings.autoInit) 
		{
			#if UNITY_ANDROID
			        string appKey = projectSettings.androidAppKey;
			#elif UNITY_IOS
			        string appKey = projectSettings.iosAppKey;
			#endif
			
			if (appKey.Equals(string.Empty))
			{
			    Debug.LogWarning("OmInitilizer Cannot init without AppKey");
			}
			else
			{
			   Om.Agent.init(appKey);
			}
		}
    }
#endif

}
