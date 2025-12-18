using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField ] private Rigidbody _rigidbody;
    [SerializeField] private CharacterMovement _character;
    private void Move() => _character.Move(_rigidbody);
    private void Jump() => _character.Jump(_rigidbody);

    public void ReceiveKey(Transform item)
    {
        //HasKey = true;
        item.SetParent(transform);
        item.localPosition = new Vector3(0, 2, 0);     
        item.localRotation = Quaternion.identity;
    }
    private void Update()
    {
        Move();
        Jump();
    }
}


