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
    [SerializeField] float patrolTimer = 2f;
    Vector3 targetDestination;

    Animator anim;
    float velocity = 0.0f;
    [SerializeField] private float acceleration;

    InterfaceJugador interfaceJugador;

    void Start()
    {
        anim = GetComponent<Animator>();
        navEnemy = GetComponent<NavMeshAgent>();
        PatrolDestination();

        interfaceJugador = FindObjectOfType<InterfaceJugador>();
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
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0)
            {
                patrolIndex++;
                if (patrolIndex == patrolPoints.Length)
                    patrolIndex = 0;
                patrolTimer = 2f;
            }
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
        /*if (interfaceJugador.BeCaugth() == true)
        {
            navEnemy.enabled = true;
            navEnemy.SetDestination(playerTarget.position);
        }
        else if (interfaceJugador.BeCaugth() == false)
        {
            PatrolPointIteration();
            PatrolDestination();
        }*/

        bool followPlayer = false;

        EnemigoVisionV2 enemigoVisionV2 = gameObject.GetComponent<EnemigoVisionV2>();
        GameObject obj = enemigoVisionV2.colliders[0].gameObject;
        if (enemigoVisionV2.IsInSight(obj))
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

        /*bool followPlayer = false;

        EnemigoVisionV2 enemigoVisionV2 = gameObject.GetComponent<EnemigoVisionV2>();
        GameObject obj = enemigoVisionV2.colliders[0].gameObject;

        InterfaceJugador interfaceJugador = gameObject.GetComponent<InterfaceJugador>();
        interfaceJugador.Timer();
        if (enemigoVisionV2.IsInSight(obj))
        {
            interfaceJugador.sneakTime -= Time.deltaTime / 2;
            if (interfaceJugador.sneakTime <= 0)
            {
                interfaceJugador.sneakTime = 0;
                followPlayer = true;
            }
            else
            {
                followPlayer = false;
            }
        }
        else
        {
            interfaceJugador.sneakTime += Time.deltaTime / 2;
            if (interfaceJugador.sneakTime >= interfaceJugador.maxSneakTimer)
            {
                interfaceJugador.sneakTime = interfaceJugador.maxSneakTimer;
            }
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
        }*/
    }
}
