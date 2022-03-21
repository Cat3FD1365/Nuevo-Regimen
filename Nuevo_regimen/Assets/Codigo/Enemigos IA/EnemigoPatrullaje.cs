using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoPatrullaje : MonoBehaviour
{
    float movementSpeed = 5f;
    float waitTime;
    float startWaitTime = 1f;

    [SerializeField] Transform[] availableSpots;
    private int randomSpot;

    void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, availableSpots.Length);
    }

    void Update()
    {
        EnemyMovement();
    }

    private void EnemyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, availableSpots[randomSpot].position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, availableSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, availableSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
