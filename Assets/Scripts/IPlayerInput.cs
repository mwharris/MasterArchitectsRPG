using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action<int> HotkeyPressed; 
    event Action MovementMethodChanged;

    float Vertical   { get; }
    float Horizontal { get; }
    float MouseX { get; }
    
    void Tick();

    bool PausePressed { get; }
    Vector2 MousePosition { get; }
    bool GetKeyDown(KeyCode keyCode);
}