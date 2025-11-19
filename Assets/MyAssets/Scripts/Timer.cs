using System;
using UnityEngine;

public class Timer : IDisposable
{
    private readonly float _startTime = 50f;
    private float _currentTime;
    private bool StartDestroyTime = false;

    public bool IsFinished() => _currentTime <= 0;

    public void Start()
    {
        _currentTime = _startTime;
    }

    public void Tick()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            _currentTime = Mathf.Max(_currentTime, 0);
            Debug.Log("立った時間:" + _currentTime.ToString("F0"));
        }
    }

    public void Reset()
    {
        StartDestroyTime = false;
        _currentTime = _startTime;

    }

    public void Dispose()
    {
        Debug.Log("timer disposable");
    }
}
