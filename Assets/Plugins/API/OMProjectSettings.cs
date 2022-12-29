using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

public class OMProjectSettings : ScriptableObject
{
    // private const string MobileAdsSettingsDir = "Assets/Plugins/Editor";

    // private const string MobileAdsSettingsResDir = "Assets/Plugins/Editor/Resources";

    // private const string MobileAdsSettingsFile =
    // "Assets/Plugins/Editor/Resources/ProjectSettings.asset";

    // private static OMProjectSettings instance;


    [SerializeField]
    public bool autoInit = false;

    [SerializeField]
    public string androidAppKey = string.Empty;

    [SerializeField]
    public string iosAppKey = string.Empty;

    [SerializeField]
    public bool isAdmobEnabled = false;

    [SerializeField]
    public bool isCN = false;    

    [SerializeField]
    public string admobAndroidAppId = string.Empty;

    [SerializeField]
    public string admobIosAppId = string.Empty;

    [SerializeField]
    public List<string> skadnetworkIds = new List<string>();


 //    public bool IsAutoInit
 //    {
 //        get
 //        {
 //            return Instance.autoInit;
 //        }

 //        set
 //        {
 //            Instance.autoInit = value;
 //        }
 //    }

 //    public string AndroidInitKey
 //    {
 //        get
 //        {
 //            return Instance.androidAppKey;
 //        }

 //        set
 //        {
 //            Instance.androidAppKey = value;
 //        }
 //    }

 //    public string IosInitKey
 //    {
 //        get
 //        {
 //            return Instance.iosAppKey;
 //        }

 //        set
 //        {
 //            Instance.iosAppKey = value;
 //        }
 //    }


 //    public bool IsAdMobEnabled
 //    {
 //        get
 //        {
 //            return Instance.isAdMobEnabled;
 //        }

 //        set
 //        {
 //            Instance.isAdMobEnabled = value;
 //        }
 //    }

 //    public bool IsCN
 //    {
 //        get
 //        {
 //            return Instance.isCN;
 //        }

 //        set
 //        {
 //            Instance.isCN = value;
 //        }
 //    }

 //    public string AdMobAndroidAppId
 //    {
 //        get
 //        {
 //            return Instance.adMobAndroidAppId;
 //        }

 //        set
 //        {
 //            Instance.adMobAndroidAppId = value;
 //        }
 //    }

 //    public string AdMobIOSAppId
 //    {
 //        get
 //        {
 //            return Instance.adMobIOSAppId;
 //        }

 //        set
 //        {
 //            Instance.adMobIOSAppId = value;
 //        }
 //    }
	
	// public List<string> SKAdNetworkIds
	// {
	//     get
	//     {
	//         return Instance.iosSKAdNetworkIds;
	//     }
	
	//     set
	//     {
	//         Instance.iosSKAdNetworkIds = value;
	//     }
	// }
	
	// public void getSettingsFromJson(Dictionary<string, object> dic) {
	// 	object obj;
	// 	Dictionary<string, object> iosSettings;
	// 	if (dic.TryGetValue("iOS", out obj)) {
	// 		iosSettings = obj as Dictionary<string, object>;
	// 		if (iosSettings.TryGetValue("SKAdNetworkIds", out obj)) {
 //                Instance.iosSKAdNetworkIds.Clear();
 //                IEnumerable<object> ids = obj as IEnumerable<object>;

 //                foreach (string id in ids)
 //                {
 //                    Instance.iosSKAdNetworkIds.Add(id);
 //                }

 //                WriteSettingsToFile();
 //            }
 //        }
	// }
	
    // public static OMProjectSettings Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             if (!AssetDatabase.IsValidFolder(MobileAdsSettingsResDir))
    //             {
    //                 AssetDatabase.CreateFolder(MobileAdsSettingsDir, "Resources");
    //             }

    //             if (File.Exists(MobileAdsSettingsFile))
    //             {
    //                 instance = (OMProjectSettings)AssetDatabase.LoadAssetAtPath(MobileAdsSettingsFile, typeof(OMProjectSettings));

    //             }

    //             if (instance == null)
    //             {
    //                 instance = ScriptableObject.CreateInstance<OMProjectSettings>();
    //                 AssetDatabase.CreateAsset(instance, MobileAdsSettingsFile);

    //             }
    //         }
    //         return instance;
    //     }
    // }

    // public void WriteSettingsToFile()
    // {
    //     AssetDatabase.SaveAssets();
    // }
}
