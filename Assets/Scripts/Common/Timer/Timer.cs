using System;
using UnityEngine;

public class Timer
{
    public float _interval;
    public float _count;
    public int Repeat { get; private set; }
    public Action _callback;

    public Timer(float interval, int repeat = 1, Action callback = null)
    {
        _interval = interval;
        Repeat = repeat;
        _callback = callback;
        TimerManager.Instance.AddTimer(this);
    }

    public void Update(float increase)
    {
        _count += increase;
        if (_count >= _interval)
        {
            _callback?.Invoke();
            _count -= _interval;

            if (Repeat > -1) Repeat--;
        }
    }

    internal void End()
    {
        TimerManager.Instance.ReadyToRemove(this);
    }
}