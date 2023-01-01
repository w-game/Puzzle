using Common;
using UI.View;
using UnityEngine;

namespace UI
{
    public class TopStack : ViewStack
    {
        [SerializeField] private AddBlockTip addBlockTip;
        [SerializeField] private ToastElement toastElement;
        [SerializeField] private SplashView splash;

        public AddBlockTip AddBlockTip => addBlockTip;

        public void ShowToast(ToastType type, string msg)
        {
            toastElement.ShowToast(new SToast
            {
                Type = type,
                msg = msg
            });
        }

        public void CheckCloseSplash()
        {
            splash.CheckClose();
        }
    }
}