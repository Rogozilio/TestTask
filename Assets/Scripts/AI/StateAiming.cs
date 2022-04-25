using UnityEngine;

namespace DefaultNamespace.AI
{
    public class StateAiming : State
    {
        private RaycastHit2D _hit;
        private int _numberBounces;
        private LineRenderer _lineRenderer;

        public StateAiming(Computer computer, StateMachine movementSM) : base(computer, movementSM)
        {
            _movementSM = movementSM;
            _computer = computer;
        }

        public override void Enter()
        {
            _numberBounces = 5;
            _lineRenderer = _computer.GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 6;
        }

        public override void PhysicsUpdate()
        {
            var numberBounces = _numberBounces;
            for (int i = 1; i < 360; i++)
            {
                var from = _computer.transform.position;
                var to = Quaternion.AngleAxis(i, Vector3.forward) * _computer.transform.up;
                _computer.Raycast(from, to, out _hit);
                var targetPos = _hit.point;
                _lineRenderer.SetPosition(0, from);
                while (_computer.Raycast(from, to, out _hit))
                {
                    if (_hit.collider.name.Split(' ')[0] == "Wall" && numberBounces > 0)
                    {
                        from = _hit.point;
                        to = Vector2.Reflect(to, _hit.normal);
                        numberBounces--;
                        _lineRenderer.SetPosition(5 - numberBounces, from);
                    }
                    else if (_hit.collider.name == "Player" && numberBounces > 0)
                    {
                        _computer.OtherTarget = targetPos;
                        _movementSM.ChangeState(_computer.StateRotate);
                        return;
                    }
                    else break;
                }
                numberBounces = _numberBounces;
            }

            _movementSM.ChangeState(_computer.StateMove);
        }
    }
}