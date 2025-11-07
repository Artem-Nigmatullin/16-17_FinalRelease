using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharactersFactory
{
    public Enemy CreateEnemy(
        Enemy enemyPrefab,
        Vector3 spawnPosition,
        Vector3 spawnOffset)
    {
        Enemy enemy = Object.Instantiate(enemyPrefab, spawnPosition + spawnOffset, Quaternion.identity);

        return enemy;
    }

    public IBehavior CreateIdleBehavior(
        EnemyIdleBehaviorType type,
        Enemy enemy,
        Transform homePosition,
        List<Transform> points)
    {
        switch (type)
        {
            case EnemyIdleBehaviorType.Stand: return new StandBehavior(enemy.transform, homePosition);
            case EnemyIdleBehaviorType.Patrol: return new PatrolBehavior(enemy.transform, points);
            case EnemyIdleBehaviorType.RandomWalk: return new RandomWalkBehavior(enemy.transform);
            default:
                Debug.LogError($"Неизвестный Idle: {type}");
                return new StandBehavior(enemy.transform, homePosition);
        }

    }

    public IBehavior CreateReactBehavior(
        EnemyReactBehaviorType type,
        Enemy enemy,
        Transform player,
        Effect _effect,
       Transform source,
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
                Debug.LogError($"Неизвестный React: {type}");
                return new RunAwayBehavior(enemy.transform, player);
        }
    }
}
