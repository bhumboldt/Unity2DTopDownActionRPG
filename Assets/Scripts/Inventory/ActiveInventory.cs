using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActiveInventory : MonoBehaviour
{
    private int activeSlotIndexNum = 0;

    private PlayerControls _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControls();
    }

    public void Start()
    {
        _playerControls.Inventory.Keyboard.performed +=
            ctx => ToggleActiveSlot((int)ctx.ReadValue<float>());
        
        ToggleActiveHighlight(0);
    }
    
    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void ToggleActiveSlot(int numValue)
    {
        ToggleActiveHighlight(numValue - 1);
    }

    private void ToggleActiveHighlight(int index)
    {
        activeSlotIndexNum = index;

        foreach (Transform inventorySlot in transform)
        {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }
        
        transform.GetChild(activeSlotIndexNum).GetChild(0).gameObject.SetActive(true);
        
        ChangeActiveWeapon();
    }

    private void ChangeActiveWeapon()
    {
        if (ActiveWeapon.Instance.CurrentActiveWeapon != null)
        {
            Destroy(ActiveWeapon.Instance.CurrentActiveWeapon.gameObject);
        }

        if (!transform.GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>())
        {
            ActiveWeapon.Instance.WeaponNull();
            return;
        }
        
        GameObject weaponToSpawn = transform.
            GetChild(activeSlotIndexNum).GetComponentInChildren<InventorySlot>().GetWeaponInfo()
            .weaponPrefab;
        GameObject newWeapon =
            Instantiate(weaponToSpawn, ActiveWeapon.Instance.transform.position, Quaternion.identity);
        newWeapon.transform.parent = ActiveWeapon.Instance.transform;
        
        ActiveWeapon.Instance.NewWeapon(newWeapon.GetComponent<MonoBehaviour>());
    }
}
