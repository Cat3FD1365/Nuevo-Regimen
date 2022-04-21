using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaMenu : MonoBehaviour
{
    public Slider Volumen;
    public float cantVolumen;
    public Image imgMute;

    public Slider Brillo;
    public float cantBrillo;
    public Image panelBrillo;

    public Toggle casillaPantalla;

    // Start is called before the first frame update
    void Start()
    {
        Volumen.value = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        AudioListener.volume = Volumen.value;
        RevisarSiEstoyMute();

        Brillo.value = PlayerPrefs.GetFloat("brillo", 0f);
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, Brillo.value);

        if (Screen.fullScreen)
        {
            casillaPantalla.isOn = true;
        }
        else
        {
            casillaPantalla.isOn = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Funciones para pantalla completa
    public void ActivarPantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }

    //Funciones para el brillo
    public void ChangeBrillo(float valor)
    {
        cantBrillo = valor;
        PlayerPrefs.SetFloat("brillo", cantBrillo);
        panelBrillo.color = new Color(0f, 0f, 0f, Brillo.value);
    }

    //Funciones para el volumen
    public void ChangeVolumen(float valor)
    {
        cantVolumen = valor;
        PlayerPrefs.SetFloat("volumenAudio", cantVolumen);
        AudioListener.volume = Volumen.value;
        RevisarSiEstoyMute();
    }

    public void RevisarSiEstoyMute()
    {
        if (cantVolumen == 0)
        {
            imgMute.enabled = true;
        }
        else
        {
            imgMute.enabled = false;
        }
    }



}
