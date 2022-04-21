using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Protector : MonoBehaviour
{
    public GameObject ProtectorInicio;
    public GameObject leMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PressEnter();
    }

    private void PressEnter()
    {
        if (Input.GetKeyDown("return") || Input.GetKeyDown("space"))
        {
            leMenu.gameObject.SetActive(true);
            ProtectorInicio.gameObject.SetActive(false);
        }
    }
}
