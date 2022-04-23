using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] CharacterController characterControler;

    float movementSpeed = 6f;
    Vector3 moveDirection;

    private Animator anim;
    [SerializeField] float velocity = 0.0f;
    [SerializeField] float acceleration;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerMovement();
        Gravity();
        PlayerAnimaton();
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = transform.right * moveX + transform.forward * moveZ;

        characterControler.Move(moveDirection * movementSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            movementSpeed = 12;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 3;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 4.5f;
        }
        else
        {
            movementSpeed = 6f;
        }
    }

    private void Gravity()
    {
       Vector3 moveVector = Vector3.zero;

        if (characterControler.isGrounded == false)
            moveVector += Physics.gravity;

        characterControler.Move(moveVector * Time.deltaTime);
    }

    private void PlayerAnimaton()
    {
        if (moveDirection == Vector3.zero && velocity >= 0.0f && !Input.GetKey(KeyCode.LeftControl)
            || moveDirection != Vector3.zero && velocity > 0.5f && !Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl))
        {
            velocity -= Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity <= 0.0f)
            {
                velocity = 0.0f;
            }
        }
        else if (moveDirection != Vector3.zero && velocity <= 0.5f && !Input.GetKey(KeyCode.LeftShift))
        {
            velocity += Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity >= 0.5f)
            {
                velocity = 0.5f;
            }
        }
        else if (moveDirection != Vector3.zero && velocity <= 1.0f && Input.GetKey(KeyCode.LeftShift))
        {
            velocity += Time.deltaTime * acceleration;
            anim.SetFloat("Speed", velocity);
            if (velocity >= 1.0f)
            {
                velocity = 1.0f;
            }
        }

        if (Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", true);
        }
        else if (!Input.GetKey(KeyCode.LeftControl))
        {
            anim.SetBool("Crouch", false);
        }
    }
}
