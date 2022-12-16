using System;
using UnityEngine;

public class Timer
{
    public float Interval { get; }
    public Action Callback { get; }
    public int Repeat { get; private set; }
    public bool IsPause { get; set; }

    private float _count;

    public Timer(float interval, int repeat = 1, Action callback = null)
    {
        Interval = interval;
        Repeat = repeat;
        Callback = callback;
        TimerManager.Instance.AddTimer(this);
    }
    
    public void Reset()
    {
        _count = 0;
        Play();
    }

    public void Update(float increase)
    {
        if (IsPause) return;
        
        _count += increase;
        if (_count >= Interval)
        {
            Callback?.Invoke();
            _count -= Interval;

            if (Repeat > -1) Repeat--;
        }
    }

    internal void End()
    {
        TimerManager.Instance.ReadyToRemove(this);
    }

    public void Pause()
    {
        IsPause = true;
    }

    public void Play()
    {
        IsPause = false;
    }
}