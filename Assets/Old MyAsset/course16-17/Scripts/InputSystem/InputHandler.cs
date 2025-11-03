using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool PressAKey => Input.GetKey(KeyCode.A);
    public bool PressDKey => Input.GetKey(KeyCode.D);
    public bool PressWKey => Input.GetKey(KeyCode.W);
    public bool PressSKey => Input.GetKey(KeyCode.S);
    public bool PressSpaceKey => Input.GetKeyDown(KeyCode.Space);
    public bool PressYKey => Input.GetKeyDown(KeyCode.Y);

}
