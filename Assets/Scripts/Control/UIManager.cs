using Common;
using UI;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private ViewStack mainStack;
    [SerializeField] private ViewStack popStack;

    public void PushMain<T>(params object[] objs) where T : ViewData, new()
    {
        mainStack.Push<T>(objs);
    }

    public void PushPop<T>(params object[] objs) where T : ViewData, new()
    {
        popStack.Push<T>(objs);
    }
}