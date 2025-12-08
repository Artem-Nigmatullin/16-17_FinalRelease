using TMPro;
using UnityEngine;

/// <summary>
/// 本クラスは、プレイヤーの状態（体力）と特定エリアへの滞在状況を管理し、
/// 画面上に関連情報を表示する機能を持っています。
/// </summary>
/// <remarks>
/// 主な機能:
/// - プレイヤーが特定のエリアに入ったかどうかを記録
/// - プレイヤーの体力の変化を追跡
/// - 現在のプレイヤー情報を画面に反映
/// 
/// 使用方法:
/// - ゲーム内でプレイヤーが行動する際、体力や滞在状況の更新が自動的に行われます。
/// - 管理者や他の開発者が画面上でプレイヤーの情報を確認できます。
/// </remarks>

[RequireComponent(typeof(TextMeshProUGUI))]
public class UI : MonoBehaviour, IHealthListener, IEnterable
{
    [SerializeField] private AggrZone _aggrZone; // プレイヤーが入ると検知されるエリア
    [SerializeField] private TextMeshProUGUI _textHealth;
    [SerializeField] private TextMeshProUGUI _textPlayerName;
    [SerializeField] private CharacterHealth _characterHealth; // キャラクターの体力

    private void Start()
    {

        Debug.Log("UI HEALTH:" + _characterHealth.Health.Value);


    }
    private void OnEnable()
    {

        _aggrZone.Entered += OnEntered;
        _characterHealth.Health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        _aggrZone.Entered -= OnEntered;
        _characterHealth.Health.Changed -= OnHealthChanged;

    }

    public void OnHealthChanged(int health)
    {       
        _textHealth.text = $"HP: {_characterHealth.Health.Value}";
        _characterHealth.Health.Value = health;

    }
    public void OnEntered(GameObject gameObject)
    {
        // _player=gameObject;
        _textPlayerName.text = $"Entered: {gameObject.name}";
        DevLog.Error("name:" + gameObject);
    }

    public void OnExit(GameObject player)
    {
        _textPlayerName.text = "Entered: -";
    }

}
