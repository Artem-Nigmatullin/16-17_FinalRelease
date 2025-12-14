using UnityEngine;


public class Coin : MonoBehaviour,ISwitcher
{
    public void Off() => gameObject.SetActive(false);


    public void On()=>gameObject.SetActive(true);


   
    
}
