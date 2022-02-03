using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum DirectionRotate
{
    Up,
    Down,
    Left,
    Right
}
public class RotateGun : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Transform transform;
    public DirectionRotate direction;
    public float speed;
    
    private bool _isPressed;
    private Vector2 _direction;

    private void Start()
    {
        switch (direction)
        {
            case DirectionRotate.Up:
                _direction = new Vector2(-1, 0);
                break;
            case DirectionRotate.Down:
                _direction = new Vector2(1, 0);
                break;
            case DirectionRotate.Left:
                _direction = new Vector2(0 ,-1);
                break;
            case DirectionRotate.Right:
                _direction = new Vector2(0, 1);
                break;
        }
    }
    
    void FixedUpdate()
    {
        if (_isPressed)
        {
            transform.eulerAngles += (Vector3)_direction * (Time.fixedDeltaTime * speed);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }
}