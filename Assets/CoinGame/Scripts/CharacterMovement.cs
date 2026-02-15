using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private SimpleCameraFollow _cameraFollow;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private InputHandler _input;
    private Vector3 _jumpPower = new Vector3(0, 23, 0);

    public void Move(Rigidbody rb)
    {
        Vector3 moveDirection = Vector3.zero;

        if (_input.PressAKey) moveDirection -= _cameraFollow.CameraRight;
        if (_input.PressDKey) moveDirection += _cameraFollow.CameraRight;
        if (_input.PressWKey) moveDirection += _cameraFollow.CameraForward;
        if (_input.PressSKey) moveDirection -= _cameraFollow.CameraForward;

        moveDirection.y = 0;
        moveDirection.Normalize();
        OnAppliedForceForMove(rb, moveDirection);
       
    }
    public void Jump(Rigidbody rb)
    {
        if (_input.PressSpaceKey)
            ApplyForceForJump(rb, _jumpPower);
    }
    private void OnAppliedForceForMove(Rigidbody rb, Vector3 direction) => rb.MovePosition(rb.position + direction*_moveSpeed * Time.fixedDeltaTime);
    private void ApplyForceForJump(Rigidbody rb, Vector3 direction) => rb.AddForce(direction , ForceMode.Impulse);

    public void MoveToWithoutPhysics(Transform source, Vector3 position, float _speed)
    {
        source.transform.position = Vector3.MoveTowards(source.transform.position, position, _speed * Time.deltaTime);
    }
}
