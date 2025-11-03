using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField ] private Rigidbody _rigidbody;
    [SerializeField] private CharacterMovement _character;

    private void Move() => _character.Move(_rigidbody);
    private void Jump() => _character.Jump(_rigidbody);

    private void Update()
    {
        Move();
        Jump();
    }
}


