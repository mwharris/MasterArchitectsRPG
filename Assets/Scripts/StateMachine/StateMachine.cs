using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private List<StateTransition> _stateTransitions = new List<StateTransition>();
    private List<StateTransition> _anyStateTransitions = new List<StateTransition>();
    
    private IState _currentState;
    public IState CurrentState => _currentState;

    public event Action<IState> OnStateChanged;

    public void Tick()
    {
        // Transition to a new State if any of our current State's transition conditions are met
        StateTransition transition = CheckForTransition();
        if (transition != null)
        {
            SetState(transition.To);
        }
        _currentState.Tick();
    }
    
    // Set a new current State
    public void SetState(IState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState?.OnExit();
        _currentState = state;
        Debug.Log($"Changed to state {state}");
        _currentState.OnEnter();
        
        OnStateChanged?.Invoke(_currentState);
    }

    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        StateTransition stateTransition = new StateTransition(from, to, condition);
        _stateTransitions.Add(stateTransition);
    }
    
    public void AddAnyTransition(IState to, Func<bool> condition)
    {
        StateTransition stateTransition = new StateTransition(null, to, condition);
        _anyStateTransitions.Add(stateTransition);
    }
    
    // Check if the current State has met a State Transition condition.
    // Return that State Transition if so.
    private StateTransition CheckForTransition()
    {
        // Any state transitions - don't check From state
        foreach (var anyTransition in _anyStateTransitions)
        {
            if (anyTransition.Condition())
            {
                return anyTransition;
            }
        }
        // Normal state transitions - check From state
        foreach (var stateTransition in _stateTransitions)
        {
            if (_currentState == stateTransition.From && stateTransition.Condition())
            {
                return stateTransition;
            }
        }
        return null;
    }

}