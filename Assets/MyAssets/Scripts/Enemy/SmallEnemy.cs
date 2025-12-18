using System.Collections;
using UnityEngine;

public class SmallEnemy : MonoBehaviour
{

    [SerializeField] private Effect _effect;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyReferences _enemyReferences;
    [SerializeField] private Enemy _enemy;
    private Transform _targetPlayer;
    private Transform _homePosition;

    private IBehavior _currentBehavior;
    private IBehavior _reactBehavior;
    private Coroutine _runCoroutine;

    public void Initialize(EnemyReferences references)
    {
        _targetPlayer = references.TargetPlayer;
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
    private void Update()
    {
        Run();
    }

    public void Init(EnemyReactBehaviorType reactType)
    {
        _currentBehavior = _spawner.SpawnReactBehavior(EnemyReactBehaviorType.RunAway, this.gameObject, _effect, this.transform);
    }

    private void Run()
    {
        if (_runCoroutine == null)
            _runCoroutine = StartCoroutine(StartRun());
    }
    private IEnumerator StartRun()
    {
        _currentBehavior.Update();
        yield return new WaitForSeconds(0.0001f);
        _runCoroutine = null;
    }


}
