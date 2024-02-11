using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming
    };

    [SerializeField] private float roamingChangeDirFloat = 2f;

    private State state;
    private EnemyPathfinding pathfinding;

    private void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    
    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }

    private IEnumerator RoamingRoutine()
    {
        while (state == State.Roaming)
        {
            Vector2 roamingPosition = GetRoamingPosition();
            pathfinding.SetMoveDirection(roamingPosition);
            yield return new WaitForSeconds(roamingChangeDirFloat);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
