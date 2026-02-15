using TMPro;
using UnityEngine;

/// <summary>
/// 本クラスは、プレイヤーの状態（体力）と特定エリアへの滞在状況を管理し、
/// 画面上に関連情報を表示する機能を持っています。
/// </summary>
/// <remarks>
/// 主な機能:
/// - プレイヤーが特定のエリアに入ったかどうかを記録
/// - プレイヤーの体力の変化を追跡
/// - 現在のプレイヤー情報を画面に反映
/// 
/// 使用方法:
/// - ゲーム内でプレイヤーが行動する際、体力や滞在状況の更新が自動的に行われます。
/// - 管理者や他の開発者が画面上でプレイヤーの情報を確認できます。
/// </remarks>

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI : SettingsMonoBehavior, IHealthListener, IEnterable
{
    [SerializeField] private AggrZone _aggrZone; // プレイヤーが入ると検知されるエリア
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private TextMeshProUGUI _textPlayerName;
    [SerializeField] private TextMeshProUGUI _textRestartGame;
    [SerializeField] private TextMeshProUGUI _textTimeGame;
    [SerializeField] private TextMeshProUGUI _textCoinCount;
    [SerializeField] private CharacterHealth _characterHealth; // キャラクターの体力
    //[SerializeField] private GameFlow _gameFlow;
    //[SerializeField] private TimerManager _timerManager;
    //[SerializeField] private CoinCollector _coinCollector;
    private void Start()
    {
        Debug.Log("UI HEALTH:" + _characterHealth.Health.Value);
    }

    private void OnEnable()
    {
        _coinCollector.CoinTaked += OnCointedCoin;
        _timerManager.StartedTime+=OnCountedTime;
        _gameFlow.RestartedGame += OnRestartedGame;
        _gameFlow.GameOver += OnGameOver;
        _aggrZone.Entered += OnEntered;
        _characterHealth.Health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        _coinCollector.CoinTaked -= OnCointedCoin;
        _timerManager.StartedTime -= OnCountedTime;
        _gameFlow.RestartedGame -= OnRestartedGame;
        _gameFlow.GameOver -= OnGameOver;
        _aggrZone.Entered -= OnEntered;
        _characterHealth.Health.Changed -= OnHealthChanged;

    }

    private void OnCointedCoin(int coin)
    {
       
        _textCoinCount.text = $"コイン{coin}/4";
    }
    private void OnCountedTime(float time)
    {
        if (_timerManager != null)
        {
            _textTimeGame.text = $"{time.ToString("F0")}:秒";
        }
    }
    private void OnGameOver()
    {
        _textRestartGame.text = $"ゲームオーバー\nRキーを押して再起動";
    }

    private void OnRestartedGame()
    {
        _textRestartGame.text = $"";
    }
    public void OnHealthChanged(int health)
    {
        _textHealth.text = $"HP: {_characterHealth.Health.Value}";
        _characterHealth.Health.Value = health;

    }

    public void OnEntered(GameObject gameObject)
    {
        // _player=gameObject;
        _textPlayerName.text = $"Entered: {gameObject.name}";
        DevLog.Error("name:" + gameObject);
    }

    public void OnExit(GameObject player)
    {
        _textPlayerName.text = "Entered: -";
    }

}
