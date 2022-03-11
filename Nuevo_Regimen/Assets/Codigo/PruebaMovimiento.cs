using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaMovimiento : MonoBehaviour
{
    public float SpeedMove = 0f;
    private Animator anim;
    

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("e"))
        {
            anim.SetBool("Run", true);
        }

        if (!Input.GetKey("e"))
        {
            anim.SetBool("Run", false);
        }

        if (Input.GetKey("p"))
        {
            anim.SetBool("Attack", true);
        }
        if (!Input.GetKey("p"))
        {
            anim.SetBool("Attack", false);
        }

        anim.SetFloat("Speed", SpeedMove);
    }
}
