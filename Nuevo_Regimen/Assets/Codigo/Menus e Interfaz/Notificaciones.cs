using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notificaciones : MonoBehaviour
{
    //Para logica del texto
    [SerializeField] Text mensaje;
    [SerializeField] string[] consejos;
    private float opa = 0;
    private bool alTope = true;

    //Para logica de correr
    private int corrio;
    private int corr = 1;
    private int random;

    //Para logica cercania a camaras y enemigos
    private bool verGuardia;
    private bool verCamara;


    //Sobre logica cambio de opacidad
    private void subiendo()
    {
        if (alTope == false && opa < 1)
        {
            Debug.Log("Subiendo");
            opa = opa + 0.06f * Time.deltaTime * 5;
        }
    }

    private void bajando()
    {
        if (alTope == true && opa > 0)
        {
            Debug.Log("Bajando");
            opa = opa - 0.07f * Time.deltaTime * 5;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(mensajesPredeterminados());
    }

    // Update is called once per frame
    void Update()
    {
        //Actualizacion del color
        mensaje.color = new Color(mensaje.color.r, mensaje.color.g, mensaje.color.b, opa * 1f);
        subiendo();
        bajando();

        //Primer vez en correr y aleatoriedad cuando se corre
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            corrio = corr;
            random = UnityEngine.Random.Range(0, 10);
            if (corrio == 1 || random == corr)
            {
                corr = 2;
                StartCoroutine(alCorrer());
            }
        }

        //Primer encuentro con guardia y camara
        if (verGuardia == true) StartCoroutine(encuentroGuardia());
        if (verCamara == true) StartCoroutine(encuentroCamara());
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemigo")) verGuardia = true;

        if (other.CompareTag("Camara")) verCamara = true;
            
    }


    IEnumerator mensajesPredeterminados()
    {
        yield return new WaitForSeconds(5);
        mensaje.text = consejos[0];
        alTope = false;
        Debug.Log("Espera");
        yield return new WaitForSeconds(7);
        Debug.Log("Esperado");
        alTope = true;

        yield return new WaitForSeconds(14);
        mensaje.text = consejos[1];
        alTope = false;
        Debug.Log("Espera");
        yield return new WaitForSeconds(7);
        Debug.Log("Esperado");
        alTope = true;

    }

    IEnumerator alCorrer()
    {
        mensaje.text = consejos[2];
        alTope = false;
        yield return new WaitForSeconds(7);
        alTope = true;
    }

    IEnumerator encuentroGuardia()
    {
        mensaje.text = consejos[4];
        alTope = false;
        yield return new WaitForSeconds(7);
        alTope = true;
    }

    IEnumerator encuentroCamara()
    {
        mensaje.text = consejos[3];
        alTope = false;
        yield return new WaitForSeconds(7);
        alTope = true;
    }
}
