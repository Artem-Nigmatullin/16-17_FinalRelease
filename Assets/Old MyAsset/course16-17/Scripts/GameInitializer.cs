using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
   private CharacterHealth _health=new CharacterHealth();

    private void Start()
    {
      
        _health.TakeDamage(10);

        _health.TakeDamage(20);
    }

}
