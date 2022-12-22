using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ElementBase : MonoBehaviour
    {
        private Dictionary<Enum, List<object>> _events = new();

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
    }
}