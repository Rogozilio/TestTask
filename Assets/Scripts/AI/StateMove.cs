using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.AI;
using UnityEngine;

public class StateMove : State
{
    private RaycastHit2D _hit;
    private Rigidbody2D _rigidbody;

    public StateMove(Computer computer, StateMachine movementSM) : base(computer, movementSM)
    {
        _movementSM = movementSM;
        _computer = computer;
    }

    public override void Enter()
    {
        _rigidbody = _computer.GetComponent<Rigidbody2D>();
    }

    public override void PhysicsUpdate()
    {
        _computer.Raycast(_computer.transform.position,
            _computer.Target.transform.position - _computer.transform.position, out _hit);
        if (_hit.collider.name == "Player")
            _movementSM.ChangeState(_computer.StateRotate);

        _computer.Agent.SetDestination(_computer.Target.position);
        _rigidbody.rotation += _computer.RotateToTarget(_computer.Agent.steeringTarget);
        _rigidbody.velocity =
            _computer.transform.up * _computer.Speed * Time.fixedDeltaTime;
    }

    public override void Exit()
    {
        _computer.Agent.isStopped = true;
    }
}