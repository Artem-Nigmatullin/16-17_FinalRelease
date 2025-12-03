using System;
using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private ReactiveVariable<int> _health = new ReactiveVariable<int>(5000);

    public ReactiveVariable<int> Health { get { return _health; } private set { _health = value; } }

    [SerializeField] private UI _uI;

    private void Start()
    {
        _health.Dead += OnDead;
    }

    public void OnDead() { gameObject.SetActive(false); }
    public void TakeDamage(int dmg)
    {
        _health.Value -= dmg; 
    }

 

    private void OnDestroy()
    {
        _health = null;
    }
}
