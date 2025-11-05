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


}
