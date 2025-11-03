using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IAgroObserver
{
    private GameObject _gameObject;
    [SerializeField] private Transform _target;
    [SerializeField] Spawner _spawner;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private List<Transform> _points;

    private IBehavior _currentBehavior;
    private IBehavior _idleBehavior;
    private IBehavior _reactBehavior;

    [Header("Текущее под-состояние Idle")]
    [SerializeField] private EnemyIdleBehaviorType _currentIdleType;
    private EnemyIdleBehaviorType _previousIdleType;

    [Header("Текущее под-состояние React")]
    [SerializeField] private EnemyReactBehaviorType _currentReactType;
    private EnemyReactBehaviorType _previousReactType;

    private event Action behaviorChanged;
    [SerializeField] private AggrZone _aggrZone;

    public bool IsChangedIdleStateInInspector => _currentIdleType != _previousIdleType;
    public bool IsChangedReactStateInInspector => _currentReactType != _previousReactType;

    public void Awake()
    {
        _currentIdleType = EnemyIdleBehaviorType.Stand;

    }
    public void Initialize()
    {
        _currentIdleType = EnemyIdleBehaviorType.Stand;
    }
    private void Start()
    {
        var groups = _points.OrderByDescending(group => group.name);

        foreach (Transform group in groups)
        {
            DevLog.Log($"group:{group.position}");
        }
    }

    private void Update()
    {

        _currentBehavior?.Update();

        ChangeStateAllEnemies();

        ChangeStateInInspector();

    }

    private void ChangeStateAllEnemies()
    {
        if (Input.GetKey(KeyCode.Y))
        {
            ChangeIdleSubState(EnemyIdleBehaviorType.RandomWalk);
            return;
        }

        if (Input.GetKey(KeyCode.I))
        {
            
            ChangeReactSubState(EnemyReactBehaviorType.Chase);
            return;
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
    public void SetTarget(Transform target) => _target = target;

    public void Init(EnemyIdleBehaviorType idleType, EnemyReactBehaviorType reactType)
    {

        _idleBehavior = _spawner.CreateIdleBehavior(this, idleType, _points);
        _reactBehavior = _spawner.CreateReactBehavior(this, reactType);

        _currentIdleType = idleType;
        _previousIdleType = _currentIdleType;

        _currentReactType = reactType;
        _previousReactType = _currentReactType;


        SetIdleBehavior();
    }

    public void ChangeIdleSubState(EnemyIdleBehaviorType newType)
    {

        _currentBehavior?.Exit();
        _idleBehavior = _spawner.CreateIdleBehavior(this, newType, _points);
        _currentBehavior = _idleBehavior;
        _currentBehavior.Enter();
        Debug.Log($"{name}: сменил состояние на {newType}");
    }

    public void ChangeReactSubState(EnemyReactBehaviorType newType)
    {

        _currentBehavior?.Exit();
        _reactBehavior = _spawner.CreateReactBehavior(this, newType);
        _currentBehavior = _reactBehavior;
        _currentBehavior.Enter();
        Debug.Log($"{name}: сменил состояние на {newType}");
    }

    public void SetIdleBehavior()
    {
        if (_idleBehavior == null)
            Debug.LogError($"{name}: idleBehavior не установлен!");
        else
            _currentBehavior = _idleBehavior;
        _currentBehavior?.Enter();
    }

    public void SetReactBehavior()
    {
        if (_reactBehavior == null)
            Debug.LogError($"{name}: ReactBehavior не установлен!");
        else
            _currentBehavior?.Exit();
            _currentBehavior = _reactBehavior;
        _currentBehavior?.Enter();
    }

    public void OnExit(GameObject player)
    {
        _gameObject = player;
        DevLog.Log($"{gameObject}:в зоне ни кого");

    }
    public void OnEntered(GameObject player)
    {
        _gameObject = player;
        DevLog.Log($"{gameObject} вижу врага в зоне");
    }

    public void ClearTarget() => _target = null;



}



