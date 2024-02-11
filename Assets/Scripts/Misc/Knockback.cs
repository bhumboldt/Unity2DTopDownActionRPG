using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public bool gettingKnockedBack { get; private set; }
    
    [SerializeField] private float knockbackTime = 0.2f;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void GetKnockedBack(Transform damageSource, float knockbackForce)
    {
        gettingKnockedBack = true;
        // TODO: learn about this
        Vector2 difference = (transform.position - damageSource.position).normalized * knockbackForce * _rigidbody.mass;
        _rigidbody.AddForce(difference, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockbackTime);
        _rigidbody.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
