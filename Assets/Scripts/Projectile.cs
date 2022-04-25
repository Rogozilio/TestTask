using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class Projectile : MonoBehaviour
    {
        public Action OnHitInPlayer;
        public Action OnHitInComputer;

        private Rigidbody2D _rigidBody;
        private float _speed;
        private Camera _camera;
        private Vector3 _dir;
        private UI _ui;

        public float SetSpeed
        {
            set => _speed = value;
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
            _camera = FindObjectOfType<Camera>();
        }

        private void OnEnable()
        {
            _dir = transform.up;
        }

        void FixedUpdate()
        {
            DisableProjectileOutCamera();
            _rigidBody.MovePosition(transform.position + _dir * _speed * Time.fixedDeltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnHitInPlayer?.Invoke();
                gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Computer"))
            {
                OnHitInComputer?.Invoke();
                gameObject.SetActive(false);
            }

            if (collision.gameObject.CompareTag("Wall"))
            {
                _dir = Vector2.Reflect(_dir, collision.contacts[0].normal);
            }

            if (collision.gameObject.CompareTag("Projectile"))
            {
                gameObject.SetActive(false);
            }
        }

        private void DisableProjectileOutCamera()
        {
            var point = _camera.WorldToViewportPoint(transform.position);
            if (point.x < 0 || point.x > 1 ||
                point.y < 0 || point.y > 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}