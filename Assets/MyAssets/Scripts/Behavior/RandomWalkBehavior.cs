using System.Collections;
using UnityEngine;

public class RandomWalkBehavior : IBehavior
{
    private const float SPEED = 2f;
    private Vector3 _targetPosition;
    private bool _hasTarget = false;
    private float _distance;
    private Coroutine _routine;
    private Movement _movement = new Movement();
    private readonly MonoBehaviour _owner;
    private readonly Transform _source;

    public string Name => "random";

    public RandomWalkBehavior(Transform source)=>  _source = source;
    
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

            yield return new WaitForSeconds(0.5f);
        }
    }

    public void Update()
    {
       
        _distance = Vector3.Distance(_source.transform.position, _targetPosition);
        if (_distance < 0.1f)
        {
            _targetPosition = new Vector3(Random.Range(-3f, 3f), _source.transform.position.y, Random.Range(-3f, 3f));
            Debug.Log($"{_source.name} гуляет по точке {_targetPosition}");
        }
       
        _movement.Move(_source, _targetPosition, SPEED);

    }
    public void Exit() { }
}
