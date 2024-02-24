using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaser : MonoBehaviour
{
    private float laserRange;
    private bool isGrowing = true;
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider;
    [SerializeField] private float laserGrowTime = 2f;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        LaserFaceMouse();
    }

    public void UpdateLaserRange(float val)
    {
        laserRange = val;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>() && other.isTrigger)
        {
            isGrowing = false;
        }
    }

    private IEnumerator IncreaseLaserLengthRoutine()
    {
        float timePassed = 0f;

        while (_spriteRenderer.size.x < laserRange && isGrowing)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / laserGrowTime;
            
            // sprite
            _spriteRenderer.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), 1f);

            // collider
            _capsuleCollider.size = new Vector2(Mathf.Lerp(1f, laserRange, linearT), _capsuleCollider.size.y);
            _capsuleCollider.offset = new Vector2(Mathf.Lerp(1f, laserRange, linearT) / 2, _capsuleCollider.offset.y);
            
            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().SlowFadeRoutine());
    }

    private void LaserFaceMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        Vector2 direction = transform.position - mousePosition;

        transform.right = -direction;
    }
}
