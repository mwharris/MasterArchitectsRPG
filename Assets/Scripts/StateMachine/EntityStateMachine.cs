using System;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using Vector3 = UnityEngine.Vector3;

public class EntityStateMachine : MonoBehaviour
{
    private StateMachine _stateMachine;
    private NavMeshAgent _navMeshAgent;
    private Entity _entity;

    public Type CurrentStateType => _stateMachine.CurrentState.GetType();

    private void Awake()
    {
        Player player = FindObjectOfType<Player>();
        
        _stateMachine = new StateMachine();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        
        Idle idle = new Idle();
        ChasePlayer chasePlayer = new ChasePlayer(_navMeshAgent);
        Attack attack = new Attack();
        Dead dead = new Dead();
        
        // Idle -> Chase
        _stateMachine.AddTransition(
            idle, 
            chasePlayer, 
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) < 5f
        );
        
        // Chase -> Attack
        _stateMachine.AddTransition(
            chasePlayer, 
            attack, 
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) < 2f
        );
        
        // Any -> Dead
        _stateMachine.AddAnyTransition(dead, () => _entity.Health <= 0);
        
        _stateMachine.SetState(idle);
    }

    private float DistanceFlat(Vector3 source, Vector3 destination)
    {
        return Vector3.Distance(new Vector3(source.x, 0, source.z), new Vector3(destination.x, 0, destination.z));
    }
    
    private void Update()
    {
        _stateMachine.Tick();
    }
}