using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class SimpleCameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private Player _player;
    [SerializeField] private Transform _source; // ссылка на персонажа
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -7); // смещение камеры
    [SerializeField] private float smoothSpeed = 5f; // скорость плавного следования

    private Vector3 _cameraForward;
    private Vector3 _cameraRight;
    public Vector3 CameraForward
    {
        get { return _cameraRight; }
        set { _cameraRight = value; }
    }
    public Vector3 CameraRight
    {
        get { return _cameraForward; }
        set { _cameraForward = value; }
    }

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

        //Vector3 desiredPosition = _source.position + offset;
        //transform.position = desiredPosition;

        //Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.2f); // плавный поворот
     
    }
}
