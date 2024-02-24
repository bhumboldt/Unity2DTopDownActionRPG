using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHitVFX;

    private WeaponInfo _weaponInfo;
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
    
    public void UpdateWeaponInfo(WeaponInfo weaponInfo)
    {
        _weaponInfo = weaponInfo;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = other.gameObject.GetComponent<Indestructible>();

        if (!other.isTrigger && (enemyHealth || indestructible))
        {
            Instantiate(particleOnHitVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void DetectDistance()
    {
        if (Vector3.Distance(transform.position, _startPosition) > _weaponInfo.weaponRage)
        {
            Destroy(gameObject);
        }
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
