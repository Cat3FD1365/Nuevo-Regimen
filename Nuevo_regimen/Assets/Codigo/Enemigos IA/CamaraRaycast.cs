using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRaycast : MonoBehaviour
{
    RaycastHit raycastTest;
    [SerializeField] float distance;
    Vector3 objectDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Raycast();
    }

    private void Raycast()
    {
        objectDirection = Quaternion.Euler(0, 0, 0) * transform.forward;
        if (Physics.Raycast(gameObject.transform.position, objectDirection, distance))
        {
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.Log("Did not Hit");
        }

    }

    private void OnDrawGizmos()
    {
        if (Physics.Raycast(gameObject.transform.position, objectDirection, distance))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        objectDirection = transform.TransformDirection(Vector3.forward) * distance;
        Gizmos.DrawRay(gameObject.transform.position, objectDirection);
    }
}
