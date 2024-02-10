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

    private GameObject slashAnim;
    
    private void Awake()
    {
        _activeWeapon = GetComponentInParent<ActiveWeapon>();
        _playerController = GetComponentInParent<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Update()
    {
        this.MouseFollowWithOffset();
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
        // float angle = (float)Math.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
        if (mousePosition.x < playerPosition.x)
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            _activeWeapon.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void OnAttack(InputValue value)
    {
        _animator.SetTrigger("Attack");
        slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slashAnim.transform.parent = this.transform.parent;
    }
}
