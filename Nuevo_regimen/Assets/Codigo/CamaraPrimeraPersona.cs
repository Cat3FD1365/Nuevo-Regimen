using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraPrimeraPersona : MonoBehaviour
{
    float mouseSensitivity = 300f;

    [SerializeField] Transform playerBody;

    float rotation_x = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MouseControl();
    }

    private void MouseControl()
    {
        float mouseMovement_x = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseMovement_y = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotation_x -= mouseMovement_y;
        rotation_x = Mathf.Clamp(rotation_x, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotation_x, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseMovement_x);
    }
}
