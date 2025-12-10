using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField] private CoinZone _coinZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            _coinZone.Coin.On();
           Destroy(_coinZone.KeyObject.gameObject);
           Destroy(gameObject);
         // _coinZone.LightningEffect.gameObject.SetActive(false);
                
        }

    }
}
