using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigoNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshAgent navEnemy;
    [SerializeField] Transform[] patrolPoints;
    int patrolIndex;
    Vector3 targetDestination;

    void Start()
    {
        navEnemy = GetComponent<NavMeshAgent>();
        PatrolDestination();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, targetDestination) < 1)
        {
            PatrolPointIteration();
            PatrolDestination();
        }
    }

    private void PatrolDestination()
    {
        targetDestination = patrolPoints[patrolIndex].position;
        navEnemy.SetDestination(targetDestination);
    }

    private void PatrolPointIteration()
    {
        patrolIndex++;
        if (patrolIndex == patrolPoints.Length)
            patrolIndex = 0;
    }
}
