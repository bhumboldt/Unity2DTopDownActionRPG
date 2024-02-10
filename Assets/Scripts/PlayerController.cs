using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        this.Move();
    }

    void Update()
    {
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }

    private void Move()
    {
        var currPosition = _rigidbody.position;
        var newPosition = currPosition + _movement * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
