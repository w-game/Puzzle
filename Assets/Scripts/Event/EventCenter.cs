using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class EventCenter
{
    private static Dictionary<string, List<Action>> _events = new Dictionary<string, List<Action>>();

    public static void Add(string key, Action callback)
    {
        if (!_events.ContainsKey(key))
        {
            _events.Add(key, new List<Action>());
        }

        _events[key].Add(callback);
    }

    public static void AddFromNonUI(string key, Action callback)
    {
        Add(key, callback);
    }

    public static void Remove(string key, Action callback)
    {
        if (!_events.ContainsKey(key)) return;

        var keyEvents = _events[key];
        if (keyEvents.Contains(callback))
        {
            keyEvents.Remove(callback);
        }
    }

    public static void Invoke(string key)
    {
        if (!_events.ContainsKey(key)) return;

        foreach (var a in _events[key])
        {
            a?.Invoke();
        }
    }
}