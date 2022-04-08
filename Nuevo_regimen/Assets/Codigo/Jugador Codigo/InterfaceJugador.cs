using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceJugador : MonoBehaviour
{
    [SerializeField] private Image visionTimer;
    private float maxSneakTimer = 3f;
    [SerializeField] private float sneakTime;

    EnemigoVisionV2 enemigoVisionV2;

    void Start()
    {
        enemigoVisionV2 = FindObjectOfType<EnemigoVisionV2>();
        sneakTime = maxSneakTimer;
    }

    void Update()
    {

    }

    public bool BeCaugth()
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

        GameObject obj = enemigoVisionV2.colliders[0].gameObject;
        if (enemigoVisionV2.IsInSight(obj))
        {
            sneakTime -= Time.deltaTime / 2;
            if (sneakTime <= 0)
            {
                sneakTime = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            sneakTime += Time.deltaTime / 2;
            if (sneakTime >= maxSneakTimer)
            {
                sneakTime = maxSneakTimer;
            }
            return false;
        }
    }
}
