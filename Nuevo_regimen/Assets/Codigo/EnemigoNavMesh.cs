using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemigoNavMesh : MonoBehaviour
{
    float movementSpeed = 5f;
    float waitTime;
    float startWaitTime = 3f;

    [SerializeField] NavMeshAgent navEnemy;
    [SerializeField] Transform[] target;

    private int randomSpot;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, target.Length);
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        float distance = Vector3.Distance(transform.position, target[randomSpot].position);
        navEnemy.enabled = true;
        navEnemy.SetDestination(target[randomSpot].position);

        if (Vector3.Distance(transform.position, target[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, target.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
