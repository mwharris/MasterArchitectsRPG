using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;

    private void Awake()
    {
        var player = FindObjectOfType<Player>();
        
        _stateMachine = new StateMachine();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        
        Idle idle = new Idle();
        ChasePlayer chasePlayer = new ChasePlayer(_navMeshAgent);
        Attack attack = new Attack();
        
        _stateMachine.Add(idle);
        _stateMachine.Add(chasePlayer);
        _stateMachine.Add(attack);

        // Idle -> Chase
        _stateMachine.AddTransition(
            idle, 
            chasePlayer, 
            () => Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 5f
        );
        
        // Chase -> Attack
        _stateMachine.AddTransition(
            chasePlayer, 
            attack, 
            () => Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 3f
        );
        
        _stateMachine.SetState(idle);
    }

    private void Update()
    {
        _stateMachine.Tick();
    }
}