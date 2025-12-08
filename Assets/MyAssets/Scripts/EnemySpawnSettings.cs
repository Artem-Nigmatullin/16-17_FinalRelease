using UnityEngine;

[CreateAssetMenu(menuName = "Settings/Enemy Spawn Settings")]
public class EnemySpawnSettings : ScriptableObject
{
    public Effect Effect;
    public float GroundOffset = 1f;
}
