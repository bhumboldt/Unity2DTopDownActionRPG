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
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        this.Move();
        this.FlipPlayerPosition();
    }

    void Update()
    {
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
        _animator.SetFloat("MoveX", _movement.x);
        _animator.SetFloat("MoveY", _movement.y);
    }

    private void Move()
    {
        var currPosition = _rigidbody.position;
        var newPosition = currPosition + _movement * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private void FlipPlayerPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePosition.x < playerPosition.x)
        {
            _spriteRenderer.flipX = true;
            // transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            _spriteRenderer.flipX = false;
            // transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
