using UnityEngine;


public class Coin : MonoBehaviour,IInteractable
{
    public void Off() => gameObject.SetActive(false);


    public void On()=>gameObject.SetActive(true);


   
    
}
