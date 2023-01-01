using System;
using Common;
using UI;
using UI.Popup;
using UnityEngine;
using UnityEngine.Events;

public class UIManager : SMonoSingleton<UIManager>
{
    [SerializeField] private ViewStack mainStack;
    [SerializeField] private ViewStack popStack;
    [SerializeField] private ViewStack topStack;

    [SerializeField] private GameObject topMask;
    
    private TopStack TopStack => topStack as TopStack;
    public void PushMain<T>(params object[] objs) where T : ViewData, new()
    {
        mainStack.Push<T>(objs);
    }

    public void PushPop<T>(params object[] objs) where T : ViewData, new()
    {
        popStack.Push<T>(objs);
    }
    
    public void PushTop<T>() where T : ViewData, new()
    {
        topStack.Push<T>();
    }

    public void PopMain<T>() where T : ViewData
    {
        mainStack.Pop<T>();
    }

    public void ShowAddBlockTip(Color color)
    {
        TopStack.AddBlockTip.ShowTip(color);
    }

    public void ShowToast(ToastType type, string msg)
    {
        TopStack.ShowToast(type, msg);
    }

    public void DecreasePower(Transform target, Action callback)
    {
        SetTopMask(true);
        EventCenter.Invoke<Action>(GamePower.EventKeys.OnDecreasePowerEnd, () =>
        {
            callback?.Invoke();
            SetTopMask(false);
        });
        EventCenter.Invoke(GamePower.EventKeys.DecreasePower, target);
    }

    public void BackToHome()
    {
        mainStack.Clear();
        popStack.Clear();
    }

    private Vector3 _originPos;
    private void Update()
    {
        if (GameManager.IsDebug)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _originPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                var targetPos = Input.mousePosition;
                var dir = (targetPos - _originPos).normalized;

                var angle = Vector3.Angle(Vector3.down, dir);

                if (angle <= 30 && Vector3.Distance(targetPos, _originPos) >= 200)
                {
                    PushPop<PopGMData>();
                }
            }
        }
    }

    public void ShowTip(string tip, UnityAction callback)
    {
        PushPop<PopTipData>(tip, callback);
    }

    public void SetTopMask(bool status)
    {
        topMask.SetActive(status);
    }

    public void ShowCheckBox(string des, UnityAction sureAction, UnityAction cancelAction = null)
    {
        PushPop<PopCheckBoxData>(des, sureAction, cancelAction);
    }

    public ViewBase GetHomeView()
    {
        return mainStack.GetView<HomeView>();
    }

    public void CheckCloseSplash()
    {
        TopStack.CheckCloseSplash();
    }
}