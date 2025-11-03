using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth
{
    private static ReactiveVariable<int> _health = new ReactiveVariable<int>(100);

    public ReactiveVariable<int> Health { get { return _health; }}

    public void TakeDamage(int dmg)
    {
        _health.Value -= dmg;
    }

}
