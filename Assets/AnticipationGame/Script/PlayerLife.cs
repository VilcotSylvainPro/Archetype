using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private float startTime; // Le temps au début du jeu
    private float endTime; // Le temps à la fin du jeu
    private bool isGameRunning = false; // Indique si le jeu est en cours
    public TextMeshProUGUI timerText;

    private int Life = 5;
    public Image imageToBlink; // L'image à faire clignoter
    public Image imageAlert;
    private float decompte = 2f; // Durée du clignotement en secondes
    private bool isBlinking = false; // Indique si l'image est en train de clignoter
    private bool isHitByAsteroid = false;

    [SerializeField] GameObject BoutonFin;
    [SerializeField] GameObject Destruction01;
    [SerializeField] GameObject Destruction02;
    [SerializeField] GameObject Destruction03;
    [SerializeField] GameObject Destruction04;
    [SerializeField] GameObject Destruction05;
    [SerializeField] GameObject AsteroidToucher;

    private CharacterController controller;
    [SerializeField] private ParticleSystem ExplosionDestruction01;
    [SerializeField] private ParticleSystem ExplosionDestruction02;
    [SerializeField] private ParticleSystem ExplosionDestruction03;
    [SerializeField] private ParticleSystem ExplosionDestruction04;
    [SerializeField] private ParticleSystem ExplosionDestruction05;


    [SerializeField] private AudioSource Music01;
    [SerializeField] private AudioSource Music02;
    [SerializeField] private AudioSource Music03;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameRunning)
        {
            
            // Afficher le temps écoulé depuis le début du jeu
            Debug.Log("Temps écoulé: " + (Time.time - startTime));
            float elapsedTime = Time.time - startTime;
            timerText.text = "Temps ecoule: " + elapsedTime.ToString("F2"); // F2 pour afficher seulement deux chiffres après la virgule
        }


        if (isBlinking == true)
        {
            decompte -= Time.deltaTime;
            Debug.Log(decompte);

            if (Mathf.Floor(decompte / 0.4f) % 2 == 0)
            {
                imageToBlink.enabled = true;
                imageAlert.enabled = true;
            }
            else
            {
                imageToBlink.enabled = false;
                imageAlert.enabled = false;
            }

            if (decompte <= 0)
            {
                isBlinking = false;
                imageToBlink.enabled = false;
                imageAlert.enabled = false;
            }
        }


        DestrcutionFusee();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            isHitByAsteroid = true;
            Life = Life - 1;
            Debug.Log(Life);

        }
    }

    //  Data.gameObject.GetComponent<DataScript>().AjouterOr(100);




    public void DestrcutionFusee()
    {
        if (!isBlinking && isHitByAsteroid)
        {
            isBlinking = true;
            decompte = 2f;
            isHitByAsteroid = false;
        }


        if ( Life == 4)
        {
           
            Destruction01.SetActive(false);
            ExplosionDestruction01.Play();

        }
        else if ( Life == 3) 
        {
            
            Destruction02.SetActive(false);
            ExplosionDestruction02.Play();
           
        }
        else if (Life == 2)
        {
            
            Destruction03.SetActive(false);
            ExplosionDestruction03.Play();

        }
        else if (Life == 1)
        {
            
            Destruction04.SetActive(false);
            ExplosionDestruction04.Play();



            if (Music01.isPlaying || Music02.isPlaying)
            {
                Music01.Stop();
                Music02.Stop();
                Music03.Play();
            }


        }
        else if (Life <= 0)
        {

            Destruction05.SetActive(false);
            BoutonFin.SetActive(true);
            EndGame();
            ExplosionDestruction05.Play();
            Destroy(controller);
            ShakeGame2.Instance.ShakeCamera(2f, 2f);
        }
    }


    public void StartGame()
    {
        // Enregistrer le temps de début
        startTime = Time.time;

        // Indiquer que le jeu est en cours
        isGameRunning = true;
    }

    public void EndGame()
    {
        // Enregistrer le temps de fin
        endTime = Time.time;

        // Indiquer que le jeu est terminé
        isGameRunning = false;

        // Afficher le temps total du jeu
        Debug.Log("Temps total du jeu: " + (endTime - startTime));
    }

    public void ChangementScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
