using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentActiveWeapon;
    
    private PlayerControls _playerControls;
    
    private bool attackButtonDown, isAttacking = false;
    
    protected override void Awake()
    {
        base.Awake();
        _playerControls = new PlayerControls();
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;
    }

    private void Start()
    {
        var attackAction = _playerControls.Combat.Attack;
        attackAction.performed += _ => StartAttacking();
        attackAction.canceled += _ => StopAttacking();
    }

    private void Update()
    {
        Attack();
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
            isAttacking = true;
            (currentActiveWeapon as IWeapon).Attack(); 
        }
    }
}
