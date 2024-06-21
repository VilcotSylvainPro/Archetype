using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RetourMenuPrincipale : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RetourMenu();
    }



    public void RetourMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Charger la scène "MenuPrincipale"
            SceneManager.LoadScene("MenuPrincipale");
        }
    }
}
