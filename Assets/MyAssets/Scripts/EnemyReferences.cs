using UnityEngine;
using UnityEngine.AI;

public class EnemyReferences : MonoBehaviour
{
    [SerializeField] private Transform _targetPlayer;
    [SerializeField] private AggrZone _aggrZone;
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Transform _homePosition;
    [SerializeField] private CoinCollector _collector;

    public Transform TargetPlayer { get => _targetPlayer; set => _targetPlayer = value; }
    public AggrZone AggrZone { get => _aggrZone; set => _aggrZone = value; }
    public NavMeshAgent Agent { get => _agent; set => _agent = value; }
    public Transform HomePosition { get => _homePosition; set => _homePosition = value; }
    public CoinCollector Collector { get => _collector; set => _collector = value; }
}
