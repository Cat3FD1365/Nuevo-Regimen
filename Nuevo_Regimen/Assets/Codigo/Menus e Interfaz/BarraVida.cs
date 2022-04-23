using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BarraVida : MonoBehaviour
{
    public int vidaMax = 100;
    public float vidaActual;
    public Image rellenoVida;


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
            SceneManager.LoadScene("gameOver");
        }
    }

    public void RevisarVida()
    {
        //Actualiza la barra de vida
        rellenoVida.fillAmount = vidaActual / vidaMax;
    }
}
