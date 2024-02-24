using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Sword : MonoBehaviour, IWeapon
{
    private Animator _animator;

    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private float swordAttackCooldown = 0.5f;
    [SerializeField] private WeaponInfo weaponInfo;

    private GameObject slashAnim;
    private Transform weaponCollider;
    private Transform slashAnimSpawnPoint;
    
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimationSpawnPoint").transform;
    }

    // Start is called before the first frame update
    void Update()
    {
        this.MouseFollowWithOffset();
    }
    
    public void SwingUpFlipAnimation()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    public void SwingDownFlipAnimation()
    {
        slashAnim.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (PlayerController.Instance.FacingLeft)
        {
            slashAnim.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 playerPosition = Camera.main.WorldToScreenPoint((PlayerController.Instance).transform.position);
        
        // TODO: determine whether I want this or not
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        
        if (mousePosition.x < playerPosition.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, -180, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void DoneAttackingAnimation()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    public void Attack()
    {
            weaponCollider.gameObject.SetActive(true);
            _animator.SetTrigger("Attack");
            slashAnim = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
            slashAnim.transform.parent = this.transform.parent;
    }
}
