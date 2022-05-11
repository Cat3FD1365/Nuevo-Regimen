using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notificaciones : MonoBehaviour
{
    [SerializeField] Text mensaje;
    [SerializeField] string[] consejos;
    public float opa = 0;
    private bool alTope = true;

    // Start is called before the first frame update
    void Start()
    {
        mensajesPredeterminados();
    }

    // Update is called once per frame
    void Update()
    {
        mensaje.color = new Color(mensaje.color.r, mensaje.color.g, mensaje.color.b, opa * 1f);
        subiendo();
        bajando();
    }


    private void mensajesPredeterminados()
    {
        //Primero hay una espera luego se pone el mensaje y el aparecer y desaparecer

        //Primer mensaje
        mensaje.text = consejos[0];
        StartCoroutine(aparicion());

    }

    private void subiendo()
    {
        if (alTope == false && opa <1)
        {
            Debug.Log("Subiendo");
            opa = opa + 0.05f * Time.deltaTime * 5;
        }
    }

    private void bajando()
    {
        if (alTope == true && opa > 0)
        {
            Debug.Log("Bajando");
            opa = opa - 0.05f * Time.deltaTime * 5;
        }
    }

    IEnumerator aparicion()
    {
        //Empezara a subir
        yield return new WaitForSeconds(2);
        alTope = false;

        Debug.Log("Espera");
        yield return new WaitForSeconds(8);
        Debug.Log("Esperado");
        alTope = true;
        
    }

    IEnumerator tiempoEspera(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
    }



}
