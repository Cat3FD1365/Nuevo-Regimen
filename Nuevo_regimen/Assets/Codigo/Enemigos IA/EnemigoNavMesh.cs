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
    int turnAroundIndex;
    float patrolTimer = 9f;
    Vector3 targetDestination;

    Animator anim;
    float velocity = 0.0f;
    [SerializeField] private float animationAcceleration = 1f;

    bool followPlayer = false;
    private bool playerOnSight;
    Vector3 playerStaticPosition = new Vector3(0, 0, 0);
    float followPlayerTimer;
    [SerializeField] float staticFollowPlayerTimer = 0.0f;

    [SerializeField] float attackRadius;
    [SerializeField] float staticAttackCooldown;


    private AudioSource audioSource;
    [SerializeField] private AudioClip[] stepClips;

    MovimientoJugador movimientoJugador;
    EnemigoVisionV2 enemigoVisionV2;

    CamaraRaycast[] camaraRaycast;
    bool cameraDetection = false;
    int cameraIteration = 0;

    void Start()
    {
        anim = GetComponent<Animator>();
        navEnemy = GetComponent<NavMeshAgent>();
        PatrolDestination();

        audioSource = GetComponent<AudioSource>();

        movimientoJugador = FindObjectOfType<MovimientoJugador>();
        enemigoVisionV2 = gameObject.GetComponent<EnemigoVisionV2>();

        camaraRaycast = FindObjectsOfType<CamaraRaycast>();
    }

    void Update()
    {
        FollowPlayer();
        LookAtReference();
        EnemyAnimaton();
        PlayerPositionReference();
        FollowVisionDistance();
    }

    private void PatrolDestination()
    {
        targetDestination = patrolPoints[patrolIndex].position;
        navEnemy.SetDestination(targetDestination);
    }

    private void PatrolPointIteration()
    {
        if (Vector3.Distance(transform.position, targetDestination) <= 3f)
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
            velocity -= Time.deltaTime * animationAcceleration;
            anim.SetFloat("Speed", velocity);
        }
        else if (Vector3.Distance(transform.position, targetDestination) >= 10 && velocity < 1.0f
            || Vector3.Distance(transform.position, playerTarget.position) >= 10 && velocity < 1.0f)
        {
            velocity += Time.deltaTime * animationAcceleration;
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

    private void FollowPlayer()
    {
        PlayerCameraDetection();

        //EnemigoVisionV2 enemigoVisionV2 = gameObject.GetComponent<EnemigoVisionV2>();
        GameObject obj = enemigoVisionV2.colliders[0].gameObject;
        float followDistance = Vector3.Distance(playerTarget.position, transform.position);

        float attackCooldown = 0;
        if (followDistance <= attackRadius && attackCooldown <= 0.0f)
        {
            anim.SetTrigger("Attack");
            attackCooldown = staticAttackCooldown;
        }
        else if (attackCooldown > 0.0f)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (attackCooldown <= 0.0f)
        {
            attackCooldown = 0;
        }

        if (enemigoVisionV2.IsInSight(obj) || movimientoJugador.movementSound == 4 && followDistance <= enemigoVisionV2.distance || cameraDetection == true)
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

    private void FollowVisionDistance()
    {
        if (movimientoJugador.movementSound == 0)
        {
            enemigoVisionV2.distance = 45;
        }
        else if (movimientoJugador.movementSound == 1)
        {
            enemigoVisionV2.distance = 35;
        }
        else if (movimientoJugador.movementSound == 2)
        {
            enemigoVisionV2.distance = 40;
        }
        else if (movimientoJugador.movementSound == 3)
        {
            enemigoVisionV2.distance = 50;
        }
        else if (movimientoJugador.movementSound == 4)
        {
            enemigoVisionV2.distance = 55;
        }
    }

    private void PlayerPositionReference()
    {
        if (playerOnSight == true)
        {
            playerStaticPosition = new Vector3(playerTarget.position.x, 0, playerTarget.position.z);
            followPlayerTimer = staticFollowPlayerTimer;
        }
        else if (playerOnSight == false)
        {
            followPlayerTimer -= Time.deltaTime;
            if (followPlayerTimer > 0.0f)
            {
                playerStaticPosition = new Vector3(playerTarget.position.x, 0, playerTarget.position.z);
            }
            else if (followPlayerTimer <= 0.0f)
            {

            }
        }
    }

    private void FollowPlayerLastPosition()
    {
        targetDestination = playerStaticPosition;
        navEnemy.SetDestination(targetDestination);
        if (Vector3.Distance(transform.position, targetDestination) <= 3f)
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

    private void PlayerCameraDetection()
    {
        if (camaraRaycast[cameraIteration].playerOnCamera == true)
        {
            int goToCamera;
            goToCamera = Random.Range(0, 25);
            if (goToCamera == 1)
            {
                cameraDetection = true;
            }
        }
        else
        {
            cameraDetection = false;
            cameraIteration++;
            if (cameraIteration == camaraRaycast.Length)
                cameraIteration = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
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
