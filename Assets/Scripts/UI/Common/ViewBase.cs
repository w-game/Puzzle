using System;
using System.Collections.Generic;
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
        public virtual bool AnimaSwitch => false;
    }

    public abstract class ViewBase : MonoBehaviour
    {
        public BaseView BaseView { get; set; }

        private Dictionary<Enum, List<object>> _events = new();
        public virtual void OnCreate(params object[] objects) { }
        
        public virtual void ScreenAdapt(Rect rect) { }
        protected void CloseView()
        {
            BaseView.CloseView();
        }

        public virtual void DoOpenAnima()
        {
            
        }

        public virtual void DoCloseAnima(Action onComplete = null)
        {
            
        }
        
        protected void AddEvent(Enum key, Action callback)
        {
            if (!_events.ContainsKey(key)) _events.Add(key, new List<object>());

            _events[key].Add(callback);
            EventCenter.Add(key, callback);
        }

        protected void AddEvent<T>(Enum key, Action<T> callback)
        {
            if (!_events.ContainsKey(key)) _events.Add(key, new List<object>());

            _events[key].Add(callback);
            EventCenter.Add(key, callback);
        }

        protected void RemoveEvent(Enum key, object callback)
        {
            if (!_events.ContainsKey(key)) return;

            var keyEvents = _events[key];
            if (keyEvents.Contains(callback))
            {
                keyEvents.Remove(callback);
                EventCenter.Remove(key, callback); 
            }
        }

        private void OnDestroy()
        {
            foreach (var key in _events.Keys)
            {
                foreach (var callback in new List<object>(_events[key]))
                {
                    RemoveEvent(key, callback);
                }
            }
        }
    }
}