using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TransparentDetection : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float transparencyAmount = 0.8f;
    [SerializeField] private float transparencyFadeTime = 0.4f;
    
    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, transparencyFadeTime, _spriteRenderer.color.a,
                    transparencyAmount));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, transparencyFadeTime, _tilemap.color.a, transparencyAmount));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            if (_spriteRenderer)
            {
                StartCoroutine(FadeRoutine(_spriteRenderer, transparencyFadeTime, _spriteRenderer.color.a, 1));
            }
            else if (_tilemap)
            {
                StartCoroutine(FadeRoutine(_tilemap, transparencyFadeTime, _tilemap.color.a, 1));
            }
        }
    }

    private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue,
        float targetTransparency)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
    
    private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue,
        float targetTransparency)
    {
        float elapsedTime = 0;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
            yield return null;
        }
    }
}
