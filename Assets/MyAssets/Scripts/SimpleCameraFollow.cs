using UnityEngine;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _source; 
    [SerializeField] private float smoothSpeed = 5f; 
    private Vector3 _cameraForward;
    private Vector3 _cameraRight;

    public Vector3 CameraForward { get => _cameraForward; set => _cameraForward = value; }
    public Vector3 CameraRight { get => _cameraRight; set => _cameraRight = value; }

    private void GetCameraPosition()
    {
        CameraForward = _camera.forward;
        _cameraForward.y = 0;
        _cameraForward.Normalize();

        CameraRight = _camera.right;
        _cameraRight.y = 0;
        _cameraRight.Normalize();
    }
    private void LateUpdate()
    {
        transform.LookAt(_source);
    }

    private void Update()
    {
        GetCameraPosition();
        if (_source == null) return;
    }
}
