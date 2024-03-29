using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }
    
    [SerializeField] private int maxHealth = 5;
    [SerializeField] private float knockBackThrustAmount = 10.0f;
    [SerializeField] private float _damageRecoveryTime = 1.0f;
    private int _currentHealth;
    private bool _canTakeDamage = true;

    private Slider healthSlider;
    private Knockback _knockback;
    private Flash _flash;
    private readonly int deathHash = Animator.StringToHash("Death");
    
    protected override void Awake()
    {
        base.Awake();
        _flash = GetComponent<Flash>();
        _knockback = GetComponent<Knockback>();
    }

    private void Start()
    {
        isDead = false;
        _currentHealth = maxHealth;
        
        UpdateHealthSlider();
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();

        if (enemy)
        {
            TakeDamage(1, other.transform);
        }
    }
    
    public void TakeDamage(int damage, Transform hitTransform)
    {
        if (!_canTakeDamage)
        {
            return;
        }
        
        ScreenshakeManager.Instance.ShakeScreen();
        
        _knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        _flash.DoFlash();
        _canTakeDamage = false;
        _currentHealth -= damage;
        Debug.Log(_currentHealth);
        CheckIfPlayerDeath();
        
        StartCoroutine(RecoveryRoutine());
        UpdateHealthSlider();
    }

    private void CheckIfPlayerDeath()
    {
        if (_currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(ActiveWeapon.Instance.gameObject);
            _currentHealth = 0;
            GetComponent<Animator>().SetTrigger(deathHash);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        SceneManager.LoadScene("Scene_1");
    }
    
    public void Heal(int healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }

        UpdateHealthSlider();
    }

    private IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(_damageRecoveryTime);
        _canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = _currentHealth;
    }
}
