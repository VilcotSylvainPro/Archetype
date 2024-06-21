using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    [SerializeField] AudioSource M_Son;
    [SerializeField] AudioClip ClickSound;
    [SerializeField] AudioClip HoverSound;
    [SerializeField] private float RotationSpeed;
    [SerializeField] private GameObject Jeu;
    [SerializeField] private GameObject StartButton;






    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationSpeed);
    }


    public void ClickSoundCick()
    {
        M_Son.PlayOneShot(ClickSound);
    }
    public void HoverSoundCick()
    {
        M_Son.PlayOneShot(HoverSound);
    }

    public void Jouer()
    {
        Jeu.SetActive(true);
        StartButton.SetActive(false);
    }

    public void Anticipation()
    {
        SceneManager.LoadScene("Anticipation");
    }


    public void Reflexe()
    {
        SceneManager.LoadScene("Reflexe");
    }

    public void Precision()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Jaugeage()
    {
        SceneManager.LoadScene("Jaugeage");
    }






    public void Quitter()
    {
        Application.Quit();
    }
}
