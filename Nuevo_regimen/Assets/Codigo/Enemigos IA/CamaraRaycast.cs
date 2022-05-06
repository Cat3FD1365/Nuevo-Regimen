using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraRaycast : MonoBehaviour
{
    [SerializeField] float distance;
    [SerializeField] Vector3[] objectDirection;
    [SerializeField] float[] anglesX;
    [SerializeField] float[] anglesY;
    [SerializeField] LayerMask layermask;
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
        objectDirection[0] = Quaternion.Euler(anglesX[0], anglesY[0], 0) * transform.forward;
        objectDirection[1] = Quaternion.Euler(anglesX[1], anglesY[0], 0) * transform.forward;
        objectDirection[2] = Quaternion.Euler(anglesX[2], anglesY[0], 0) * transform.forward;
        objectDirection[3] = Quaternion.Euler(anglesX[0], anglesY[1], 0) * transform.forward;
        objectDirection[4] = Quaternion.Euler(anglesX[0], anglesY[2], 0) * transform.forward;
        if (Physics.Raycast(gameObject.transform.position, objectDirection[0], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[1], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[2], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[3], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[4], distance, layermask))
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
        objectDirection[0] = Quaternion.Euler(anglesX[0], anglesY[0], 0) * transform.forward;
        objectDirection[1] = Quaternion.Euler(anglesX[1], anglesY[0], 0) * transform.forward;
        objectDirection[2] = Quaternion.Euler(anglesX[2], anglesY[0], 0) * transform.forward;
        objectDirection[3] = Quaternion.Euler(anglesX[0], anglesY[1], 0) * transform.forward;
        objectDirection[4] = Quaternion.Euler(anglesX[0], anglesY[2], 0) * transform.forward;
        if (Physics.Raycast(gameObject.transform.position, objectDirection[0], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[1], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[2], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[3], distance, layermask)
            || Physics.Raycast(gameObject.transform.position, objectDirection[4], distance, layermask))
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawRay(gameObject.transform.position, objectDirection[0] * distance);
        Gizmos.DrawRay(gameObject.transform.position, objectDirection[1] * distance);
        Gizmos.DrawRay(gameObject.transform.position, objectDirection[2] * distance);
        Gizmos.DrawRay(gameObject.transform.position, objectDirection[3] * distance);
        Gizmos.DrawRay(gameObject.transform.position, objectDirection[4] * distance);
    }
}
