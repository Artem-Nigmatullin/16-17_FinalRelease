using System.Collections;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] private float _amplitude = 1;
    [SerializeField] private float _frequency = 1;
    [SerializeField] private float _phase = 1;
    [SerializeField] private Transform _cube;
    [SerializeField] private float _rotationSpeed = 500f;
    private float time = 0;
    private Coroutine coroutineRotate;

    private void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            UseRotateProcess();
        }
    }

    private void UseRotateProcess()
    {
        coroutineRotate = StartCoroutine(ProcessRotateAnimation(transform));
    }
    private IEnumerator ProcessMove()
    {
        
        float elapsed = 0;
        while (true)
        {
            time = Time.time;
            float yPos = _amplitude * Mathf.Sin(time * _frequency + _phase);
            elapsed += Time.deltaTime;
            Debug.Log($"elapsed 2: {elapsed:F2} секунд");
            transform.position = new Vector3(transform.position.x, yPos, transform.position.z);
         
            yield  return null;
        }
    }
    private IEnumerator ProcessRotateAnimation(Transform cube)
    {
        float animationTime = 1f;
        Debug.Log($"timeScale: {Time.timeScale}");
       // StartCoroutine(ProcessMove());

        while (true)
        {
            float elapsed = 0;
            // float tick = Time.deltaTime;

            while (elapsed < animationTime)
            {
                // cube.transform.rotation = Quaternion.Lerp(startRotation, endRotation, Mathf.PingPong(progress, 1f));
                cube.Rotate(Vector3.up * _rotationSpeed, Space.Self);
                elapsed += Time.deltaTime;
                Debug.Log($"elapsed 1: {elapsed:F2} ");
                // yield return StartCoroutine(ProcessMove()); // ждём следующий кадр
                yield return null;
            }
            Debug.Log("立った時間");

            yield return null;


        }

    }
}
