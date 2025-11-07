using System.Collections;
using UnityEngine;

public class StandBehavior : IBehavior
{
    private Vector3 _currentPosition;
    private Vector3 _homePosition;
    private Vector3 randomOffset;
    private bool _hasTarget = false;
    private float distance;
    private Coroutine _routine;
    private const float SPEED = 1f;
    private readonly MonoBehaviour _owner;
    private readonly Transform _source;
    private readonly Transform _target;
    private readonly Movement _movement=new Movement();
    public string Name => "stand";
    public StandBehavior(Transform source, Transform target)
    {

        _source = source;
        _target = target;

    }
    public void Enter()
    {
     

    }


    private IEnumerator StandRoutine()
    {
        while (true)
        {
            // Немного двигаемся вокруг точки (случайное смещение)
            Vector3 offset = new Vector3(Random.Range(0f, 0.5f), 0f, Random.Range(0f, 0.5f));
            Vector3 targetPos = _target.position + offset;

            while (Vector3.Distance(_source.position, targetPos) > 0.05f)
            {
                _source.position = Vector3.MoveTowards(_source.position, targetPos, SPEED* Time.deltaTime);
                yield return null;
            }

            // Подождать немного перед следующим шагом
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }
    public void Update()
    {

        //if (_routine != null)
        //    _owner.StopCoroutine(_routine);

        //_routine = _owner.StartCoroutine(StandRoutine());
        //Debug.Log($"{_source.name} стоит на месте");

        _currentPosition = _source.transform.position;
        _homePosition = _target.position;
        randomOffset = new Vector3(Random.Range(1f, 7f), _currentPosition.y, Random.Range(1f, 7f));
        _homePosition.y = randomOffset.y;

        if (_hasTarget == true && distance > 1f)
        {
            distance = Vector3.Distance(_currentPosition, _homePosition);
            _homePosition.y = _currentPosition.y;
        }
            _hasTarget = true;
       _movement.Move(_source,_homePosition, SPEED);
    }

    public void Exit()
    {
        if (_routine != null)
            _owner.StopCoroutine(_routine);

        Debug.Log($"{_source.name} устал стоять");
    }
}
