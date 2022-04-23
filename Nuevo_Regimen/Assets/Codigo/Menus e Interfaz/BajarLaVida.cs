using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarLaVida : MonoBehaviour
{
    public BarraVida cantVida;

    public float daño = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) cantVida.vidaActual = cantVida.vidaActual - daño;
    }
}
