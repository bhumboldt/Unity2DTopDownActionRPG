using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitVFX;
    [SerializeField] private bool isEnemyProjectile;

    [SerializeField] private float projectileRange = 10f;
    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        this.MoveProjectile();
        DetectDistance();
    }
    
    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
        
        if (!other.isTrigger && (enemyHealth || indestructible || playerHealth))
        {
            if ((playerHealth && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                playerHealth?.TakeDamage(1, gameObject.transform);
                Instantiate(particleOnHitVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            } else if (!other.isTrigger && indestructible)
            {
                Instantiate(particleOnHitVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    public void DetectDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
