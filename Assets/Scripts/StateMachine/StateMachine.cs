using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private Dictionary<IState, List<StateTransition>> _stateTransitions = new Dictionary<IState, List<StateTransition>>();
    private List<IState> _states = new List<IState>();
    private IState _currentState;
    public IState CurrentState => _currentState;

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

    // Add a new State to our List of States 
    public void Add(IState state)
    {
        _states.Add(state);
    }

    // Set a new current State
    public void SetState(IState state)
    {
        if (_currentState == state)
            return;

        _currentState?.OnExit();
        _currentState = state;
        _currentState.OnEnter();
        
        Debug.Log($"Changed to state {state}");
    }

    // Add a new State Transition
    public void AddTransition(IState from, IState to, Func<bool> condition)
    {
        // Create a new list if this key doesn't already exist
        if (!_stateTransitions.ContainsKey(from))
        {
            _stateTransitions[from] = new List<StateTransition>();
        }
        // Create and add the new State Transition
        StateTransition stateTransition = new StateTransition(from, to, condition);
        _stateTransitions[from].Add(stateTransition);
    }
    
    // Check if the current State has met a State Transition condition.
    // Return that State Transition if so.
    private StateTransition CheckForTransition()
    {
        if (_stateTransitions.ContainsKey(_currentState))
        {
            foreach (var stateTransition in _stateTransitions[_currentState])
            {
                if (stateTransition.Condition())
                {
                    return stateTransition;
                }
            }
        }
        return null;
    }
}