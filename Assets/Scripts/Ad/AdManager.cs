using System.Collections.Generic;
using ByteDance.Union;
using Newtonsoft.Json;
using UnityEngine;

namespace Common
{
    public class AdManager : Singleton<AdManager>
    {
        protected override void Init()
        {
            ABUUserConfig userConfig = new();
            userConfig.logEnable = true;
            ABUAdSDK.setupMSDK("", "msdk demo", userConfig);
        }
    }
}