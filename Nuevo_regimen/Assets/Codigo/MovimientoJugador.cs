using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [SerializeField] CharacterController characterControler;

    float movementSpeed = 12f;

    void Start()
    {

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

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;

        characterControler.Move(moveDirection * movementSpeed * Time.deltaTime);
    }

    private void Gravity()
    {
       Vector3 moveVector = Vector3.zero;

        if (characterControler.isGrounded == false)
            moveVector += Physics.gravity;

        characterControler.Move(moveVector * Time.deltaTime);
    }
}
