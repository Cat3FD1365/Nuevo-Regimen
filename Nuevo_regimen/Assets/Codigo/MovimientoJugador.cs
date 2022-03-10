using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private float moveSpeed;
    [SerializeField] private float walkSpeed;
    private Vector3 moveDirection;
    private Vector3 vectorGravity;
    private CharacterController playerController;

    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    void Update()
    {
        PlayerMovement();
        Gravity();
    }

    private void PlayerMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveX, 0, moveZ);

        moveDirection *= walkSpeed;

        playerController.Move(moveDirection * Time.deltaTime);
    }

    private void Gravity()
    {
        vectorGravity = Vector3.zero;

        if (playerController.isGrounded == false)
            vectorGravity += Physics.gravity;

        playerController.Move(vectorGravity * Time.deltaTime);
    }
}
