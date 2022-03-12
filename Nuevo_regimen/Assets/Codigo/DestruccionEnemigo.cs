using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestruccionEnemigo : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player Fist")
        {
            Destroy(gameObject);
        }
    }
}
