using System;
using Common;
using UI;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private ViewStack mainStack;
    [SerializeField] private ViewStack popStack;
    [SerializeField] private ViewStack topStack;

    private TopStack TopStack => topStack as TopStack;
    public void PushMain<T>(params object[] objs) where T : ViewData, new()
    {
        mainStack.Push<T>(objs);
    }

    public void PushPop<T>(params object[] objs) where T : ViewData, new()
    {
        popStack.Push<T>(objs);
    }

    public void PopMain<T>() where T : ViewData
    {
        mainStack.Pop<T>();
    }

    public void CheckCloseSplash()
    {
        if (GameManager.User.PrivacyPolicy)
        {
            TopStack.Push<PopPrivacyPolicyData>();
        }
        else
        {
            CloseSplash();
        }
    }

    public void CloseSplash()
    {
        TopStack.CloseSplash();
    }

    public void ShowAddBlockTip(Color color)
    {
        TopStack.AddBlockTip.ShowTip(color);
    }

    public void ShowToast(string msg)
    {
        TopStack.ShowToast(msg);
    }

    public void DecreasePower(Transform target, Action callback)
    {
        EventCenter.Invoke<Action>(GamePower.EventKeys.OnDecreasePowerEnd, () =>
        {
            callback?.Invoke();
        });
        EventCenter.Invoke(GamePower.EventKeys.DecreasePower, target);
    }

    public void BackToHome()
    {
        mainStack.Clear();
        popStack.Clear();
    }
}