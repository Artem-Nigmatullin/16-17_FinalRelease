using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDependencies : MonoBehaviour
{
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private CoinCollector _collector;
    [SerializeField] private Effect _effect;
    [SerializeField] private AggrZone _aggrZone;
}
