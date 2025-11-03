using UnityEngine;
using UnityEngine.AI;

public class ChaseBehavior : IBehavior
{
    const float SPEED = 3f;
    private float _distance;
    private readonly NavMeshAgent _navMesh;
    private readonly Transform _source;
    private readonly Transform _target;
    private Movement _movement=new Movement();
    public string Name => "name chase";
    public ChaseBehavior( Transform source, Transform target)
    {
        _source = source;
        _target = target;
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
           _movement.Move(_source,chasePos, SPEED);
        }
    }
    public void MoveCharacterWithNavMesh()
    {
        _navMesh.SetDestination(_source.position);
   
    }
    public void Enter()
    {
       
        Debug.Log($"{_source.name} догоняет!");
    }
 

    public void Update()
    {
        if (_navMesh != null)
        {
            MoveCharacterWithNavMesh();
        }
        else
        {
            MoveCharacterWithDifferentDistance();
        }
        //MoveCharacterWithDifferentDistance();
    }

    public void Exit()
    {

    }
}
