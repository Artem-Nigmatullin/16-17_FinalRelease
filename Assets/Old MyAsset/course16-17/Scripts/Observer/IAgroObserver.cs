using UnityEngine;

public interface IAgroObserver
{
    void OnEntered( GameObject player);
    void OnExit( GameObject player);
}
