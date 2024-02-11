using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMaterial;
    [SerializeField] private float restoreTime = 0.2f;
    
    private Material _defaultMaterial;
    private SpriteRenderer _spriteRenderer;
    
    public float GetRestoreTime()
    {
        return restoreTime;
    }
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultMaterial = GetComponent<SpriteRenderer>().material;
    }
    
    public void DoFlash()
    {
        StartCoroutine(FlashRoutine());
    }
    
    private IEnumerator FlashRoutine()
    {
        _spriteRenderer.material = whiteFlashMaterial;
        yield return new WaitForSeconds(restoreTime);
        _spriteRenderer.material = _defaultMaterial;
    }
}
