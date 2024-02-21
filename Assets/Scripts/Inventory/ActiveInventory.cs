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
    }
}
