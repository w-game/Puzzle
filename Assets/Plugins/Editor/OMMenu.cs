using UnityEditor;
using UnityEngine;

public class OMMenu
{

   [MenuItem("OpenMediation/GitHub", false, 0)]
    public static void Documentation()
    {
        Application.OpenURL("https://github.com/AdTiming/OpenMediation-Unity");
    }
    [MenuItem("OpenMediation/Integration Manager", false , 1)]
    public static void ShowSdkManager()
    {
        OMDependenciesManager.ShowDependenciesManager();
    }

    [MenuItem("OpenMediation/Integration Manager(CN)", false , 1)]
        public static void ShowSdkManagerCN()
    {
        OMDependenciesManager.ShowDependenciesManagerCN();
    }
}
