using System;

namespace Ad
{
    public abstract class AdBase
    {
        public abstract void LoadAd();
        public abstract void ShowAd(Action<bool> callback);
        public abstract void CloseAd();
    }
}