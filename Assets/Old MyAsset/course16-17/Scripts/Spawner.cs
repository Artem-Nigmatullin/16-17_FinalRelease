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
    private List<Transform> enemies = new();
    private Vector3 _spawnOffset;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private Transform _target;
    [SerializeField] private List<Transform> _targetsAll;
    [SerializeField] private Effect _effect;
    private CharactersFactory _charactersFactory = new CharactersFactory();
    private Vector3 random;
    public void Initialize()
    {

        _spawnOffset = new Vector3(0, 1, 0);

    }

    private void Start()
    {
        int enemyCount = 2;

        for (int i = 0; i < enemyCount; i++)
        {
            random = new Vector3(
               Random.Range(2, 10),
               transform.position.y,
               Random.Range(2, 14));
          //  _spawnPoint.position += random;

            SpawnEnemy(_spawnPoint);
            _targetsAll = enemies;
            SelectBehavior();
        }
    }

    public void SelectBehavior() => _lastEnemy?.Init(_idleType, _reactType);

    public void SpawnEnemy(Transform spawnPoint)
    {
        _lastEnemy = _charactersFactory.CreateEnemy(_enemyPrefab, spawnPoint.position+random, _spawnOffset);
        enemies?.Add(_lastEnemy.transform);


    }
    public IBehavior SpawnIdleBehavior(
        EnemyIdleBehaviorType type,
        Enemy enemy,
        List<Transform> points)
    {
        return _charactersFactory.CreateIdleBehavior(type, enemy, _homePosition, points);

    }

    public IBehavior SpawnReactBehavior(
        EnemyReactBehaviorType type,
        Enemy enemy,
        Transform targetPlayer,
        Transform enemyPosition,
        NavMeshAgent navMesh = null)
    {

        return _charactersFactory.CreateReactBehavior(type, enemy, targetPlayer, _effect, enemyPosition, navMesh);
    }

}

