using System;
using System.Collections.Generic;
using Common;
using UnityEngine;

public class TimerManager : SMonoSingleton<TimerManager>
{
    private List<Timer> _timers = new List<Timer>();
    private List<Timer> _waitForRemove = new List<Timer>();

    public void AddTimer(Timer timer)
    {
        _timers.Add(timer);
    }

    internal void RemoveTimer(Timer timer)
    {
        _timers.Remove(timer);
    }

    public void ReadyToRemove(Timer timer)
    {
        _waitForRemove.Add(timer);
    }

    void Update()
    {
        foreach (var timer in new List<Timer>(_timers))
        {
            timer.Update(Time.deltaTime);
            if (timer.Repeat == 0)
            {
                _waitForRemove.Add(timer);
            }
        }

        foreach (var timer in _waitForRemove)
        {
            RemoveTimer(timer);
        }
        _waitForRemove.Clear();
    }
}