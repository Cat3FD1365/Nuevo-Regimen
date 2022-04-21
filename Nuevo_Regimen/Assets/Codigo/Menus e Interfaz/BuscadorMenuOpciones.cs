using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuscadorMenuOpciones : MonoBehaviour
{
    public ControlOpciones panelOpciones;
    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<ControlOpciones>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
