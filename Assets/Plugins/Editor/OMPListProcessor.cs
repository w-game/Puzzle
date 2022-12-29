#if UNITY_IPHONE || UNITY_IOS
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;


public static class OMPListProcessor
{
    [PostProcessBuild]
    public static void OnPostProcessBuild(BuildTarget buildTarget, string path)
    {
        string plistPath = Path.Combine(path, "Info.plist");
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        if (OMProjectSettingsAssetHandler.projectSettingsInstance.isAdmobEnabled)
        {
            string appId = OMProjectSettingsAssetHandler.projectSettingsInstance.admobAndroidAppId;
            if (appId.Length == 0)
            {
                NotifyBuildFailure(
                    "iOS AdMob app ID is empty. Please enter a valid app ID to run ads properly.");
            }
            else
            {
                plist.root.SetString("GADApplicationIdentifier", appId);
            }
        }
		
		PlistElementArray SKAdNetworkItems;
		SKAdNetworkItems = plist.root.CreateArray("SKAdNetworkItems");
		
		List<string> iosSKAdNetworkIds = OMProjectSettingsAssetHandler.projectSettingsInstance.skadnetworkIds;
		
		
		foreach (string id in iosSKAdNetworkIds)
		{
		    PlistElementDict SKAdNetworkItem;
		    SKAdNetworkItem = SKAdNetworkItems.AddDict();
		    SKAdNetworkItem.SetString("SKAdNetworkIdentifier", id);
		}
        File.WriteAllText(plistPath, plist.WriteToString());
    }

    private static void NotifyBuildFailure(string message)
    {
        string prefix = "[OMUnityManager] ";

        EditorUtility.DisplayDialog(
            "Setting AdMob App ID", "Error: " + message, "", "");

#if UNITY_2017_1_OR_NEWER
        throw new BuildPlayerWindow.BuildMethodException(prefix + message);
#else
        throw new OperationCanceledException(prefix + message);
#endif
    }
}

#endif
