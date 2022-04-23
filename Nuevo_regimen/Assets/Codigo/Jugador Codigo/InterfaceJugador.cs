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
        sneakTime = maxSneakTimer;
    }

    void Update()
    {

    }

    public void Timer()
    {
        if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift))
        {
            maxSneakTimer = 6f;
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            maxSneakTimer = 6f;
        }
        else
        {
            maxSneakTimer = 3f;
        }

        visionTimer.fillAmount = sneakTime / maxSneakTimer;
    }
}
