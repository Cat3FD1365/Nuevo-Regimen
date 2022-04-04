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

    private Animator anim;
    private float velocity = 0.0f;
    [SerializeField] private float acceleration;

    void Start()
    {
        anim = GetComponent<Animator>();
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, availableSpots.Length);
    }

    void Update()
    {
        EnemyMovement();
        EnemyAnimaton();
    }

    private void EnemyMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, availableSpots[randomSpot].position, 
            movementSpeed * Time.deltaTime);

        transform.LookAt(availableSpots[randomSpot]);

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

    private void EnemyAnimaton()
    {
        if (Vector3.Distance(transform.position, availableSpots[randomSpot].position) < 10 && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
        }
        else if (Vector3.Distance(transform.position, availableSpots[randomSpot].position) >= 10 && velocity < 1.0f)
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
}
