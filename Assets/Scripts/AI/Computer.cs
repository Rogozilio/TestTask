using System;
using UnityEngine;
using UnityEngine.AI;

namespace DefaultNamespace.AI
{
    public class Computer : MonoBehaviour
    {
        private Launcher _launcher;
        private StateMachine _movementSM;
        private Transform _player;

        [SerializeField]
        private UnitParameters ComputerParametrs;
        public float Speed => ComputerParametrs.Speed;
        public float SpeedRotate => ComputerParametrs.SpeedRotate;
        public float SpeedFiring => ComputerParametrs.SpeedFiring;
        public float SpeedProjectile => ComputerParametrs.SpeedProjectile;

        public StateMove StateMove { get; private set; }

        public StateRotate StateRotate { get; private set; }

        public StateAiming StateAiming { get; private set; }

        public StateShot StateShot { get; private set; }

        public Transform Target { get; private set; }
        
        public Vector3 OtherTarget { get; set; }

        public NavMeshAgent Agent { get; private set; }

        private void Awake()
        {
            _movementSM = new StateMachine();
            StateMove = new StateMove(this, _movementSM);
            StateRotate = new StateRotate(this, _movementSM);
            StateAiming = new StateAiming(this, _movementSM);
            StateShot = new StateShot(this, _movementSM);

            _player = GameObject.FindGameObjectWithTag("Player").transform;
            Target = _player;
            _launcher = new Launcher(GameObject.FindGameObjectsWithTag("Projectile"), SpeedFiring,
                SpeedProjectile);
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;

            _movementSM.Initialize(StateMove);
        }

        private void Update()
        {
            _movementSM.CurrentState.HandleInput();
            _movementSM.CurrentState.LogicUpdate();
        }

        private void FixedUpdate()
        {
            _movementSM.CurrentState.PhysicsUpdate();
        }

        public bool Raycast(Vector3 pos, Vector3 dir, out RaycastHit2D hit2D)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos, dir, 50f);
            hit2D = hit;
            return hit;
        }

        public float RotateToTarget(Vector3 target)
        {
            var angle = Vector2.SignedAngle(transform.up,
                target - transform.position);

            var value = Math.Round(angle) / Math.Abs(Math.Round(angle));

            return double.IsNaN(value) ? 0f : (float)value * SpeedRotate * Time.fixedDeltaTime;
        }

        public void TakeShot()
        {
            StartCoroutine(_launcher.Shot(transform.GetChild(0)));
        }
    }
}