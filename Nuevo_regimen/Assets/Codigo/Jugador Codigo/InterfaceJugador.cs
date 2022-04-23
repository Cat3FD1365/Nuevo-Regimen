using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceJugador : MonoBehaviour
{
    [SerializeField] Image visionTimer;
    public float maxSneakTimer = 1.5f;
    public float sneakTime;

    void Start()
    {

    }

    void Update()
    {
        Timer();
    }

    public void Timer()
    {
        EnemigoNavMesh enemigoNavMesh = gameObject.GetComponent<EnemigoNavMesh>();

        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            maxSneakTimer = 3f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            maxSneakTimer = 3f;
        }
        else
        {
            maxSneakTimer = 1.5f;
        }

        visionTimer.fillAmount = sneakTime / maxSneakTimer;
    }
}
