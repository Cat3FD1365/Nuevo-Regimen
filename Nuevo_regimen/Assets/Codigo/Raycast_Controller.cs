using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_Controller : MonoBehaviour
{

    [SerializeField] LayerMask layermask;
    void Update()
    {
        if(Physics.Raycast(transform.position, transform.TransformDirection (Vector3.forward), out RaycastHit hitinfo, 2f, layermask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 2f, Color.green);
        }
    }
}
