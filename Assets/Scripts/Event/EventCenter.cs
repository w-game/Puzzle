using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventCenter
{
    private static Dictionary<Enum, List<object>> _events = new();

    public static void Add(Enum key, Action callback)
    {
        if (!_events.ContainsKey(key))
        {
            _events.Add(key, new List<object>());
        }

        _events[key].Add(callback);
    }
    
    public static void Add<T>(Enum key, Action<T> callback)
    {
        if (!_events.ContainsKey(key))
        {
            _events.Add(key, new List<object>());
        }

        _events[key].Add(callback);
    }

    public static void AddFromNonUI(Enum key, Action callback)
    {
        Add(key, callback);
    }

    public static void Remove(Enum key, object callback)
    {
        if (!_events.ContainsKey(key)) return;

        var keyEvents = _events[key];
        if (keyEvents.Contains(callback))
        {
            keyEvents.Remove(callback);
        }
    }
    
    public static void Invoke(Enum key)
    {
        if (!_events.ContainsKey(key)) return;

        foreach (var a in _events[key])
        {
            var action = a as Action;
            action?.Invoke();
        }
    }

    public static void Invoke<T>(Enum key, T data)
    {
        if (!_events.ContainsKey(key)) return;

        foreach (var a in _events[key])
        {
            var action = a as Action<T>;
            action?.Invoke(data);
        }
    }
}