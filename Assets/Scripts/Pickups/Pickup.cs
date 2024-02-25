using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pickup : MonoBehaviour
{
    private enum PickupType
    {
        Health,
        StaminaGlobe,
        GoldCoin
    }
    
    [SerializeField] private PickupType pickupType;
    [SerializeField] private float pickupDistance = 5f;
    [SerializeField] private float accelerationRate = 0.3f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 1f;
    
    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update()
    {
        Vector3 playerPos = PlayerController.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickupDistance)
        {
            moveDirection = (playerPos - transform.position).normalized;
            moveSpeed += accelerationRate;
        }
        else
        {
            moveDirection = Vector3.zero;
            moveSpeed = 0f;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = moveDirection * moveSpeed * Time.fixedDeltaTime;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            DetectPickupType();
            Destroy(gameObject);
        }
    }

    private IEnumerator AnimCurveSpawnRoutine()
    {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);
        Vector2 endPoint = new Vector2(randomX, randomY);
        
        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }

    private void DetectPickupType()
    {
        switch (pickupType)
        {
            case PickupType.Health:
                PlayerHealth.Instance.Heal(1);
                break;
            case PickupType.GoldCoin:
                EconomyManager.Instance.UpdateCurrentGold();
                break;
            case PickupType.StaminaGlobe:
                Stamina.Instance.AddStamina(1);
                break;
            default:
                break;
        }
    }
}
