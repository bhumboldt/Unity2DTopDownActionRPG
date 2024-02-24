using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenshakeManager : Singleton<ScreenshakeManager>
{
    private CinemachineImpulseSource _cinemachineImpulseSource;
    
    protected override void Awake()
    {
        base.Awake();
        _cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen()
    {
        _cinemachineImpulseSource.GenerateImpulse();
    }
}
