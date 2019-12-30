using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<IState> _states = new List<IState>();
    private IState _currentState;
    
    public void Tick()
    {
        _currentState.Tick();
    }
    
    public void Add(IState state)
    {
        _states.Add(state);
    }

    public void SetState(IState state)
    {
        if (_currentState == state)
            return;

        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();
        
        Debug.Log($"Changed to state {state}");
    }
}