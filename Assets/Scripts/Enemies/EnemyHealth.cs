using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 3;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float _knockbackThrust = 15f;

    private Knockback _knockback;
    private int currentHealth;
    private Flash _flash;

    private void Awake()
    {
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        currentHealth = startingHealth;
    }
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        _knockback.GetKnockedBack(PlayerController.Instance.transform, _knockbackThrust);
        _flash.DoFlash();
        StartCoroutine(CheckDetectDeathRoutine());
    }

    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(_flash.GetRestoreTime());
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            GetComponent<PickupSpawner>().DropPickup();
            Destroy(gameObject);
        }
    }
}
