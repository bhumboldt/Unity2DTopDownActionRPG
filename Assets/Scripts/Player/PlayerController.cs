using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft
    {
        get { return facingLeft; }
    }

    public static PlayerController Instance;
    
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private InputActionAsset _inputActionAsset;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private TrailRenderer _trailRenderer;
    
    private Vector2 _movement;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private bool isDashing = false;
    private bool facingLeft = false;
    private float startingMoveSpeed;
    
    void Awake()
    {
        Instance = this;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _inputActionAsset.FindActionMap("Combat").FindAction("Dash").performed += _ => Dash();
        startingMoveSpeed = moveSpeed;
    }

    public void FixedUpdate()
    {
        this.Move();
        this.FlipPlayerPosition();
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
            facingLeft = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
            facingLeft = false;
        }
    }

    private void Dash()
    {
        if (isDashing)
        {
            return;
        }
        
        isDashing = true;
        moveSpeed *= dashSpeed;
        _trailRenderer.emitting = true;
        StartCoroutine(DashCooldownRoutine());
    }

    private IEnumerator DashCooldownRoutine()
    {
        float dashCD = 0.25f;
        yield return new WaitForSeconds(0.2f);
        _trailRenderer.emitting = false;
        moveSpeed = startingMoveSpeed;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;
    }
}
