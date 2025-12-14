using System.Collections;
using UnityEngine;

public class CoinZone : MonoBehaviour
{
    [SerializeField] private CoinCollector _coinCollector;
    [SerializeField] private GameObject _keyObject;
    [SerializeField] private SmallEnemy _smallEnemy;
    [SerializeField] private Lock _lock;

    private ISwitcher[] _interactables;
    private Coin _coin;
    private Lightning _lightningEffect;
    private SpawnEffect _spawnEffect;
    private Player _player;
    private float _distance;
    private float _currentDistance;
    private float _minDistance = 2;
    private float minTime = 1.7f;
    private float maxTime = 3f;
    private bool HasPlayer => _player != null;
    private bool HasEnemy => _smallEnemy != null;
    public Coin Coin { get => _coin; set => _coin = value; }
    public Lightning LightningEffect { get => _lightningEffect; }
    public SpawnEffect SpawnEffect { get => _spawnEffect; }
    public GameObject KeyObject { get => _keyObject; }
    public SmallEnemy SmallEnemy { get => _smallEnemy; set => _smallEnemy = value; }
    public bool IsKeyWithEnemy => _keyObject != null &&
        _keyObject.activeInHierarchy &&
        _keyObject.transform.IsChildOf(_smallEnemy.transform);

    public bool IsKeyWithPlayer => _player != null && _keyObject != null &&
    _keyObject.activeInHierarchy &&
    _keyObject.transform.IsChildOf(_player.transform);

    public bool IsCoinWithPlayer => _coin != null && _player != null &&
        _coin.gameObject.activeInHierarchy &&
        _coin.transform.IsChildOf(_player?.transform);


    private void Awake()
    {
        _interactables = GetComponentsInChildren<ISwitcher>(true);
        GetInteractablesItem();
    }
    private void OnEnable()
    {
        _coinCollector.OnCoinPickedWithPlayer += DisableEffect;
    }
    private void OnDisable()
    {
        _coinCollector.OnCoinPickedWithPlayer -= DisableEffect;
    }
    private void GetInteractablesItem()
    {
        foreach (var interact in _interactables)
        {
            if (interact is Lightning lightning)
                _lightningEffect = lightning;
            if (interact is Coin coin)
                Coin = coin;
            if (interact is SpawnEffect effect)
            {
                _spawnEffect = effect;
            }
        }

    }
    public float GetDistance()
    {
        if (HasEnemy == false || HasPlayer == false) return 0;
        return _distance = Vector3.Distance(_player.transform.position, _smallEnemy.transform.position);

    }
    private void Start()
    {
        _lightningEffect.PlayEffect();

        if (_lightningEffect == null)
        {
            throw new System.InvalidOperationException(nameof(_lightningEffect));
        }
    }
    private void Update()
    {
        if (IsKeyWithEnemy == false) return;
        _currentDistance = GetDistance();

        if (_currentDistance < _minDistance)
        {

            _player.ReceiveKey(KeyObject.transform);
            _smallEnemy?.gameObject.SetActive(false);
            _lock.gameObject.SetActive(true);

        }

    }
    private void DisableEffect()
    {
      gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() is Player player)
        {
            _player = player;

            if (AreAllConditionsMet())
            {

                _spawnEffect.PlayRedEffect();
                _spawnEffect.StopBlueEffect();
                _keyObject.gameObject.SetActive(true);
                _smallEnemy?.gameObject.SetActive(true);
                _smallEnemy?.Attach(KeyObject.transform);
                Coin.Off();
                _lightningEffect.Off();
                return;
            }
            if (HasSpawnRedEffectInScene() == false)
            {
                _spawnEffect.PlayBlueEffect();
            }

            _lightningEffect.Off();

        }

    }

    private bool AreAllConditionsMet()
    {
        return HasLightningEffectInScene() && HasEnemy && HasCoinInScene();
    }
    private bool HasCoinInScene()
    {
        return _coin.gameObject.activeInHierarchy == true;
    }
    private bool HasSmallEnemyInScene()
    {
        return _smallEnemy.gameObject.activeInHierarchy == true;
    }
    private bool HasLightningEffectInScene()
    {
        return _lightningEffect.Effect.isPlaying;
    }
    private bool HasSpawnRedEffectInScene()
    {
        return _spawnEffect.BlueEffect.isPlaying;
    }


}
