using UnityEngine;

public class SpawnerSceneReference : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private Transform _player;

    public Transform Player { get => _player; private set => _player = value; }
    public Transform HomePosition
    {
        get => _homePosition;
        set
        {
            if (value == null)
            {
                Debug.LogError("Trying to set HomePosition to NULL!");
                return;
            }

            _homePosition = value;
        }
    }
    public Transform SpawnPoint
    {
        get => _spawnPoint;
        set
        {
            if (value == null)
            {
                Debug.LogError("Trying to set SpawnPoint to NULL!");
                return;
            }

            _spawnPoint = value;
        }
    }
}
