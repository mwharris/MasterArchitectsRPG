using UnityEngine;

public class PauseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject _panel;
    
    private void Awake()
    {
        _panel.SetActive(false);
        GameStateMachine.OnGameStateChanged += HandleGameStateChanged;
    }

    private void HandleGameStateChanged(IState state)
    {
        _panel.SetActive(state is Pause);
    }

    private void OnDestroy()
    {
        GameStateMachine.OnGameStateChanged -= HandleGameStateChanged;
    }
}