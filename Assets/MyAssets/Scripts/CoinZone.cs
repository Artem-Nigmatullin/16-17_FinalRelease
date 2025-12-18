using System;
using UnityEngine;

public class CoinZone : MonoBehaviour
{
    public event Action<CoinZone> Entered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            Entered.Invoke(this);
       }

    }
}
