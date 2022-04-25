using System;
using UnityEngine;

namespace DefaultNamespace.AI
{
    public class StateRotate : State
    {
        private RaycastHit2D _hit;
        private Rigidbody2D _rigidbody;

        public StateRotate(Computer computer, StateMachine movementSM) : base(computer, movementSM)
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
            float value = 0;
            _computer.Raycast(_computer.transform.position,
                _computer.Target.transform.position - _computer.transform.position, out _hit);
            if (_hit.collider.name != "Player")
            {
                if(_computer.OtherTarget == Vector3.zero)
                    _movementSM.ChangeState(_computer.StateAiming);
                else
                    value = _computer.RotateToTarget(_computer.OtherTarget);
            }
            
            if (_computer.OtherTarget == Vector3.zero)
            {
                value = _computer.RotateToTarget(_computer.Target.transform.position);
            }
            
            _rigidbody.rotation += value;
            _rigidbody.velocity = Vector3.zero;
            if (value == 0f)
            {
                _movementSM.ChangeState(_computer.StateShot);
                _computer.OtherTarget = Vector3.zero;
            }
                
        }
    }
}