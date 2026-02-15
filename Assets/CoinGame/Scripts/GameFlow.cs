using System;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Run,
    TimeIsUp,
    IsDead,
    GameOver,
    Restart
}

public class GameFlow : MonoBehaviour
{
    [SerializeField] private CoinZoneSet _zones;
    [SerializeField] private CoinZone _coinZone;
    [SerializeField] private TimerManager _timerManager;
    [SerializeField] private Player _player;
    [SerializeField] CharacterHealth _characterHealth;
    private GameState _gameState = GameState.Run;
    public event Action GameOver;
    public event Action RestartedGame;

    private void OnEnable()
    {
        _characterHealth.Died += OnDied;
        _timerManager.TimeIsUp += OnDied;
    }
    private void OnDisable()
    {
        _characterHealth.Died -= OnDied;
        _timerManager.TimeIsUp -= OnDied;
    }
    private void OnDied()
    {
        _gameState = GameState.TimeIsUp;

    }

    private void Update()
    {
        if (_gameState == GameState.TimeIsUp || _gameState == GameState.IsDead)
        {
            Dead();
        }
        if (_gameState == GameState.GameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    private void Dead()
    {
        GameOver?.Invoke();
        _player.gameObject.SetActive(false);
        _gameState = GameState.GameOver;
    }
    private void Restart1()
    {
        RestartedGame?.Invoke();
        _player.gameObject.SetActive(true);
        _timerManager.RestartTime();
        if (_characterHealth.Health.Value <= 0)
        {
            _characterHealth.Restart();

        }
    }

    private void Restart()
    {
        RestartedGame?.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }

}
