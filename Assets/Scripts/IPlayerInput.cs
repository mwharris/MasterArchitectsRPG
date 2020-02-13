using System;

public interface IPlayerInput
{
    event Action<int> HotkeyPressed; 
    event Action MovementMethodChanged;

    float Vertical   { get; }
    float Horizontal { get; }
    float MouseX { get; }
    
    void Tick();

    bool PausePressed { get; }
}