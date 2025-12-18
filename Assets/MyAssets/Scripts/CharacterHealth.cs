using System;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private ReactiveVariable<int> _health = new ReactiveVariable<int>(200);
    public event Action Died;
    public ReactiveVariable<int> Health { get { return _health; } private set { _health = value; } }

    [SerializeField] private UI _uI;

    public void Restart()
    {
        _health.Value = 200;

    }

    private void OnEnable()
    {
        _health.Dead += OnDead;

    }

    private void OnDisable()
    {
        _health.Dead -= OnDead;
    }

    public void OnDead() { Died?.Invoke(); }

    public void TakeDamage(int dmg)
    {
        _health.Value -= dmg;
        if (_health.Value <= 0)
            _health.Value = 0;

    }
    private void OnDestroy()
    {
        _health = null;
    }
}
