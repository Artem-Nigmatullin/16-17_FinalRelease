using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : IBehavior
{
    private const float _speed = 2f;
    private Queue<Vector3> _pointQueue;
    private Vector3 _currentPoint;
    private float _minDistanceToTarget = 0.1f;
    private List<Transform> _points;
    private bool _initialized = false;
    private readonly Transform _source;
    private Movement _movement = new Movement();
    public string Name => "patrol";

    public PatrolBehavior(Transform source, List<Transform> points)
    {
        _source = source;
        _points = points;
        GetPatrolPoints(points);
    }

    public void InitRoute(List<Transform> points)
    {

        _points = points;
        _pointQueue = new Queue<Vector3>();
        foreach (var point in _points)
        {
            _pointQueue.Enqueue(point.position);
        }
        _currentPoint = _pointQueue.Dequeue();
        _pointQueue.Enqueue(_currentPoint);
    }

    public void Enter()
    {
        Debug.Log("начал патрулирование ");

    }


    private void GetPatrolPoints(List<Transform> points)
    {
        InitRoute(points);
        Debug.LogError($"get patrol ");

    }

    public void Update()
    {
        //if (_initialized == false) return;

        Vector3 direction = _currentPoint - _source.transform.position;

        if (direction.magnitude <= _minDistanceToTarget)
        {
            _currentPoint = _pointQueue.Dequeue();
            _pointQueue.Enqueue(_currentPoint);

        }
        _movement.Move(_source, _currentPoint, _speed);
    }

    public void Exit()
    {
        Debug.Log("Завершил прогулку");
    }
}
