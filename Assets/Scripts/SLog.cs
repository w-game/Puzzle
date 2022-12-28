using UnityEngine;

namespace Common
{
    public static class ELogTag
    {
        public static string AStar = "AStar";
        public static string Cafe = "Cafe";
        public static string CatReceiver = "CatReceiver";
    }
    public class SLog
    {
        public static void D(string tag, object msg)
        {
            if (GameManager.IsDebug)
            {
                Debug.Log($"{tag} -> {msg}");
            }
        }
        
        public static void E(string tag, object msg)
        {
            if (GameManager.IsDebug)
            {
                Debug.LogError($"{tag} -> {msg}");
            }
        }
    }
}