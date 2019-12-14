using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private IMover _mover;
    private Rotator _rotator;

    public IPlayerInput PlayerInput { get; set; } = new PlayerInput();

    void Awake()
    {
        PlayerInput.MovementMethodChanged += UpdateMovementMethod;
        
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);
        _rotator = new Rotator(this);
    }

    private void Update()
    {
        _mover.Tick();
        _rotator.Tick();
        PlayerInput.Tick();
    }

    private void UpdateMovementMethod()
    {
        if (_mover is Mover)
        {
            _mover = new NavmeshMover(this);
        }
        else
        {
            _mover = new Mover(this);
        }
    }
}