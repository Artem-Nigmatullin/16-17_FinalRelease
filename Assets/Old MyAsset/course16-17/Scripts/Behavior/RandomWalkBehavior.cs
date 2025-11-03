using System.Collections;
using UnityEngine;

public class RandomWalkBehavior : IBehavior
{
    private const float SPEED = 2f;
    private Vector3 _targetPosition;
    private bool _hasTarget = false;
    private float _distance;
    private Coroutine _routine;
    private Movement _movement=new Movement();
    private readonly MonoBehaviour _owner;
    private readonly Transform _source;
    public string Name => "random";

    public RandomWalkBehavior(Transform source, MonoBehaviour owner)
    {
        _source = source;
        _owner = owner;
    }

    public void Enter()
    {

        Debug.Log($"{_source.name} начинает гулять");
    }

    private IEnumerator WalkRoutine()
    {
        while (true)
        {
            Vector3 target = new Vector3(Random.Range(-3f, 3f), _source.position.y, Random.Range(-3f, 3f));

            while (Vector3.Distance(_source.position, target) > 0.05f)
            {
                _source.position = Vector3.MoveTowards(_source.position, target, SPEED * Time.deltaTime);
                yield return null;
            }

            // Немного подождать перед следующей точкой (опционально)
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void Update()
    {
        //if (_routine != null)
        //    _owner.StopCoroutine(_routine);

        //_routine = _owner.StartCoroutine(WalkRoutine());

        _distance = Vector3.Distance(_source.transform.position, _targetPosition);
        if (_hasTarget == false || _distance < 0.1f)
        {
            _targetPosition = new Vector3(Random.Range(-3f, 3f), _source.transform.position.y, Random.Range(-3f, 3f));
            _hasTarget = true;
            Debug.Log($"{_source.name} гуляет по точке {_targetPosition}");
        }
        _movement.Move(_source, _targetPosition, SPEED);

    }

    public void Exit()
    {
        if (_routine != null)
            _owner.StopCoroutine(_routine);

        Debug.Log($"{_source.name} завершил прогулку");
    }
}
