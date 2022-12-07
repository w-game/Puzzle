using ByteDance.Union;
using UnityEngine;

public sealed class RewardVideoAdListener : ABURewardVideoAdCallback
{
    private RewardVideoAdExample example;

    public RewardVideoAdListener(RewardVideoAdExample example)
    {
        this.example = example;
    }

    public void OnError(int code, string message)
    {
        var errMsg = "OnRewardVideoAdLoadError-- code : " + code + "--message : " + message;
        Debug.LogError("<Unity Log>..." + errMsg);
        ToastManager.Instance.ShowToast(errMsg);
    }

    public void OnRewardVideoAdLoad(object ad)
    {
        ToastManager.Instance.ShowToast("OnRewardVideoAdLoad");
        Debug.Log("<Unity Log>..." + "OnRewardVideoAdLoad");
    }

    public void OnRewardVideoAdCached()
    {
        Debug.Log("<Unity Log>..." + "OnRewardVideoCached");
        this.example.rewardVideoAdLoadSuccess = true;
    }
}