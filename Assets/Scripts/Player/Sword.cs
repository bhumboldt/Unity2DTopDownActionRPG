using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private ActiveWeapon _activeWeapon;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;
    [SerializeField] private InputActionAsset inputActions;
    [SerializeField] private float swordAttackCooldown = 0.5f;

    private bool attackButtonDown, isAttacking = false;

    private GameObject slashAnim;
    
    private void Awake()
    {
        var attackAction = inputActions.FindActionMap("Combat").FindAction("Attack");
        attackAction.performed += _ => StartAttacking();
        attackAction.canceled += _ => StopAttacking();
        
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerController = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Update()
    {
        this.MouseFollowWithOffset();
        Attack();
    }
    
    public void SwingUpFlipAnimation()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (_playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void SwingDownFlipAnimation()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (_playerController.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint(_playerController.transform.position);
        
        // TODO: determine whether I want this or not
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
        if (mousePosition.x < playerPosition.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DoneAttackingAnimation()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void StartAttacking()
    {
        attackButtonDown = true;
    }

    private void StopAttacking()
    {
        attackButtonDown = false;
    }

    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(swordAttackCooldown);
        isAttacking = false;
    }

    private void Attack()
    {
        if (attackButtonDown && !isAttacking)
        {
            isAttacking = true;
            weaponCollider.gameObject.SetActive(true);
            _animator.SetTrigger("Attack");
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        }
    }
}
