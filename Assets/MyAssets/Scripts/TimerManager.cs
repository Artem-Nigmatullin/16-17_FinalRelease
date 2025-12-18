using System;
using UnityEngine;

/// <summary>
/// 本クラスは、時間の経過を管理するタイマー機能を提供します。
/// </summary>
/// <remarks>
/// 主な機能:
/// - タイマーの開始
/// - タイマーの進行状況の更新
/// - タイマー終了時のリソース解放
/// 
/// 使用方法:
/// - ゲーム内やアプリ内で一定時間の処理やイベントを管理する際に使用します。
/// - タイマーは自動的に進行し、不要になった場合はリソースが解放されます。
/// </remarks>
public class TimerManager : MonoBehaviour
{
    [SerializeField] Player _gameobjectPlayer;
    private Timer _timer;
    [SerializeField] CoinCollector _collector;
    private CoinCollectorState _state;
    public event Action TimeIsUp;

    public bool IsActiveTime { get { return _timer != null; } }
    private void OnEnable()
    {
        _collector.OnTimerResetRequested += OnResetTime;
    }

    private void OnDisable()
    {
        _collector.OnTimerResetRequested -= OnResetTime;
        _timer = null;
    }

    private void Awake()
    {
        _timer = new Timer();
    }

    public void RestartTime()
    {
        if (_timer != null)
        {
            _timer = new Timer();
        }
        _timer.Start();
    }

    private void Start()
    {
        _timer.Start();
    }

    private void OnResetTime()
    {
        _timer = null;
    }
    private void Update()
    {
        if (IsActiveTime)
        {
            StartTime();
        }
    }

    private void StartTime()
    {
        _timer.Tick();

        if (_timer.IsFinished() || _collector.State == CoinCollectorState.FullPickedCoin)
        {
            TimeIsUp?.Invoke();
        }
    }
    private void OnDestroy()
    {
        _timer?.Dispose();
    }

}
