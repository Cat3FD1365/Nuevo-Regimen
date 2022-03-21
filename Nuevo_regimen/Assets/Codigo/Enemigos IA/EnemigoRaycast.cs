using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoRaycast : MonoBehaviour
{
    float radius;
    [SerializeField] [Range(0, 360)] float angle;

    GameObject playerReference;

    LayerMask targetMask;
    LayerMask obstructionMask;

    bool canSeePlayer;

    void Start()
    {
        playerReference = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FieldOfViewRoutine());
    }

    private IEnumerator FieldOfViewRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FieldOfViewCheck();
        }
    }

    private void FieldOfViewCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector3 directionTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.position, directionTarget) < angle / 2)
            {
                float distanceTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionTarget, distanceTarget, obstructionMask))
                    canSeePlayer = true;
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer == true)
            canSeePlayer = false;
    }
}
