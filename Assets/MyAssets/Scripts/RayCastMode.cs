using UnityEngine;

public class RayCastMode : MonoBehaviour
{
    [SerializeField] private Transform _sourceRay;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _rayLength = 5f;
    private IInteractable _interactable;

    private void Update()
    {
        CreateRaycast();
    }
    private Vector3 GetPointWithMouse() => Input.mousePosition;
    private bool isHit(Ray ray, out RaycastHit hitInfo)
    {
        bool result = Physics.Raycast(ray, out hitInfo);
        Debug.DrawLine(ray.origin, ray.direction * 100f, Color.yellow);
        return false;
    }

    private bool IsInteractable(RaycastHit hit)
    {
        _interactable = hit.collider.GetComponent<IInteractable>();
        return _interactable != null;
    }
    private void CreateRaycast()
    {
        Vector3 point = GetPointWithMouse();
        Ray cameraRay = Camera.main.ScreenPointToRay(point);

        if (isHit(cameraRay, out RaycastHit hit) && IsInteractable(hit))
        {
            if (Input.GetMouseButton(0))
            {

                Transform target = hit.collider.transform;
                target.gameObject.SetActive(false);

            }

        }

        Physics.RaycastAll(cameraRay);
        Collider[] colliders = Physics.OverlapSphere(Vector2.zero, 10);

    }
    public void CreateRayLine()
    {
        Quaternion rotation = Quaternion.Euler(_rotation);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 mouseWorldPosition = Camera.main.WorldToScreenPoint(mousePos);

        Debug.DrawLine(_sourceRay.position, mouseWorldPosition * _rayLength, Color.yellow);
    }
    private void OnGUI()
    {
        int size = 12;
        float posX = Camera.main.pixelWidth / 2 - size / 2;
        float posY = Camera.main.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}
