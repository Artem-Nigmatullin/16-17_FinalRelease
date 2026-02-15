using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CoinCollectorState
{
    StartPickupCoin,
    FullPickedCoin
}
public class CoinCollector : MonoBehaviour
{

    [SerializeField] private List<Coin> coinList;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<Spawner> _spawners;
    private List<Coin> coinCollected = new List<Coin>();
    public event Action OnTimerResetRequested;
    public event Action CoinFullPicked;
    public event Action<Coin> CoinPicked;
    public event Action<int> CoinTaked;
    public bool HasFullCoin = false;
    private CoinCollectorState _state;
    public bool IsNotEmptyCoin => coinCollected.Count > 0;

    public CoinCollectorState State { get => _state;}

    private void OnEnable()
    {
        CoinPicked += OnCoinPiked;
    }
    private void OnDisable()
    {
        CoinPicked -= OnCoinPiked;
    }
    private void Start()
    {
        foreach (var coin in coinList)
        {
            DevLog.Error("id:" + coin.GetInstanceID());
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            CoinPicked?.Invoke(coin);

        }
    }

    private void CollectCoin(Coin coin)
    {
        coinCollected.Add(coin);
        CoinTaked?.Invoke(coinCollected.Count);
        DevLog.Log("coin: " + coin);
        coin.Hide();
    }
    private void OnCoinPiked(Coin coin)
    {
        CollectCoin(coin);

        if (State == CoinCollectorState.StartPickupCoin)
        {
            ChangeStateEnemy();
        }
        if (AreAllCoinsCollected() == false) return;
        HandleAllCoinsCollected();
    }

    private void HandleAllCoinsCollected()
    {
       

        CoinFullPicked?.Invoke();
        OnTimerResetRequested?.Invoke();

        DeadAllEnemies();

        DevLog.Error("coin full");
        _state = CoinCollectorState.FullPickedCoin;
    }
    private void DeadAllEnemies()
    {
        foreach (var enemy in GetAllEnemies())
        {
            enemy.Dead();

        }
    }
    private void ChangeStateEnemy()
    {
        foreach (var enemy in GetAllEnemies())
        {
            enemy.ChangeChaseState();
            enemy.SetReactBehavior();
        }
    }

    private IEnumerable<Enemy> GetAllEnemies()
    {
        foreach (var spawner in _spawners)
        {
            foreach (var enemy in spawner.Enemies)
            {
                yield return enemy;
            }
        }
    }

    private bool AreAllCoinsCollected()
    {
        return coinList.Select(x => x.GetInstanceID()).OrderBy(id => id)
               .SequenceEqual(
                     coinCollected.Select(x => x.GetInstanceID()).OrderBy(id => id));
    }
}
