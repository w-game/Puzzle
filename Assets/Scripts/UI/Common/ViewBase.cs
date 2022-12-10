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
        public ViewData ViewData { get; set; }
        public BaseView BaseView { get; set; }

        private Dictionary<string, List<Action>> _events = new();
        public virtual void OnCreate(params object[] objects) { }
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

        protected void AddEvent(string key, Action callback)
        {
            if (!_events.ContainsKey(key)) _events.Add(key, new List<Action>());

            _events[key].Add(callback);
            EventCenter.Add(key, callback);
        }

        protected void RemoveEvent(string key, Action callback)
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
                foreach (var callback in new List<Action>(_events[key]))
                {
                    RemoveEvent(key, callback);
                }
            }
        }
    }
}