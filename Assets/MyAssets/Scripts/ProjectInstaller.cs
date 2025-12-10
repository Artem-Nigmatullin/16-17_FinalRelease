using System.Collections.Generic;
using UnityEngine;

public class ProjectInstaller : MonoBehaviour
{
    [SerializeField] private List<Spawner> _spawners;
    [SerializeField] private SpawnerDependencies _dependency;
    [SerializeField] private EnemyReferences _enemyReferences;
    [SerializeField] private Enemy enemy;
    [SerializeField] private SmallEnemy _smallEnemy;
    public void Initialize()
    {
        InitSpawners();
        InitEnemies();
        _smallEnemy.Initialize(_enemyReferences);
    }

    private void InitEnemies()=> enemy.Initialize(_enemyReferences);
    
    private void InitSpawners()
    {
        foreach (var spawner in _spawners)
        {
            spawner.InitializeDependency(_dependency);
        }

    }
}
