using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausaJuego : MonoBehaviour
{
    public GameObject menuPausa;
    public GameObject HUD;
    public GameObject menuOpciones;
    private bool pausado;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;
            menuPausa.SetActive(pausado);
            HUD.SetActive(!pausado);
            menuOpciones.SetActive(false);
        }

        if (pausado == true) Pause();
        else Play();

    }

    public void Play()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        pausado = false;
    }
    public void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;
        Time.timeScale = 0f;
        pausado = true;
    }
}
