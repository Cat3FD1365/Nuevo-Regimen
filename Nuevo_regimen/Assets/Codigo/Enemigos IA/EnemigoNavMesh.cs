using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemigoNavMesh : MonoBehaviour
{
    [SerializeField] NavMeshAgent navEnemy;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] Transform playerTarget;
    [SerializeField] Transform[] turnAround;
    int patrolIndex;
    [SerializeField] int turnAroundIndex;
    [SerializeField] float patrolTimer = 9f;
    Vector3 targetDestination;

    Animator anim;
    float velocity = 0.0f;
    [SerializeField] private float acceleration;

    bool followPlayer = false;
    bool playerOnSight;
    Vector3 playerStaticPosition = new Vector3(0, 0, 0);
    float folloPlayerTimer = 1.0f;

    private AudioSource audioSource;
    [SerializeField] private AudioClip[] stepClips;

    void Start()
    {
        anim = GetComponent<Animator>();
        navEnemy = GetComponent<NavMeshAgent>();
        PatrolDestination();

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        FollowPlayer();
        LookAtReference();
        EnemyAnimaton();
        PlayerPositionReference();
    }

    private void PatrolDestination()
    {
        targetDestination = patrolPoints[patrolIndex].position;
        navEnemy.SetDestination(targetDestination);
    }

    private void PatrolPointIteration()
    {
        if (Vector3.Distance(transform.position, targetDestination) <= 1f)
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 8 && patrolTimer >= 6)
            {
                turnAroundIndex = 0;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 6 && patrolTimer >= 4)
            {
                turnAroundIndex = 3;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 4 && patrolTimer >= 2)
            {
                turnAroundIndex = 1;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 2 && patrolTimer >= 0)
            {
                turnAroundIndex = 2;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 0)
            {
                patrolIndex++;
                if (patrolIndex == patrolPoints.Length)
                    patrolIndex = 0;
                patrolTimer = 9f;
            }
        }
    }

    private void LookAtReference()
    {
        Vector3 enemyPosition = new Vector3(navEnemy.transform.position.x, 0, navEnemy.transform.position.z);
        turnAround[0].transform.position = new Vector3(enemyPosition.x + 1, 0, enemyPosition.z);
        turnAround[1].transform.position = new Vector3(enemyPosition.x - 1, 0, enemyPosition.z);
        turnAround[2].transform.position = new Vector3(enemyPosition.x, 0, enemyPosition.z + 1);
        turnAround[3].transform.position = new Vector3(enemyPosition.x, 0, enemyPosition.z - 1);
    }

    private void EnemyAnimaton()
    {
        if (Vector3.Distance(transform.position, targetDestination) < 10 && velocity > 0.0f 
            || Vector3.Distance(transform.position, playerTarget.position) < 10 && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
        }
        else if (Vector3.Distance(transform.position, targetDestination) >= 10 && velocity < 1.0f
            || Vector3.Distance(transform.position, playerTarget.position) >= 10 && velocity < 1.0f)
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
        EnemigoVisionV2 enemigoVisionV2 = gameObject.GetComponent<EnemigoVisionV2>();
        GameObject obj = enemigoVisionV2.colliders[0].gameObject;

        if (enemigoVisionV2.IsInSight(obj))
        {
            followPlayer = true;
            playerOnSight = true;
            if (followPlayer == true)
            {
                navEnemy.enabled = true;
                navEnemy.SetDestination(playerTarget.position);
                patrolTimer = 9f;
            }
        }
        else
        {
            playerOnSight = false;
            if (followPlayer == true)
            {
                FollowPlayerLastPosition();
            }
            else if (followPlayer == false)
            {
                PatrolPointIteration();
                PatrolDestination();
            }
        }
    }

    private void PlayerPositionReference()
    {
        if (playerOnSight == true)
        {
            playerStaticPosition = new Vector3(playerTarget.position.x, 0, playerTarget.position.z);
            folloPlayerTimer = 1.0f;
        }
        else if (playerOnSight == false)
        {
            folloPlayerTimer -= Time.deltaTime;
            if (folloPlayerTimer > 0.0f)
            {
                playerStaticPosition = new Vector3(playerTarget.position.x, 0, playerTarget.position.z);
            }
            else if (folloPlayerTimer <= 0.0f)
            {

            }
        }
    }

    private void FollowPlayerLastPosition()
    {
        targetDestination = playerStaticPosition;
        navEnemy.SetDestination(targetDestination);
        if (Vector3.Distance(transform.position, targetDestination) <= 1f)
        {
            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 8 && patrolTimer >= 6)
            {
                turnAroundIndex = 0;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 6 && patrolTimer >= 4)
            {
                turnAroundIndex = 3;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 4 && patrolTimer >= 2)
            {
                turnAroundIndex = 1;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 2 && patrolTimer >= 0)
            {
                turnAroundIndex = 2;
                Vector3 direction = (turnAround[turnAroundIndex].position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 7);
            }
            if (patrolTimer <= 0)
            {
                patrolTimer = 9f;
                followPlayer = false;
            }
        }
    }

    private void Step_Sound()
    {
        AudioClip clip = StepClip();
        audioSource.PlayOneShot(clip);
    }

    private AudioClip StepClip()
    {
        return stepClips[UnityEngine.Random.Range(0, stepClips.Length)];
    }
}
