using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{
    private enum State
    {
        Roaming,
        Attacking
    };

    [SerializeField] private float roamingChangeDirFloat = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private State state;
    private EnemyPathfinding pathfinding;
    private Vector2 roamPosition;
    private float timeRoaming = 0f;
    private bool canAttack = true;

    private void Awake()
    {
        pathfinding = GetComponent<EnemyPathfinding>();
        state = State.Roaming;
    }
    
    private void Start()
    {
        roamPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl()
    {
        switch (state)
        {
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
            default:
                break;
        }
    }

    private void Roaming()
    {
        timeRoaming += Time.deltaTime;
        pathfinding.SetMoveDirection(roamPosition);
        
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }
        
        if (timeRoaming >= roamingChangeDirFloat)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        
        if (canAttack && attackRange != 0)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                pathfinding.StopMoving();
            }
            else
            {
                pathfinding.SetMoveDirection(roamPosition);
            }
            
            StartCoroutine(AttackCooldownRoutine());
        }
    }

    private IEnumerator AttackCooldownRoutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
