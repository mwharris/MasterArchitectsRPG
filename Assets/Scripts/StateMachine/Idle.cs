using UnityEngine;

public class Idle : IState
{
    public void Tick()
    {
        Debug.Log("Idle");
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}