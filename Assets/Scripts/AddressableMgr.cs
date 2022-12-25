using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Common
{
    public class AddressableMgr
    {
        public static void Load<T>(string path, Action<T> result)
        {
            Addressables.LoadAssetAsync<T>(path).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    result?.Invoke(handle.Result);
                }
            };
        }
    }
}