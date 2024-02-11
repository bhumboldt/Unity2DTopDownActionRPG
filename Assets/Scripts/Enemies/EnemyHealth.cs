using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;

    private Knockback _knockback;
    private int currentHealth;

    private void Awake()
    {
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        _knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
