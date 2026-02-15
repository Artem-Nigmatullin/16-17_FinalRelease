using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

public class SmallEnemy : MonoBehaviour
{

    [SerializeField] private Effect _effect;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyReferences _enemyReferences;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform _targetPlayer;
    private Transform _homePosition;

    private IBehavior _currentBehavior;
    private IBehavior _reactBehavior;
    private Coroutine _runCoroutine;

    public void Initialize(EnemyReferences references)
    {
        //_targetPlayer = references.TargetPlayer;
        _homePosition = references.HomePosition;
    }

    public void Set(IBehavior newBehavior)
    {
        if (_currentBehavior != null)
            _currentBehavior.Exit();
        _currentBehavior = newBehavior;

    }

    public void Attach(Transform item)
    {
        item.SetParent(this.gameObject.transform);
        item.localPosition = new Vector3(0, 2, 0);      // поставить прямо на врага
        item.localRotation = Quaternion.identity;
    }
    private void FixedUpdate()
    {
        Action();
    }
    private void Update()
    {

    }

    public void Init(EnemyReactBehaviorType reactType)
    {
        _currentBehavior = _spawner.SpawnReactBehavior(EnemyReactBehaviorType.RunAway, this.gameObject, _effect, this.transform);
    }


    private void Action()
    {
        if (_targetPlayer == null) return;
        Vector3 playerPos = _targetPlayer.transform.position;
        playerPos.y = transform.position.y;

        float distance = Vector3.Distance(transform.position, playerPos);

        if (distance < 5f)
        {
            Vector3 dir = (transform.position - playerPos).normalized;
            Vector3 runPos = transform.position + dir;
            Move(transform, runPos, 2f);
        }
    }

    public void Move(Transform source, Vector3 position, float _speed)
    {
        source.position = Vector3.MoveTowards(source.transform.position, position, _speed * Time.fixedDeltaTime);
    }


}
