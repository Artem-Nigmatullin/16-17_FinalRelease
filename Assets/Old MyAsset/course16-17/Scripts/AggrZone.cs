using System;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;

public class AggrZone : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;

    [SerializeField] private Transform _target;

    [SerializeField] private Spawner _spawner;
    private bool HasEnemy => _enemy != null;

    public event Action<GameObject> Entered;

    public GameObject GetGameObject() => _player.gameObject;
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Player>() is Player player && HasEnemy)
        {
            _player = player;
            Entered?.Invoke(player.gameObject);

                _enemy.SetTarget(_player.transform);
                _enemy.SetReactBehavior();
            
            Debug.Log($"{_enemy.name} заметил игрока!");


            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() is Player player && HasEnemy)
        {
            Entered?.Invoke(player.gameObject);
            _player = player;
 
                _enemy.SetIdleBehavior();
                _enemy.ClearTarget();

            //_currentState = EnemyBehaviorType.Idle;
            DevLog.Error($"{_enemy.name} потерял игрока");


        }

    }

}
