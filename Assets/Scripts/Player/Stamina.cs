using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    private float maxStamina;
    private int startingStamina = 3;
    private Transform staminaContainer;
    private const string STAMINA_CONTAINER_TEXT = "StaminaContainer";

    [SerializeField] private int timeBetweenStaminaRefresh = 3;
    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    
    public int CurrentStamina { get; private set; }

    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }

    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }
    
    public void UseStamina(int amount)
    {
        CurrentStamina -= amount;
        UpdateStaminaUI();
    }
    
    public void AddStamina(int amount)
    {
        if (CurrentStamina < maxStamina)
        {
            CurrentStamina += amount;
            UpdateStaminaUI();
        }
    }

    private IEnumerator RefreshStaminaRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRefresh);
            AddStamina(1);
        }
    }

    private void UpdateStaminaUI()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            if (i <= CurrentStamina - 1)
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = fullStaminaImage;
            }
            else
            {
                staminaContainer.GetChild(i).GetComponent<Image>().sprite = emptyStaminaImage;
            }
        }

        if (CurrentStamina < maxStamina)
        {
            StopAllCoroutines();
            StartCoroutine(RefreshStaminaRoutine());
        }
    }
}
