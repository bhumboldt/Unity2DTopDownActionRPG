using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grape : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Attack()
    {
        _animator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - PlayerController.Instance.transform.position.x < 0)
        {
            _spriteRenderer.flipX = false;
        } 
        else
        {
            _spriteRenderer.flipX = true;
        }
    }

    public void SpawnProjectileAnimEvent()
    {
        Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
    }
}
