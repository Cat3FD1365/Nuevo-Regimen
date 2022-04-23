using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BajarLaVida : MonoBehaviour
{
    BarraVida cantVida;

    [SerializeField] float daño = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        cantVida = FindObjectOfType<BarraVida>();
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
