using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Enemy _enemy;

    private void Awake()
    {
    

        _enemy.Initialize();
        _spawner.Initialize();
    }
}
