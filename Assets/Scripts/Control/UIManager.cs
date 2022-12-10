using Common;
using UI;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private ViewStack mainStack;
    [SerializeField] private ViewStack popStack;
    [SerializeField] private ViewStack topStack;

    public void PushMain<T>(params object[] objs) where T : ViewData, new()
    {
        mainStack.Push<T>(objs);
    }

    public void PushPop<T>(params object[] objs) where T : ViewData, new()
    {
        popStack.Push<T>(objs);
    }

    public void CloseSplash()
    {
        var stack = topStack as TopStack;
        stack?.CloseSplash();
    }
}