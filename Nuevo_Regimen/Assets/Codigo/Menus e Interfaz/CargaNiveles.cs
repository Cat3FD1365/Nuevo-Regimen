using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CargaNiveles : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void CargaDeNivel (string Nivel)
    {
        SceneManager.LoadScene(Nivel);
    }

    public void QuitarJuego()
    {
        Debug.Log("Salido");
        Application.Quit();
    }
}
