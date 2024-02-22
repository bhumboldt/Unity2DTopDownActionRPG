using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; }
    
    private PlayerControls _playerControls;
    private float timeBetweenAttacks;
    
    private bool attackButtonDown, isAttacking = false;
    
    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
    }

    private void Start()
    {
        var attackAction = _playerControls.Combat.Attack;
        attackAction.performed += _ => StartAttacking();
        attackAction.canceled += _ => StopAttacking();
        
        AttackCooldown();
    }

    private void Update()
    {
        Attack();
    }

    private void AttackCooldown()
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttacksRoutine());
    }

    private IEnumerator TimeBetweenAttacksRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttacks = (CurrentActiveWeapon as IWeapon).GetWeaponInfo().weaponCooldown;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null;
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            AttackCooldown();
            isAttacking = true;
            (CurrentActiveWeapon as IWeapon).Attack(); 
        }
    }
}
