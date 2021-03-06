using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPrimeraPersona : MonoBehaviour
{
    float mouseSensitivity = 0f;
    float sensivilityTimer = 0.5f;

    [SerializeField] Transform playerBody;

    float rotation_x = 0f;

    [SerializeField] GameObject cameraPlayer;
    bool crouchCamera = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        //Esto fue una soluci?n porque la camara iniciaba viendo hacia otro lado con la sensibilidad
        sensivilityTimer -= Time.deltaTime;
        if (sensivilityTimer <= 0)
            mouseSensitivity = 0500f;
        MouseControl();
        CameraPosition();
    }

    private void MouseControl()
    {
        float mouseMovement_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseMovement_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotation_x -= mouseMovement_y;
        rotation_x = Mathf.Clamp(rotation_x, -35f, 80f);

        transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseMovement_x);
    }

    private void CameraPosition()
    {
        float lerpSpeed = 3f * Time.deltaTime;
        Vector3 initialPosition = new Vector3(0f, 1.370f, 0.250f);
        Vector3 crouchPosition = new Vector3(0f, 1.080f, 0.450f);

        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            crouchCamera = true;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            crouchCamera = true;
        }
        else
        {
            crouchCamera = false;
        }

        if (crouchCamera == true)
        {
            cameraPlayer.transform.localPosition = Vector3.Lerp(cameraPlayer.transform.localPosition, crouchPosition, lerpSpeed);
        }
        else if (crouchCamera == false)
        {
            cameraPlayer.transform.localPosition = Vector3.Lerp(cameraPlayer.transform.localPosition, initialPosition, lerpSpeed);
        }
    }
}