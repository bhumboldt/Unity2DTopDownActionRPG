using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;
    
    private Camera _camera;
    private Vector2 startPos;
    private Vector2 travel => (Vector2)_camera.transform.position - startPos;

    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Start()
    {
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = startPos + travel * parallaxOffset;
    }
}
