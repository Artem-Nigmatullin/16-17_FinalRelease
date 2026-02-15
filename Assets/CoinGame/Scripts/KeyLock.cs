using System;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public event Action<KeyLock> InsertedKey;
    public event Action<KeyLock> ClearedZone;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            InsertedKey?.Invoke(this);

        }
    }
}
