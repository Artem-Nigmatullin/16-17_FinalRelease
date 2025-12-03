using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 本クラスは、敵キャラクターの生成と行動パターンの設定を管理します。
/// </summary>
/// <remarks>
/// 主な機能:
/// - 新しい敵キャラクターを生成し、管理リストに追加
/// - 生成した敵に対して待機行動や反応行動を割り当て
/// - 生成位置や移動ポイントをランダムに調整
/// 
/// 使用方法:
/// - ゲーム内で敵キャラクターを出現させ、行動パターンを設定する際に使用します。
/// - 管理者や他の開発者は、このクラスを通じて敵の生成状況や行動設定を把握できます。
/// </remarks>
public class Spawner : MonoBehaviour
{
    [SerializeField] private EnemyIdleBehaviorType _idleType;
    [SerializeField] private EnemyReactBehaviorType _reactType;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private Effect _effect;
    [SerializeField] private Transform _player;
    private Enemy _lastEnemy;
    private List<Enemy> enemies = new();
    private CharactersFactory _charactersFactory = new CharactersFactory();
    private float _ground = 1;

    public List<Enemy> Enemies { get => enemies; set => enemies = value; }

    private void Start()
    {
        SpawnEnemy(_spawnPoint);
        SelectBehavior();
    }
    public void Update()
    {
        DeleteCloneInGame();
    }

    public void DeleteCloneInGame()
    {
        if (gameObject.activeInHierarchy)
            _enemyPrefab.gameObject.SetActive(false);
    }
    private Vector3 SetRandomPosition() => new Vector3(Random.Range(2, -2), transform.position.y + _ground, Random.Range(2, -1));

    public void SelectBehavior() => _lastEnemy?.Init(_idleType, _reactType);

    public void SpawnEnemy(Transform spawnPoint)
    {
        if (spawnPoint is null)
        {
            throw new System.ArgumentNullException(nameof(spawnPoint));
        }

        spawnPoint.position += SetRandomPosition();
        Vector3 spawnOffset = new Vector3(0, 1, 0);
        _lastEnemy = _charactersFactory.CreateEnemy(spawnPoint.position, spawnOffset, _enemyPrefab);
        Enemies?.Add(_lastEnemy);
    }

    public IBehavior SpawnIdleBehavior(
        EnemyIdleBehaviorType type,
        Enemy enemy,
        List<Transform> points)
    {
        return _charactersFactory.CreateIdleBehavior(type, enemy, points, _homePosition);

    }

    public IBehavior SpawnReactBehavior(
        EnemyReactBehaviorType type,
        Enemy enemy,
        Effect effect,
        Transform enemyPosition,
        NavMeshAgent navMesh = null)
    {

        return _charactersFactory.CreateReactBehavior(type, enemy, effect, enemyPosition, _player, navMesh);
    }
}

