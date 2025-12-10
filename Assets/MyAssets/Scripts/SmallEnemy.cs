using System;
using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;
using Random = UnityEngine.Random;

public class SmallEnemy : MonoBehaviour
{

    private Transform _targetPlayer;
    private Transform _homePosition;
    [SerializeField] private Effect _effect;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private EnemyReferences _enemyReferences;

    private RunAwayBehavior _runAwayBehavior;

    private EnemyReactBehaviorType _previousReactType;
    private IBehavior _currentBehavior;
    private IBehavior _reactBehavior;
    private IBehavior _forCollectCoinBehavior;

    private Coroutine _runCoroutine;
    private event Action behaviorChanged;
    private bool NotStartCollectCoin() => _forCollectCoinBehavior == null;

    private float _dist = 0;

    private bool IsEnteredPlayer;
    public void Initialize(EnemyReferences references)
    {

        _targetPlayer = references.TargetPlayer;
        _homePosition = references.HomePosition;
        _runAwayBehavior = new RunAwayBehavior(this.transform, _targetPlayer.transform);

    }


    public void Give(Transform player,Transform item)
    {
       item.SetParent(player.transform);
       item.localPosition = new Vector3(0, 2, 0);      // поставить прямо на врага
       item.localRotation = Quaternion.identity;
    }
    public void Attach(Transform item)
    {
        item.SetParent(this.gameObject.transform);
        item.localPosition = new Vector3(0, 2, 0);      // поставить прямо на врага
        item.localRotation = Quaternion.identity;
    }
    private void Awake()
    {

    }
    private void OnEnable()
    {
        Run();
    }

    public void Init(EnemyReactBehaviorType reactType)
    {

        _currentBehavior = _spawner.SpawnReactBehavior(reactType, this.gameObject, _effect, this.transform);
    }
    public void ChangeReactSubState(EnemyReactBehaviorType newType)
    {


        _reactBehavior = _spawner.SpawnReactBehavior(newType, this.gameObject, _effect, this.transform);
        _currentBehavior = _reactBehavior;
        Debug.Log($"{name}: {newType}に変更");
    }

    private void Run()
    {
        if (_runCoroutine != null)
        {
            StopCoroutine(_runCoroutine);
        }
       _runCoroutine= StartCoroutine(StartRun());
    }

    private IEnumerator StartRun()
    {
        while (true)
        {
            _runAwayBehavior?.Update();
            yield return new WaitForSeconds(0.02f);

        }
    }
}
