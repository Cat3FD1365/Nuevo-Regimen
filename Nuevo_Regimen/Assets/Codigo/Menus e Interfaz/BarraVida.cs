using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public int vidaMax;
    public float vidaActual;
    public Image rellenoVida;
    public Image moriste;
    public GameObject interfaz;
    public GameObject Jugador;

    // Start is called before the first frame update
    void Start()
    {
        vidaActual = vidaMax;
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();

        perdiste();
    }

    public void perdiste()
    {
        //Si la vida llega a 0 se acaba el juego
        if (vidaActual <= 0)
        {
            Jugador.SetActive(false);
            interfaz.SetActive(false);
            moriste.enabled = true;
        }
    }

    public void RevisarVida()
    {
        //Actualiza la barra de vida
        rellenoVida.fillAmount = vidaActual / vidaMax;
    }
}
