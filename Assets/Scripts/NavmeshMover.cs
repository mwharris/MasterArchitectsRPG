using UnityEngine;
using UnityEngine.AI;

public class NavmeshMover : IMover
{
    private readonly Player _player;
    private readonly NavMeshAgent _navmeshAgent;

    public NavmeshMover(Player player)
    {
        _player = player;
        _navmeshAgent = player.GetComponent<NavMeshAgent>();
        _navmeshAgent.enabled = true;
    }
    
    public void Tick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var hitInfo))
            {
                _navmeshAgent.destination = hitInfo.point;
            }
        }
    }
}