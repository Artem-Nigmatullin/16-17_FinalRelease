using UnityEngine;

public class TimerManager : MonoBehaviour
{
   private Timer _timer;

    private void Awake()
    {
        _timer = new Timer();
    }

    private void Start()
    {
      _timer.Start();   
    }

    private void Update()
    {
        _timer.Tick();   
    }

    private void OnDestroy()
    {
        _timer?.Dispose();
        _timer=null;
    }

}
