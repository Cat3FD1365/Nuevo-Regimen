using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigoNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshAgent navEnemy;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Transform playerTarget;
    int patrolIndex;
    Vector3 targetDestination;

    private Animator anim;
    private float velocity = 0.0f;
    [SerializeField] private float acceleration;

    void Start()
    {
        anim = GetComponent<Animator>();
        navEnemy = GetComponent<NavMeshAgent>();
        PatrolDestination();
    }

    void Update()
    {
        FollowPlayer();
        EnemyAnimaton();
    }

    private void PatrolDestination()
    {
        targetDestination = patrolPoints[patrolIndex].position;
        navEnemy.SetDestination(targetDestination);
    }

    private void PatrolPointIteration()
    {
        if (Vector3.Distance(transform.position, targetDestination) <= 1)
        {
            patrolIndex++;
            if (patrolIndex == patrolPoints.Length)
                patrolIndex = 0;
        }
        
    }

    private void EnemyAnimaton()
    {
        if (Vector3.Distance(transform.position, targetDestination) < 10 && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
        }
        else if (Vector3.Distance(transform.position, targetDestination) >= 10 && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
        }
        else if (velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        else if (velocity > 1.0f)
        {
            velocity = 1.0f;
        }
    }

    public void FollowPlayer()
    {
        bool followPlayer = false;

        EnemigoVisionV2 EV2 = gameObject.GetComponent<EnemigoVisionV2>();
        GameObject obj = EV2.colliders[0].gameObject;
        if (EV2.IsInSight(obj))
        {
            followPlayer = true;
        } 
        else
        {
            followPlayer = false;
        } 

        if (followPlayer == true)
        {
            navEnemy.enabled = true;
            navEnemy.SetDestination(playerTarget.position);
        }
        else if (followPlayer != true)
        {
            PatrolPointIteration();
            PatrolDestination();
        }
    }
}
