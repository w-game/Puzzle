using UnityEngine;

namespace UI
{
    public enum ViewType
    {
        View,
        Popup
    }

    public abstract class ViewData
    {
        public abstract string ViewName { get; }
        public abstract ViewType ViewType { get; }
        public abstract bool Mask { get; }
    }

    public abstract class ViewBase : MonoBehaviour
    {
        public ViewData ViewData { get; set; }
        public BaseView BaseView { get; set; }

        public virtual void OnCreate(params object[] objects) { }
        protected void CloseView()
        {
            BaseView.CloseView();
        }
    }
}