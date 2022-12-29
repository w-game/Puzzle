using UnityEngine.Events;

namespace Ad
{
    public abstract class AdBase
    {
        protected UnityAction<bool> AdCallback { get; set; }
        public abstract void InitAd();
        public abstract void LoadAd();
        public abstract void ShowAd(UnityAction<bool> callback);
        public abstract void CloseAd();
    }
}