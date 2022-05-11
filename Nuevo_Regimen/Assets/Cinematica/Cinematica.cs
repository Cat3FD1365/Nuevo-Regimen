using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cinematica : MonoBehaviour
{
    public void Scene()
    {
        StartCoroutine(Load());

        IEnumerator Load()
        {
            yield return new WaitForSeconds(131.0f);
            SceneManager.LoadScene("Laberinto_Stefy");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Scene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
