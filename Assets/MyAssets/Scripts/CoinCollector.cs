using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] private List<Coin> coinList;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<Spawner> _spawners;
    private List<Coin> coinCollected = new List<Coin>();
    public event Action StartedCollect;
    public bool HasFullCoin = false;
    private bool _isNotEmptyCoin;
    public bool IsNotEmptyCoin { get { return _isNotEmptyCoin; }private set { _isNotEmptyCoin = coinCollected.Count > 0; } }
    private int FullCoin = 9;

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
            coinCollected.Add(coin);
            DevLog.Log("монета:" + coin);
            coin.Pickup();

           // if (coinCollected.Count != FullCoin) return;

                ChangeStateEnemy();
            //if (_isNotEmptyCoin)
            //{
            //}

            if (IsCollectedItem())
            {
                DevLog.Error("все монеты собраны");
                _enemy.Dead();
                HasFullCoin = true;
                return;
            }

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

    private bool IsCollectedItem()
    {
        return coinList.Select(x => x.GetInstanceID()).OrderBy(id => id)
               .SequenceEqual(
                     coinCollected.Select(x => x.GetInstanceID()).OrderBy(id => id));

    }
}
