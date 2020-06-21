using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    public static IPlayerInput Instance { get; set; }

    public event Action<int> HotkeyPressed;
    public event Action MovementMethodChanged;
    
    public float Vertical => Input.GetAxis("Vertical");
    public float Horizontal => Input.GetAxis("Horizontal");
    public float MouseX => Input.GetAxis("Mouse X");
    public bool PausePressed => Input.GetKeyDown(KeyCode.Escape);
    public Vector2 MousePosition => Input.mousePosition;
    public bool GetKeyDown(KeyCode keyCode) => Input.GetKeyDown(keyCode);

    private void Awake()
    {
        Instance = this;
    }

    public void Update()
    {
        Tick();
    }

    public void Tick()
    {
        DetectHotkeyInput();
        DetectMoveMethodInput();
    }
    
    private void DetectHotkeyInput()
    {
        if (HotkeyPressed == null)
        {
            return;
        }

        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                HotkeyPressed?.Invoke(i);
            }
        }
    }

    private void DetectMoveMethodInput()
    {
        if (Input.GetKeyDown(KeyCode.Minus))
            MovementMethodChanged?.Invoke();
    }
}