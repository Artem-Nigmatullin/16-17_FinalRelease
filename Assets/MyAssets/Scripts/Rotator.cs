using System.Collections;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Coroutine coroutineRotate;
    [SerializeField] private Transform _cube;
    [SerializeField] private float _rotationSpeed = 2f;

    public IEnumerator ProcessAnimation(Transform cube)
    {
        float progress = 0.3f;
        float animationTime = 1.4f;

        Vector3 startScale = cube.localScale;
        while (true)
        {
            progress += Time.deltaTime;
            cube.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }
}
