using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CoinZone : MonoBehaviour
{
    private float minTime = 1.7f;   // минимум секунд
    private float maxTime = 3f;
    private IInteractable[] _interactables;
    private Coin _coin;
    private Lightning _lightningEffect;
    private Coroutine randomCoroutine;
    private Player _player;
    private float _dist;
    private float _currentDistance;
    private bool HasEnemy => _smallEnemy != null;
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private SmallEnemy _smallEnemy;
    [SerializeField] private Lock _lock;
    private bool HasPlayer => _player != null;

    public Coin Coin { get => _coin; set => _coin = value; }
    public Lightning LightningEffect { get => _lightningEffect; }
    public GameObject KeyObject { get => _keyObject; }

    private void Awake()
    {
        _interactables = GetComponentsInChildren<IInteractable>();
        GetInteractablesItem();

    }

    private void GetInteractablesItem()
    {
        foreach (var interact in _interactables)
        {
            if (interact is Lightning lightning)
                _lightningEffect = lightning;
            if (interact is Coin coin)
                Coin = coin;

        }

    }
    public float GetDistance()
    {
        if (HasPlayer == false) return _dist = 0;
        return _dist = Vector3.Distance(_player.transform.position, _smallEnemy.transform.position);
    }
    private void Update()
    {

        if (HasEnemy && HasPlayer)
        {
            _currentDistance = GetDistance();
            if (_currentDistance < 2 && IsEnemyActive())
            {
                _smallEnemy.Give(_player.transform, KeyObject.transform);
                _smallEnemy.gameObject.SetActive(false);
                _lock.gameObject.SetActive(true);
            }
        }
    }
    private void Start()
    {
        StartRandomDissable();
        if (_lightningEffect == null)
        {
            throw new System.InvalidOperationException(nameof(_lightningEffect));
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            _player = player;
            // if (!_coinCollector.IsNotEmptyCoin) return;

            if (Coin == null)
            {
                throw new System.InvalidOperationException(nameof(Coin));
            }
            if (IsLightningEffectActive())
            {
                

                _smallEnemy.gameObject.SetActive(true);
                _smallEnemy.Attach(KeyObject.transform);

                Coin.Off();
                if (_lightningEffect != null)
                   _lightningEffect.gameObject.SetActive(false);

            }
            

        }

    }
    public bool IsCoin()
    {
        return _coin.gameObject.activeInHierarchy == true;
    }
    private bool IsEnemyActive()
    {
        return _smallEnemy.gameObject.activeInHierarchy == true;
    }
    private bool IsLightningEffectActive()
    {
        return LightningEffect.gameObject.activeInHierarchy == true;
    }

    private void StartRandomDissable()
    {
        if (randomCoroutine != null)
        {
            StopCoroutine(randomCoroutine);
        }
       
            randomCoroutine = StartCoroutine(RandomDisable());
    }

    private IEnumerator RandomDisable()
    {
        while (true)
        {
            float randomTime = Random.Range(minTime, maxTime);

            yield return new WaitForSeconds(randomTime);

            LightningEffect?.Off();


            yield return new WaitForSeconds(randomTime);


            LightningEffect?.On();
        }
    }
}
