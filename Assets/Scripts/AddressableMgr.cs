using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Common
{
    public class AddressableMgr
    {
        private static Dictionary<string, object> _resources = new();
        public static void Load<T>(string path, Action<T> result) where T : class
        {
            if (_resources.ContainsKey(path))
            {
                result?.Invoke(_resources[path] as T);
                return;
            }
            
            Addressables.LoadAssetAsync<T>(path).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    if (!_resources.ContainsKey(path))
                    {
                        _resources.Add(path, handle.Result);
                    }
                    result?.Invoke(handle.Result);
                }
            };
        }
    }
}