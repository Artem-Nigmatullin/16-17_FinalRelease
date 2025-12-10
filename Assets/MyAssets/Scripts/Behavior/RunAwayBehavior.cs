using UnityEngine;

public class RunAwayBehavior : IBehavior
{
    const float SPEED = 5f;
    private float _distance;
    private readonly Movement _movement = new Movement();
    private Transform _target;
    private readonly Transform _source;
    public string Name => "name run away";
    public RunAwayBehavior(Transform source, Transform target)
    {
        _source = source;
        _target = target;

    }

    public void Enter() { }

    public void Update()
    {
        Vector3 playerPos = _target.transform.position;
        _distance = Vector3.Distance(_source.transform.position, playerPos);
        if (_distance < 5)
        {
            playerPos.y = _source.transform.position.y;
            Vector3 dir = (_source.transform.position - playerPos).normalized;
            Vector3 runPos = _source.transform.position + dir;
            _movement.Move(_source, runPos, SPEED);
        }
    }

    public void Exit() { }
}
