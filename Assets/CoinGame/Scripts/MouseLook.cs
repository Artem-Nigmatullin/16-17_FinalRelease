using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Transform _source;
    [SerializeField] private float _mouseSensitivity = 1f;
    [SerializeField] private float _xRotation = 0f;
    [SerializeField] private float _minValue = -160f;
    [SerializeField] private float _maxValue = 160f;
    [SerializeField] private RayCastMode _raycast;
    private Camera _camera;
    private float _mouseX;
    private float _mouseY;

    private void Start()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        SetCharacterRotation();
    }

    private void SetCharacterRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        _mouseX = mouseDelta.x * _mouseSensitivity * Time.deltaTime;
        _mouseY = mouseDelta.y * _mouseSensitivity * Time.deltaTime;
        Vector3 sourceDirection = new Vector3(_mouseX,_mouseY,0);
        _xRotation -= _mouseY;
        _xRotation = Mathf.Clamp(_xRotation, _minValue, _maxValue);
        _source.Rotate(Vector3.up * _mouseX);
    }
}
