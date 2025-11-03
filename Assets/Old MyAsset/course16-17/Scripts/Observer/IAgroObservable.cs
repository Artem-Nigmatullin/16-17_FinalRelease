using System;
using UnityEngine;

public interface IAgroObservable 
{
    event Action<GameObject> Entered;
 
}
