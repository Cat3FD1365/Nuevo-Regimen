using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private float velocity = 0.0f;
    [SerializeField] private float acceleration;
    private Animator anim;
    [SerializeField] SphereCollider handCollider;

    private float moveSpeed;
    private float rotationSpeed = 100;
    [SerializeField] private float walkSpeed;
    private Vector3 moveDirection;
    private Vector3 vectorGravity;
    private Vector3 moveRotation;
    private CharacterController playerController;

    private bool CanMove = true;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        handCollider.enabled = false;
    }

    void Update()
    {
        PlayerMovement();
        PlayerRotation();
        Gravity();
        PlayerAttack();
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);

        if (moveDirection == Vector3.zero && velocity > 0.0f)
        {
            velocity -= Time.deltaTime * acceleration;
            AnimationIdle();
        }
        else if (moveDirection != Vector3.zero && velocity < 1.0f)
        {
            velocity += Time.deltaTime * acceleration;
            AnimationRun();
        }
        else if (velocity < 0.0f)
        {
            velocity = 0.0f;
        }
        else if (velocity > 1.0f)
        {
            velocity = 1.0f;
        }

        moveDirection *= walkSpeed;

        playerController.Move(moveDirection * Time.deltaTime);
    }

    private void PlayerRotation()
    {
        float rotateX = Input.GetAxis("Horizontal");

        moveRotation = new Vector3(rotateX, 0, 0);
        moveRotation.Normalize();

        transform.Translate(moveRotation * moveSpeed * Time.deltaTime, Space.World);

        if (moveRotation != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveRotation, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void Gravity()
    {
        vectorGravity = Vector3.zero;

        if (playerController.isGrounded == false)
            vectorGravity += Physics.gravity;

        playerController.Move(vectorGravity * Time.deltaTime);
    }

    private void AnimationIdle()
    {
        anim.SetFloat("Speed", velocity);
    }

    private void AnimationWalk()
    {
        anim.SetFloat("Speed", velocity);
    }

    private void AnimationRun()
    {
        anim.SetFloat("Speed", velocity);
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void AttackBoolActive()
    {
        handCollider.enabled = true;
    }

    private void AttackBoolFalse()
    {
        handCollider.enabled = false;
    }
}
