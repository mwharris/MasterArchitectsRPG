using UnityEngine;
using UnityEngine.AI;

public class Mover : IMover
{
    private readonly Player _player;
    private readonly CharacterController _characterController;

    public Mover(Player player)
    {
        _player = player;
        _characterController = player.GetComponent<CharacterController>();
        player.GetComponent<NavMeshAgent>().enabled = false;
    }

    public void Tick()
    {
        Vector3 movementInput = new Vector3(PlayerInput.Instance.Horizontal, 0, PlayerInput.Instance.Vertical);
        Vector3 movement = _player.transform.rotation * movementInput * _player.Stats.Get(StatType.MoveSpeed);
        _characterController.SimpleMove(movement);
    }
}