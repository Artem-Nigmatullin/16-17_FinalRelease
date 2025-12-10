using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour,IInteractable
{
    public void Off() => gameObject.SetActive(false);

    public void On() => gameObject.SetActive(true);
}
