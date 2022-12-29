using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEditor;
using UnityEngine;

public class OMProjectSettingsAssetHandler
{

    public const string OM_PROJECT_SETTINGS_DIR = "Assets/Plugins";

    public const string OM_PROJECT_SETTINGS_RESOURCE = "Assets/Plugins/Resources";

	public const string OM_PROJECT_SETTINGS_PATH = "Assets/Plugins/Resources/ProjectSettings.asset";

    private static OMProjectSettings instance;
    
	public static OMProjectSettings projectSettingsInstance 
	{
	    get
        {
            if (instance == null)
            {
                if (!AssetDatabase.IsValidFolder(OM_PROJECT_SETTINGS_RESOURCE))
                {
                    AssetDatabase.CreateFolder(OM_PROJECT_SETTINGS_DIR, "Resources");
                }

                if (File.Exists(OM_PROJECT_SETTINGS_PATH))
                {
                    instance = (OMProjectSettings)AssetDatabase.LoadAssetAtPath(OM_PROJECT_SETTINGS_PATH, typeof(OMProjectSettings));

                }

                if (instance == null)
                {
                    instance = ScriptableObject.CreateInstance<OMProjectSettings>();
                    AssetDatabase.CreateAsset(instance, OM_PROJECT_SETTINGS_PATH);

                }
            }
            return instance;
        }
	}

	static public void WriteSettingsToFile()
    {
        AssetDatabase.SaveAssets();
    }

	static public void getSettingsFromJson(Dictionary<string, object> dic) 
	{
		object obj;
		Dictionary<string, object> iosSettings;
		if (dic.TryGetValue("iOS", out obj)) {
			iosSettings = obj as Dictionary<string, object>;
			if (iosSettings.TryGetValue("SKAdNetworkIds", out obj)) {
                instance.skadnetworkIds.Clear();
	            IEnumerable<object> ids = obj as IEnumerable<object>;

	            foreach (string id in ids)
	            {
                    instance.skadnetworkIds.Add(id);
	            }

	            WriteSettingsToFile();
	        }
	    }
	}
}