using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{

    [SerializeField] private EnemyIdleBehaviorType _idleType;
    [SerializeField] private EnemyReactBehaviorType _reactType;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    private Enemy _lastEnemy;
    private List<Enemy> enemies;
    private Vector3 _spawnOffset;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private Transform _target;
    [SerializeField] private Effect _effect;
    private CharactersFactory _charactersFactory=new CharactersFactory();

    public void Initialize()
    {
        enemies = new List<Enemy>();
        _spawnOffset = new Vector3(0, 1, 0);
    }

    private void Start()
    {
        SpawnEnemy();
        SelectBehavior();
    }

    public void SelectBehavior() => _lastEnemy?.Init(_idleType, _reactType);

    public IBehavior CreateIdleBehavior(Enemy enemy, EnemyIdleBehaviorType type, List<Transform> points)
    {
        switch (type)
        {
            case EnemyIdleBehaviorType.Stand: return new StandBehavior(enemy.transform, _homePosition, this);
            case EnemyIdleBehaviorType.Patrol: return new PatrolBehavior(enemy.transform, points);
            case EnemyIdleBehaviorType.RandomWalk: return new RandomWalkBehavior(enemy.transform, this);
            default:
                Debug.LogError($"Неизвестный Idle: {type}");
                return new StandBehavior(enemy.transform, _homePosition, this);
        }

    }
    public void SpawnEnemy()
    {
        _lastEnemy = _charactersFactory.CreateEnemy(_enemyPrefab, _spawnPoint.position, _spawnOffset);
        enemies?.Add(_lastEnemy);


    }

    public IBehavior CreateReactBehavior(Enemy enemy, EnemyReactBehaviorType type, NavMeshAgent navMesh = null)
    {
        switch (type)
        {
            case EnemyReactBehaviorType.RunAway: return new RunAwayBehavior(enemy.transform, _target);
            case EnemyReactBehaviorType.Chase:
                return navMesh != null
                ? new ChaseBehavior(navMesh, _target)
                : new ChaseBehavior(enemy.transform, _target);
            case EnemyReactBehaviorType.Die: return new DieBehavior(enemy.gameObject, _effect);
            default:
                Debug.LogError($"Неизвестный React: {type}");
                return new RunAwayBehavior(enemy.transform, _target);
        }
    }

    public Enemy CreateEnemy()
    {

        _lastEnemy = Instantiate(_enemyPrefab, _spawnPoint.position + _spawnOffset, Quaternion.identity);
        

        return _lastEnemy;
    }


}

