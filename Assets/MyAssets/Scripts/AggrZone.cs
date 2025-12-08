using System;
using System.Collections;
using UnityEngine;

public class AggrZone : MonoBehaviour
{
    [SerializeField] private CoinCollector _collector;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _target;
    [SerializeField] private CharacterHealth _character;
    [SerializeField] private UI _ui;
    private readonly float _enterDistance = 5.5f;
    private readonly float _exitDistance = 12.0f;
    private bool HasEnemy => _enemy != null;
    private bool HasPlayer => _player != null;
    public event Action<GameObject> Entered;
    private bool playerInTrigger = false;
    private bool isActive = false;
    private float _dist;
    private Coroutine _damageCoroutine;
    private bool _isDamageActive = false;
    public float GetDistance()
    {
        if (HasPlayer == false) return _dist = 0;
        return _dist = Vector3.Distance(_player.transform.position, _enemy.transform.position);
    }

    private void Update()
    {

        GetDistance();

        if (_dist <= _enterDistance)
        {
            _enemy?.SetTarget(_player.transform);
            _enemy?.SetReactBehavior();
            if (_collector.IsNotEmptyCoin)
            {
                StartTakeDamage();
            }
        }


        if (_dist > _exitDistance)
        {
            _enemy?.SetIdleBehavior();
            _enemy?.ClearTarget();
        }

    }

    private void StartTakeDamage()
    {
        if (_isDamageActive) return;
        if (_damageCoroutine != null)
        {
            StopCoroutine(_damageCoroutine);
        }
        _damageCoroutine = StartCoroutine(CoroutineTakeDamage());
        _isDamageActive = true;
    }
    private IEnumerator CoroutineTakeDamage()
    {
        _character.TakeDamage(10);
        yield return new WaitForSeconds(2);
        _isDamageActive = false; // корутина завершилась
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<Player>() is Player player && HasEnemy)
        {
            _player = player;
            _ui.OnEntered(_player.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.GetComponent<Player>() is Player player && HasEnemy)
        {
            Entered?.Invoke(_player.gameObject);
            _player = player;


        }

    }

}
