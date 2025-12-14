using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private CoinZone _coinZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            _coinZone.Coin.On();
           _coinZone.KeyObject.gameObject.SetActive(false);
           _coinZone.LightningEffect.Off();
           Destroy( _coinZone.SmallEnemy.gameObject);
           Destroy(gameObject);
                
        }

    }
}
