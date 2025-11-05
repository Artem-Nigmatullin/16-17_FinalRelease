using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth:MonoBehaviour 
{
    private ReactiveVariable<int> _health = new ReactiveVariable<int>(1000);

    public ReactiveVariable<int> Health { get { return _health; }private set { _health = value; } }


    public void TakeDamage(int dmg)
    {
        _health.Value -= dmg;
    }


}
