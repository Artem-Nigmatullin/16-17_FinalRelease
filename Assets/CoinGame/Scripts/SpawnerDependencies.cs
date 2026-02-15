using UnityEngine;

public class SpawnerDependencies : MonoBehaviour
{
    public CharactersFactory CharactersFactory=new CharactersFactory();
    [SerializeField] public SpawnerSceneReference SpawnerSceneReference;
    [SerializeField] public EnemySpawnSettings  EnemySpawnSettings;
}
