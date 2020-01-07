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

    public event Action<IState> OnEntityStateChanged;

    private void Awake()
    {
        Player player = FindObjectOfType<Player>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _entity = GetComponent<Entity>();
        _stateMachine = new StateMachine();
        
        // Just want to explain this line further b/c there's a lot going on here.
        // I believe what this is doing is registering a lambda function with StateMachine.OnStateChanged.
        // This lamdba function receives "state", and simply called EntityStateMachine.OnStateChanged(state).
        _stateMachine.OnStateChanged += state => OnEntityStateChanged?.Invoke(state);

        Idle idle = new Idle();
        ChasePlayer chasePlayer = new ChasePlayer(_navMeshAgent, player);
        Attack attack = new Attack();
        Dead dead = new Dead(_entity);
        
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
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) <= 2f
        );
        
        /*
        // Attack -> Chase
        _stateMachine.AddTransition(
            attack, 
            chasePlayer, 
            () => DistanceFlat(_navMeshAgent.transform.position, player.transform.position) > 2f
        );
        */
        
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