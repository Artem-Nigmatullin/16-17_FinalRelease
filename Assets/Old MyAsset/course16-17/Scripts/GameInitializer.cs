using System;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   [SerializeField] private CharacterHealth _health;

    public void Initialize()
    {
        _health.TakeDamage(10);

        _health.TakeDamage(20);
        Debug.Log("game initializer:" + _health.Health.Value);

    }

    private void Start()
    {
    }

}
