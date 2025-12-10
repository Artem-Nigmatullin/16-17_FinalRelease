using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private Transform _targetPlayer;
    private NavMeshAgent _agent;
    private Transform _homePosition;
    private AggrZone _aggrZone;
    [SerializeField] private Effect _effect;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyReferences _enemyReferences;
    [SerializeField] private List<Transform> _points;

    [Header("current state Idle")]
    [SerializeField] private EnemyIdleBehaviorType _currentIdleType;
    [Header("current state  React")]
    [SerializeField] private EnemyReactBehaviorType _currentReactType;

    private EnemyIdleBehaviorType _previousIdleType;
    private EnemyReactBehaviorType _previousReactType;
    private IBehavior _currentBehavior;
    private IBehavior _idleBehavior;
    private IBehavior _reactBehavior;
    private IBehavior _forCollectCoinBehavior;
    private IBehavior _dieBehavior;

    private event Action behaviorChanged;
    private bool NotStartCollectCoin() => _forCollectCoinBehavior == null;

    private float _dist = 0;
    private bool IsChangedIdleStateInInspector => _currentIdleType != _previousIdleType;
    private bool IsChangedReactStateInInspector => _currentReactType != _previousReactType;

    private bool IsEnteredPlayer;

    private void Awake()
    {
        _aggrZone= _enemyReferences.AggrZone;
    }
    public void Initialize(EnemyReferences references)
    {
        _targetPlayer = references.TargetPlayer;
        _currentIdleType = EnemyIdleBehaviorType.Stand;
        _agent = references.Agent;
        _homePosition = references.HomePosition;
    }
    private void Start()
    {
        var groups = _points.OrderByDescending(group => group.name);

        foreach (Transform group in groups)
        {
            DevLog.Log($"group:{group.position}");
        }

    }

    
    private bool IsSafetyDistance()
    {

        if (_dist >= 2)
        {
            return false;
        }
        return true;

    }

    public void Dead()
    {
        Destroy(gameObject);
    }
    public void Update()
    {
        _dist = _aggrZone.GetDistance();

        if (IsSafetyDistance() == false)
        {

            if (NotStartCollectCoin())
            {
                _currentBehavior?.Update();
            }
            _forCollectCoinBehavior?.Update();
        }
        else
        {
            DamagePlayerForExplosion();
            _dieBehavior?.Update();

        }

        ChangeStateAllEnemies();

        ChangeStateInInspector();

    }

    private void ChangeStateAllEnemies()
    {

        if (Input.GetKey(KeyCode.Y))
        {
            _forCollectCoinBehavior = null;
            ChangeIdleSubState(EnemyIdleBehaviorType.RandomWalk);

        }

        if (Input.GetKey(KeyCode.I))
        {
            _forCollectCoinBehavior = null;
            ChangeReactSubState(EnemyReactBehaviorType.Chase);

        }
    }
    private void ChangeStateInInspector()
    {

        if (IsChangedIdleStateInInspector)
        {
            ChangeIdleSubState(_currentIdleType);
            _previousIdleType = _currentIdleType;
            return;
        }

        if (IsChangedReactStateInInspector)
        {
            ChangeReactSubState(_currentReactType);
            _previousReactType = _currentReactType;
            return;
        }
    }
    public void Set(IBehavior behavior)
    {
        _currentBehavior = behavior;
    }
    public void SetTarget(Transform target) => _targetPlayer = target;


    public void ChangeChaseState()
    {
        _forCollectCoinBehavior = _spawner.SpawnReactBehavior(EnemyReactBehaviorType.Chase, this.gameObject, _effect, this.transform);

    }

    private void DamagePlayerForExplosion()
    {
        _dieBehavior = _spawner.SpawnReactBehavior(EnemyReactBehaviorType.Die, this.gameObject, _effect, this.transform);
    }
    public void Init(EnemyIdleBehaviorType idleType, EnemyReactBehaviorType reactType)
    {
            
        _idleBehavior = _spawner.SpawnIdleBehavior(idleType, this.gameObject, _points);
        _reactBehavior = _spawner.SpawnReactBehavior(reactType, this.gameObject, _effect, this.transform);

        _currentIdleType = idleType;
        _previousIdleType = _currentIdleType;

        _currentReactType = reactType;
        _previousReactType = _currentReactType;


        SetIdleBehavior();

    }

    public void ChangeIdleSubState(EnemyIdleBehaviorType newType)
    {
        _idleBehavior = _spawner.SpawnIdleBehavior(newType, this.gameObject, _points);
        _currentBehavior = _idleBehavior;
        Debug.Log($"{name}:  {newType}に変更");

    }

    public void ChangeReactSubState(EnemyReactBehaviorType newType)
    {


        _reactBehavior = _spawner.SpawnReactBehavior(newType, this.gameObject, _effect, this.transform);
        _currentBehavior = _reactBehavior;
        Debug.Log($"{name}: {newType}に変更");
    }


    public void SetIdleBehavior()
    {

        if (_idleBehavior == null)
            throw new InvalidOperationException("idleBehavior 指定されてません!");
        else
            _currentBehavior = _idleBehavior;

    }

    public void SetReactBehavior()
    {
        if (_reactBehavior == null)
            throw new InvalidOperationException("ReactBehavior 指定されてません!");
        else
            _currentBehavior?.Exit();
        _currentBehavior = _reactBehavior;

    }

    public void ClearTarget() => _targetPlayer = null;



}



