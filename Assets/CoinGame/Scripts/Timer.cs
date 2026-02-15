using System;
using UnityEngine;
public class Timer : IDisposable
{
    private readonly float _startTime = 30f;
    private float _currentTime;
    private bool StartDestroyTime = false;

    public float CurrentTime { get => _currentTime;private set => _currentTime = value; }

    public bool IsFinished() => CurrentTime <= 0;

    public void Start()
    {
        CurrentTime = _startTime;
    }

    public void Tick()
    {
        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
            CurrentTime = Mathf.Max(CurrentTime, 0);
            DevLog.Warn("残りの時間:" + CurrentTime.ToString("F0"));
        }
    }

    public void Reset()
    {
        StartDestroyTime = false;
        CurrentTime = _startTime;

    }

    public void Dispose()
    {
        Debug.Log("timer disposable");
    }
}
