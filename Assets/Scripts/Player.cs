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
 
    private float _speedRotate;
    private float _speed;
    private float _speedFiring;
    private float _speedProjectile;

    private Rigidbody2D _rigidbody2D;
    private Launcher _launcher;

    private void Awake()
    {
        _speed = PlayerParameters.Speed;
        _speedRotate = PlayerParameters.SpeedRotate;
        _speedFiring = PlayerParameters.SpeedFiring;
        _speedProjectile = PlayerParameters.SpeedProjectile;
        
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
    }
}