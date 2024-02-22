using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    private void Update()
    {
        MouseFollowWithOffset();
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
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public void Attack()
    {
        Debug.Log("Staff Attack");
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
}
