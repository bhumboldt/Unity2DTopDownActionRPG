using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathfinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;
    private Vector2 _moveDirection;
    private Rigidbody2D _rigidbody;
    private Knockback _knockback;
    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    public void SetMoveDirection(Vector2 movement)
    {
        _moveDirection = movement;
    }
    
    public void FixedUpdate()
    {
        if (_knockback.GettingKnockedBack)
        {
            return;
        }
        
        this.Move();

        if (_moveDirection.x < 0)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false;
        }
    }
    
    private void Move()
    {
        var currPosition = _rigidbody.position;
        var newPosition = currPosition + _moveDirection * (moveSpeed * Time.fixedDeltaTime);
        _rigidbody.MovePosition(newPosition);
    }
}
