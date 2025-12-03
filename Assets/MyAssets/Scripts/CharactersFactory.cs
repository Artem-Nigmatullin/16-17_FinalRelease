using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharactersFactory
{
    private List<Enemy> _enemies = new List<Enemy>();
    public Enemy CreateEnemy(
        Vector3 spawnPosition,
        Vector3 spawnOffset,
        Enemy enemyPrefab)
    {
        Enemy enemy = Object.Instantiate(enemyPrefab, spawnPosition + spawnOffset, Quaternion.identity);
        _enemies.Add(enemy);
        return enemy;
    }

    public IBehavior CreateIdleBehavior(
        EnemyIdleBehaviorType type,
        Enemy enemy,
        List<Transform> points,
         Transform homePosition)
    {
        switch (type)
        {
            case EnemyIdleBehaviorType.Stand: return new StandBehavior(enemy.transform, homePosition);
            case EnemyIdleBehaviorType.Patrol: return new PatrolBehavior(enemy.transform, points);
            case EnemyIdleBehaviorType.RandomWalk: return new RandomWalkBehavior(enemy.transform);
            default:
                Debug.LogError($"error Idle: {type}");
                return new StandBehavior(enemy.transform, homePosition);
        }
    }

    public IBehavior CreateReactBehavior(
        EnemyReactBehaviorType type,
        Enemy enemy,
        Effect _effect,
        Transform source,
        Transform player,
        NavMeshAgent navMesh = null)
    {
        switch (type)
        {
            case EnemyReactBehaviorType.RunAway: return new RunAwayBehavior(enemy.transform, player);
            case EnemyReactBehaviorType.Chase:
                return navMesh != null
                ? new ChaseBehavior(navMesh, source)
                : new ChaseBehavior(source, player);
            case EnemyReactBehaviorType.Die: return new DieBehavior(enemy.gameObject, _effect);
            default:
                Debug.LogError($"error React: {type}");
                return new RunAwayBehavior(enemy.transform, player);

        }
    }
}
