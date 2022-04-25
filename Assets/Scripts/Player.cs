using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    public UnitParameters PlayerParameters;

    private LineRenderer _lineRenderer;
    private float _speedRotate;
    private float _speed;
    private float _speedFiring;
    private float _speedProjectile;
    private int counter = 5;

    private Rigidbody2D _rigidbody2D;
    private Launcher _launcher;

    private void Awake()
    {
        _speed = PlayerParameters.Speed;
        _speedRotate = PlayerParameters.SpeedRotate;
        _speedFiring = PlayerParameters.SpeedFiring;
        _speedProjectile = PlayerParameters.SpeedProjectile;

        _lineRenderer = GetComponent<LineRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _launcher = new Launcher(GameObject.FindGameObjectsWithTag("Projectile"), _speedFiring,
            _speedProjectile);
    }

    private void Start()
    {
        foreach (var projectile in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            projectile.SetActive(false);
        }
    }

    void FixedUpdate()
    {
        _rigidbody2D.rotation += -Input.GetAxis("Horizontal") * _speedRotate * Time.fixedDeltaTime;
        _rigidbody2D.velocity =
            transform.up * Input.GetAxis("Vertical") * _speed * Time.fixedDeltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(_launcher.Shot(transform.GetChild(0)));
        }
        _lineRenderer.SetPosition(0, transform.position);
        var from = transform.position;
        var to = transform.up;
        while (Raycast(from, to, out var hit))
        {
            counter--;
            _lineRenderer.SetPosition(5 - counter, hit.point);
            if (hit.collider.name.Split(' ')[0] == "Wall")
            {
                from = hit.point;
                to = Vector2.Reflect(to, hit.normal);
            }

            if (counter <= 0)
                break;
        }

        counter = 5;
    }

    public bool Raycast(Vector3 pos, Vector3 dir, out RaycastHit2D hit2D)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, 50f);
        hit2D = hit;
        return hit;
    }
}