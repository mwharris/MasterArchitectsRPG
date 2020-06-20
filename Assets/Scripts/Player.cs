using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _characterController;
    private IMover _mover;
    private Rotator _rotator;
    private Inventory _inventory;

    public Stats Stats { get; private set; }

    void Awake()
    {
        PlayerInput.Instance.MovementMethodChanged += UpdateMovementMethod;
        
        _characterController = GetComponent<CharacterController>();
        _mover = new Mover(this);
        _rotator = new Rotator(this);
        _inventory = GetComponent<Inventory>();
        
        Stats = new Stats();
        Stats.Bind(_inventory);
    }

    private void Update()
    {
        if (Pause.Active)
        {
            return;
        }
        _mover.Tick();
        _rotator.Tick();
    }

    private void UpdateMovementMethod()
    {
        if (_mover is Mover)
            _mover = new NavmeshMover(this);
        else
            _mover = new Mover(this);
    }
}