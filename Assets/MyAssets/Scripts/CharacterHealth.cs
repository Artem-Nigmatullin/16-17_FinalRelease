using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private ReactiveVariable<int> _health = new ReactiveVariable<int>(2000);

    public ReactiveVariable<int> Health { get { return _health; } private set { _health = value; } }

    [SerializeField] private UI _uI;

    private void OnEnable()
    {
        _health.Dead += OnDead;

    }
    private void OnDisable()
    {
        _health.Dead -= OnDead;
    }
    public void OnDead() { gameObject.SetActive(false); }
    public void TakeDamage(int dmg)
    {
        _health.Value -= dmg;
        if (_health.Value <= 0)
            _health.Value = 0;

    }

    private void OnDestroy()
    {
        _health = null;
    }
}
