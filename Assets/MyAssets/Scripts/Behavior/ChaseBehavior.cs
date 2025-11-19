using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : IBehavior
{
    const float SPEED = 3f;
    private float _distance;
    private readonly NavMeshAgent _navMesh;
    private List<Transform> _sources=new List<Transform>();
    private readonly Transform _source;
    private readonly Transform _target;
    private Movement _movement = new Movement();
    public string Name => "name chase";
    public ChaseBehavior(List <Transform>sources ,Transform target)
    {
        _sources = sources;
        _target = target;
    }

    public ChaseBehavior(Transform source, Transform target)
    {
       
        _source = source;
        _target = target;
    }

    public ChaseBehavior(NavMeshAgent navMesh, List<Transform> sources)
    {
        _navMesh = navMesh;
        _sources = sources;
    }


    public ChaseBehavior(NavMeshAgent navMesh, Transform source)
    {
        _navMesh = navMesh;
        _source = source;
    }
    private void MoveCharacterWithDifferentDistance()
    {

        _distance = Vector3.Distance(_source.position, _target.position);
        if (_distance > 0.1)
        {
            Vector3 playerPos = _target.position;
            playerPos.y = _source.transform.position.y;

            Vector3 dir = (playerPos - _source.transform.position).normalized;
            Vector3 chasePos = _source.transform.position + dir;
            _movement.Move(_source, chasePos, SPEED);
        }


    }

    private void AllMoveCharacterWithDifferentDistance()
    {

        foreach (Transform sources in _sources)
        {
            _distance = Vector3.Distance(sources.position, _target.position);
            if (_distance > 0.1)
            {
                Vector3 playerPos = _target.position;
                playerPos.y = sources.transform.position.y;

                Vector3 dir = (playerPos - sources.transform.position).normalized;
                Vector3 chasePos = sources.transform.position + dir;
                _movement.Move(sources, chasePos, SPEED);
            }

        }
    }
    public void MoveCharacterWithNavMesh()
    {
        foreach (Transform source in _sources)
            _navMesh.SetDestination(source.position);

    }
    public void Enter() { }


    public void Update()
    {
        if (_navMesh != null)
        {
            MoveCharacterWithNavMesh();
        }
        else
        {
           // AllMoveCharacterWithDifferentDistance();
            MoveCharacterWithDifferentDistance();
        }
        //MoveCharacterWithDifferentDistance();
    }

    public void Exit()
    {

    }
}
