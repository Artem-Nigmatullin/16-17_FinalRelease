using UnityEngine;

public enum ZoneState
{
    SuccessEntered,
    Waiting,
    EnemySearchingKey,
    KeyCaught,
    LockActive,
    WaitingKeyLockTrigger,
    CompleteResponseKeyLockTrigger,
    Completed
}

public class CoinZoneSet : MonoBehaviour
{
    [SerializeField] private CoinZone _zone;
    [SerializeField] private Player _player;
    [SerializeField] private CoinCollector _coinCollector;
    private Key _keyObject;
    private SmallEnemy _smallEnemy;
    private KeyLock _keyLockTrigger;
    private ZoneState _state = ZoneState.Waiting;
    private ISwitcher[] _interactables;
    private Coin _coin;
    private Lightning _lightningEffect;
    private SpawnEffect _spawnEffect;
    private float _distance;
    private float _currentDistance;
    private float _minDistance = 2;
    private bool HasPlayer => _player != null;
    private bool HasEnemy => _smallEnemy != null;
    public Coin Coin { get => _coin; set => _coin = value; }
    public Lightning LightningEffect { get => _lightningEffect; }
  
    public bool IsKeyWithEnemy => _keyObject != null &&
        _keyObject.gameObject.activeInHierarchy &&
        _keyObject.transform.IsChildOf(_smallEnemy.transform);


    private void Start()
    {
        _lightningEffect.PlayEffect();

        if (_lightningEffect == null)
        {
            throw new System.InvalidOperationException(nameof(_lightningEffect));
        }
    }

    public float GetDistance()
    {
        if (HasEnemy == false || HasPlayer == false) return 0;
        return _distance = Vector3.Distance(_player.transform.position, _smallEnemy.transform.position);

    }

    private void Update()
    {
        _currentDistance = GetDistance();

        if (IsKeyWithEnemy == true)
        {
            EnemyPursuit();

        }

    }

    private void ActivateKeyLockZone()
    {
        _keyLockTrigger?.gameObject.SetActive(true);
    }

    private void OnInsertedKey(KeyLock keylock)
    {
        _keyObject.gameObject.SetActive(false);
        _lightningEffect.Hide();
        _state = ZoneState.CompleteResponseKeyLockTrigger;
    }

    private void OnEnable()
    {
        _keyLockTrigger.InsertedKey += OnInsertedKey;
        _zone.Entered += OnPlayerEntered;
    }

    private void OnDisable()
    {
        _keyLockTrigger.InsertedKey -= OnInsertedKey;
        _zone.Entered -= OnPlayerEntered;
    }

    public ZoneState State => _state;

    private void Awake()
    {
        _interactables = GetComponentsInChildren<ISwitcher>(true);
        GetInteractablesItem();
        _keyObject = GetComponentInChildren<Key>(true);
        _smallEnemy = GetComponentInChildren<SmallEnemy>(true);
        _keyLockTrigger = GetComponentInChildren<KeyLock>(true);
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

    public void OnPlayerEntered(CoinZone zone)
    {


        if (_state == ZoneState.Waiting
            && LightningEffect.Effect.isPlaying)
        {
            DevLog.Log("zone name:" + gameObject.name);
            DevLog.Log("state:" + _state);
            EnterLightningFailure();
        }
        if (_state == ZoneState.WaitingKeyLockTrigger)
        {
            ActivateKeyLockZone();
            DevLog.Log("state:" + _state);
        }
        if (_state == ZoneState.CompleteResponseKeyLockTrigger)
        {
            TakeCoin();
            DevLog.Log("state:" + _state);
        }
        if (_state == ZoneState.Completed)
        {
            Complete();
            DevLog.Log("state:" + _state);
        }
        else
        {
            EnterSuccess();
            DevLog.Log("state:" + _state);
        }
    }
    public void EnterSuccess()
    {
        _lightningEffect?.Hide();
    }
    public void EnterLightningFailure()
    {
        SetupEffect();
        _coin.Hide();
        AttachKeyWithEnemy();
    }

    private void SetupEffect()
    {
        _spawnEffect?.PlayRedEffect();
        _spawnEffect?.StopBlueEffect();
    }

    private void AttachKeyWithEnemy()
    {
        _keyObject.gameObject.SetActive(true);
        _smallEnemy?.gameObject.SetActive(true);
        _smallEnemy?.Attach(_keyObject.transform);

    }

    public void EnemyPursuit()
    {
        if (_currentDistance < _minDistance)
        {
            _spawnEffect?.StopRedEffect();
            _player?.ReceiveKey(_keyObject.transform);
            _keyLockTrigger?.gameObject.SetActive(true);
            _state = ZoneState.WaitingKeyLockTrigger;

        }
    }

    public void InsertKey()
    {
        _coin.Show();
        _state = ZoneState.CompleteResponseKeyLockTrigger;
    }

    public void TakeCoin()
    {
        _coin.Show();
        _spawnEffect?.gameObject.SetActive(false);
        _keyObject?.gameObject.SetActive(false);
        _keyLockTrigger.gameObject.SetActive(false);
        _state = ZoneState.Completed;
    }

    public void Complete()
    {
        _spawnEffect?.StopRedEffect();
        _spawnEffect?.StopBlueEffect();
   
    }

}
