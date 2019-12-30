using UnityEngine.AI;

public class ChasePlayer : IState
{
    private NavMeshAgent _navMeshAgent;
    
    // Chase the player using an NavMeshAgent.
    // Needs to be turned on / off with the state.

    public ChasePlayer(NavMeshAgent navMeshAgent)
    {
        _navMeshAgent = navMeshAgent;
    }
    
    public void Tick()
    {
    }

    public void OnEnter()
    {
        _navMeshAgent.enabled = true;
    }

    public void OnExit()
    {
        _navMeshAgent.enabled = false;
    }
}