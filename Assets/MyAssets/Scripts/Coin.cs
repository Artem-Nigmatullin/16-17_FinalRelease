using UnityEngine;


public class Coin : MonoBehaviour,ISwitcher
{
    public void Hide() => gameObject.SetActive(false);

    public void Show()=>gameObject.SetActive(true);


   
    
}
