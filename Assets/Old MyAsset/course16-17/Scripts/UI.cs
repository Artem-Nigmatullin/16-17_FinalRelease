using TMPro;
using UnityEngine;

public class UI : MonoBehaviour, IHealthObserver, IAgroObserver
{
    [SerializeField] private AggrZone _aggrZone;
    [SerializeField] private TextMeshProUGUI _textMesh;
    private GameObject _player;

    private CharacterHealth _characterHealth=new CharacterHealth();

    private string _playerName;

    private void Awake()
    {
     
        _aggrZone.Entered += OnEntered;
        _characterHealth.Health.Changed += OnHealthChanged;
    }

    private void OnDestroy()
    {
        _aggrZone.Entered -= OnEntered;
        _characterHealth.Health.Changed -= OnHealthChanged;

    }

    public void OnHealthChanged(int health)
    {
        _characterHealth.Health.Value = health;
        UpdateUI();
        DevLog.Log("вызов метода ui (OnNotify)");
    }

    public void OnEntered(GameObject player)
    {
        _player = player;
        UpdateUI();
        DevLog.Warn("вызов метода ui (OnPlayerEntered)");
    }

    public void OnExit(GameObject player)
    {
        _player = null;
        UpdateUI();
        DevLog.Log("UI: покинул зону");
    }

    private void UpdateUI()
    {
        _textMesh.text = $"HP: {_characterHealth.Health.Value}\n" +
                         $"зашел в зону: {_playerName}";
        _playerName = _player != null ? _player.name : "";

    }

}
