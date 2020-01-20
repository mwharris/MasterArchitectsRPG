using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateMachine : MonoBehaviour
{
    public static event Action<IState> OnGameStateChanged;
    
    private StateMachine _stateMachine;
    private static bool _initialized;

    private void Awake()
    {
        if (_initialized)
        {
            Destroy(this.gameObject);
            return;
        }
        _initialized = true;
        DontDestroyOnLoad(this.gameObject);
        
        _stateMachine = new StateMachine();
        _stateMachine.OnStateChanged += state => OnGameStateChanged?.Invoke(state);
        
        var menu = new Menu();
        var loading = new LoadLevel();
        var play = new Play();
        var pause = new Pause();
        
        _stateMachine.SetState(loading);
        
        _stateMachine.AddTransition(loading, play, loading.Finished);
        
        _stateMachine.AddTransition(play, pause, () => Input.GetKeyDown(KeyCode.Escape));
        _stateMachine.AddTransition(pause, play, () => Input.GetKeyDown(KeyCode.Escape));
        
        _stateMachine.AddTransition(pause, loading, () => RestartButton.Pressed);
    }
    
    private void Update()
    {
        _stateMachine.Tick();
    }
}

public class Menu : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class Play : IState
{
    public void Tick()
    {
    }

    public void OnEnter()
    {
    }

    public void OnExit()
    {
    }
}

public class LoadLevel : IState
{
    private readonly List<AsyncOperation> _operations = new List<AsyncOperation>();
    
    // TrueForAll() runs a function for all elements and returns true if they all evaluate to true
    public bool Finished() => _operations.TrueForAll(t => t.isDone);
    
    public void Tick()
    {
    }

    public void OnEnter()
    {
        _operations.Add(SceneManager.LoadSceneAsync("Level1"));
        _operations.Add(SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive));
    }

    public void OnExit()
    {
        _operations.Clear();
    }
}

public class Pause : IState
{
    public static bool Active { get; private set; }
    
    public void Tick()
    {
    }

    public void OnEnter()
    {
        Active = true;
        Time.timeScale = 0f;
    }

    public void OnExit()
    {
        Active = false;
        Time.timeScale = 1f;
    }
}
