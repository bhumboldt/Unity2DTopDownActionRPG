using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float knockBackThrustAmount = 10.0f;
    [SerializeField] private float _damageRecoveryTime = 1.0f;
    private int _currentHealth;
    private bool _canTakeDamage = true;
    
    private Knockback _knockback;
    private Flash _flash;
    
    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy && _canTakeDamage)
        {
            TakeDamage(1);
            _knockback.GetKnockedBack(other.gameObject.transform, knockBackThrustAmount);
            _flash.DoFlash();
            StartCoroutine(RecoveryRoutine());
        }
    }
    
    private void TakeDamage(int damage)
    {
        _canTakeDamage = false;
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        if (_currentHealth <= 0)
        {
            // Destroy(gameObject);
        }
    }

    private IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }
}
