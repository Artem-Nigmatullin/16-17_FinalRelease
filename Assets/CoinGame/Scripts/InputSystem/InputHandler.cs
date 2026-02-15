using UnityEngine;

[CreateAssetMenu(
    fileName = "InputHandler",
    menuName = "Game Data/Player Data")]
public class InputHandler : ScriptableObject
{
    [SerializeField] public bool PressAKey => Input.GetKey(KeyCode.A);
    [SerializeField] public bool PressDKey => Input.GetKey(KeyCode.D);
    [SerializeField] public bool PressWKey => Input.GetKey(KeyCode.W);
    [SerializeField] public bool PressSKey => Input.GetKey(KeyCode.S);
    [SerializeField] public bool PressSpaceKey => Input.GetKeyDown(KeyCode.Space);
    [SerializeField] public bool PressYKey => Input.GetKeyDown(KeyCode.Y);

}
