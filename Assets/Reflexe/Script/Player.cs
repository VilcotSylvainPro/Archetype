using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class Player : MonoBehaviour
{


    private Vector3 startPosition;

    public static Player Instance { get; private set; }

    [SerializeField] private GameObject CarburantVisuel;
    [SerializeField] private GameObject Particule01;
    [SerializeField] private GameObject Particule02;
    [SerializeField] private GameObject Particule03;
    [SerializeField] private GameObject Particule04;
    [SerializeField] private GameObject Particule05;
    [SerializeField] private GameObject Particule06;

    [SerializeField] private GameObject Bon;
    [SerializeField] private GameObject Faux;

    [SerializeField] private GameObject BonSoleil;
    [SerializeField] private GameObject FauxSoleil;

    [SerializeField] private GameObject MoteurOK;
    [SerializeField] private GameObject MoteurAlert;
    [SerializeField] private GameObject MoteurBad;
    private bool isEngineQTEActive = false;

    [SerializeField] private ParticleSystem Particule01Speed;
    [SerializeField] private ParticleSystem Particule02Speed;
    [SerializeField] private ParticleSystem Particule03Speed;
    [SerializeField] private ParticleSystem Particule04Speed;
    [SerializeField] private ParticleSystem Particule05Speed;
    [SerializeField] private ParticleSystem Particule06Speed;


    [SerializeField] private ParticleSystem ExplosionBlanche;
    [SerializeField] private ParticleSystem ExplosionJaune;
    [SerializeField] private ParticleSystem ExplosionRouge;


    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private float asteroidSpawnRate = 1.0f;
    [SerializeField] private float asteroidSpawnDistance = 10.0f;


    [SerializeField] private GameObject Sun01Prefab;
    [SerializeField] private GameObject Sun02Prefab;
    [SerializeField] private GameObject Sun03Prefab;
    [SerializeField] private float SunSpawnRate = 20.0f;
    [SerializeField] private float SunSpawnDIstance = 10.0f;


    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject GameOverButton;

    private int Timer;


    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI Distance;
    [SerializeField] private TextMeshProUGUI Temps;
    [SerializeField] private TextMeshProUGUI ToucheQTE;
    [SerializeField] private GameObject AffichageQTE;
    [SerializeField] private GameObject SonSpaceTravel;

    [SerializeField] private AudioSource SonPlayerQTE;
    [SerializeField] private AudioSource SonPlayerSun;
    [SerializeField] private AudioSource SonSunGood;
    [SerializeField] private AudioSource SonPlayerBoost;
    [SerializeField] private AudioSource SonMoteurWarning;
    [SerializeField] private AudioSource SonMoteurBad;
    [SerializeField] private AudioSource SonMoteurGood;
    private float startTime;






    [SerializeField] float speed = 0.0f;
    private float timeToNextQTE = 3.0f;
    private bool isQTEActive = false; 
    private Key qteKey;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }



    private void Start()
    {
        Bon.SetActive(false);
        Faux.SetActive(false);
        FauxSoleil.SetActive(false);
        BonSoleil.SetActive(false);
        StartCoroutine(QTECoroutine());
        StartCoroutine(AsteroidCoroutine());
        StartCoroutine(SunCoutoutine());
        StartCoroutine(EngineQTECoroutine());
        MoteurOK.SetActive(true);
        MoteurAlert.SetActive(false);
        MoteurBad.SetActive(false);
        Time.timeScale = 1;

        Timer = 0;
        startTime = Time.time;

        startPosition = transform.position;
    }

    private void Update()
    {
        FlameVisuel();
        TimerComptage();
        ParticulesVisuel();
        particuleVitesse();
        MiseEnTexteInformation();
        CliquerSoleil();
        MusicHyperSpace();


        float distanceTravelled = Vector3.Distance(startPosition, transform.position);
        Distance.GetComponent<TMP_Text>().text = "Distance: " + distanceTravelled.ToString("F1") + " m";
        SpeedText.GetComponent<TMP_Text>().text = "Vitesse : " + speed.ToString();



        RenderSettings.skybox.SetFloat("_Rotation", Time.time * 0.5f);

        gameObject.transform.Rotate(0.0f, 0.0f, 3.0f * Time.deltaTime); ;


        if (speed < 0)
        {
            
            StopAllCoroutines();


            GameOver.SetActive(true);
            GameOverButton.SetActive(true);



            Time.timeScale = 0;
        }



        if (isQTEActive && Keyboard.current[qteKey].wasPressedThisFrame)
        {
            speed += 5.0f;
            SonPlayerBoost.Play();
            isQTEActive = false;
            Bon.SetActive(true);

            Debug.Log("BonneTouche");
        }


        if (isEngineQTEActive && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            
            isEngineQTEActive = false;
            MoteurAlert.SetActive(false);
            MoteurOK.SetActive(true);
            SonMoteurGood.Play();
            StartCoroutine(HideImageAfterDelay(MoteurOK, 2.0f));

        }



    }

    private void FixedUpdate()
    {
        transform.position = transform.position + new Vector3(0, 0, 1) * speed * Time.deltaTime;
    }

    private IEnumerator QTECoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeToNextQTE);
            isQTEActive = true;
            Bon.SetActive(false);
            Faux.SetActive(false);
            qteKey = GetRandomKey();
            Debug.Log("Appuyer sur " + qteKey);

            yield return new WaitForSeconds(2.0f);

            if (isQTEActive)
            {
                speed -= 5.0f;
                isQTEActive = false;
                Faux.SetActive(true);
                SonPlayerQTE.Play();

                Debug.Log("MauvaiseTouche");
            }
        }
    }

    private IEnumerator AsteroidCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(asteroidSpawnRate);

            float xPos = Random.Range(-asteroidSpawnDistance, asteroidSpawnDistance);
            if (xPos > -30.0f && xPos < 30.0f)
            {
                xPos += 30.0f * Mathf.Sign(xPos);
            }

            Vector3 spawnPosition = transform.position + new Vector3(xPos, Random.Range(-asteroidSpawnDistance, asteroidSpawnDistance), Random.Range(2 * asteroidSpawnDistance, 7 * asteroidSpawnDistance));
            Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity);
        }
    }


    private IEnumerator SunCoutoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(SunSpawnRate);

            float xPos = Random.Range(-SunSpawnDIstance, SunSpawnDIstance);
            if (xPos > -30.0f && xPos < 30.0f)
            {
                xPos += 30.0f * Mathf.Sign(xPos);
            }

            Vector3 spawnPosition = transform.position + new Vector3(xPos, Random.Range(-SunSpawnDIstance, SunSpawnDIstance), 2 * SunSpawnDIstance);

            int spawnSunIndex = 0;

            spawnSunIndex = Random.Range(0, 3);

            if(spawnSunIndex == 0)
            {
                Instantiate(Sun01Prefab, spawnPosition, Quaternion.identity);
            }
            else if (spawnSunIndex == 1) 
            {
                Instantiate(Sun02Prefab, spawnPosition, Quaternion.identity);
            }
            else if (spawnSunIndex == 2) 
            {
                Instantiate(Sun03Prefab, spawnPosition, Quaternion.identity);
            }
            
        }
    }

    private IEnumerator SoleilRater()
    {
        FauxSoleil.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        FauxSoleil.SetActive(false);
        SonPlayerSun.Play();
    }

    private IEnumerator SoleilToucher()
    {
        BonSoleil.SetActive(true);
        SonSunGood.Play();
        yield return new WaitForSeconds(2.0f);
        BonSoleil.SetActive(false);
    }


    private IEnumerator HideImageAfterDelay(GameObject image, float delay)
    {
        yield return new WaitForSeconds(delay);
        image.SetActive(false);
    }


    private IEnumerator EngineQTECoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));

            
            isEngineQTEActive = true;
            SonMoteurWarning.Play();
            MoteurAlert.SetActive(true);
            MoteurBad.SetActive(false);
            MoteurOK.SetActive(false);
            yield return new WaitForSeconds(2.0f);

            if (isEngineQTEActive)
            {
                DecreaseSpeedMoteur(100.0f);
                SonMoteurBad.Play();
                MoteurBad.SetActive(true);
                MoteurAlert.SetActive(false);
                isEngineQTEActive = false;
                StartCoroutine(HideImageAfterDelay(MoteurBad, 2.0f));
            }
        }
    }





    private Key GetRandomKey()
    {

        List<Key> keys = new List<Key>()
        {
            Key.A, Key.B, Key.C, Key.D, Key.E,
            Key.F, Key.G, Key.H, Key.I, Key.J,
            Key.K, Key.L, Key.M, Key.N, Key.O,
            Key.P, Key.Q, Key.R, Key.S, Key.T,
            Key.U, Key.V, Key.W, Key.X, Key.Y, Key.Z
        };


        int randomIndex = Random.Range(0, keys.Count);
        return keys[randomIndex];
    }



    public void FlameVisuel()
    {
        if (speed <= 0)
        {
            CarburantVisuel.SetActive(false);

        }
        else if (speed > 0)
        {
            CarburantVisuel.SetActive(true);

        }
    }


    public void particuleVitesse()
    {
        Particule01Speed.startSpeed = speed + 30;
        Particule02Speed.startSpeed = speed + 15;
        Particule03Speed.startSpeed = speed;
        Particule04Speed.startSpeed = speed - 15;
        Particule05Speed.startSpeed = speed - 30;
        Particule06Speed.startSpeed = speed - 45;
    }

    public void ParticulesVisuel()
    {
        if (speed <= 0)
        {

            Particule01.SetActive(false);
            Particule02.SetActive(false);
            Particule03.SetActive(false);
            Particule04.SetActive(false);
            Particule05.SetActive(false);
            Particule06.SetActive(false);
        }
        if (speed >= 10 && speed < 15)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(false);
            Particule03.SetActive(false);
            Particule04.SetActive(false);
            Particule05.SetActive(false);
            Particule06.SetActive(false);


        }
        if (speed >= 15 && speed < 20)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(true);
            Particule03.SetActive(false);
            Particule04.SetActive(false);
            Particule05.SetActive(false);
            Particule06.SetActive(false);

        }
        if (speed >= 20 && speed < 50)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(true);
            Particule03.SetActive(true);
            Particule04.SetActive(false);
            Particule05.SetActive(false);
            Particule06.SetActive(false);

        }

        if (speed >= 50 && speed < 100)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(true);
            Particule03.SetActive(true);
            Particule04.SetActive(true);
            Particule05.SetActive(false);
            Particule06.SetActive(false);

        }

        if (speed >= 100 && speed < 150)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(true);
            Particule03.SetActive(true);
            Particule04.SetActive(true);
            Particule05.SetActive(true);
            Particule06.SetActive(false);

        }

        if (speed >= 150)
        {
            Particule01.SetActive(true);
            Particule02.SetActive(true);
            Particule03.SetActive(true);
            Particule04.SetActive(true);
            Particule05.SetActive(true);
            Particule06.SetActive(true);

        }
    }




    public void MiseEnTexteInformation()
    {
        SpeedText.GetComponent<TMP_Text>().text = "Vitesse : " + speed.ToString();



        if (isQTEActive == true)
        {
            AffichageQTE.SetActive(true);
            ToucheQTE.GetComponent<TMP_Text>().text = "Touche à appuyer : " + qteKey;
        }
        else if (isQTEActive == false) 
        {
            AffichageQTE.SetActive(false);

        }

    }




    public void CliquerSoleil()
    {
       


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Sun")))
        {
            
            if (hit.transform.CompareTag("Sun") && hit.transform.name == "RedSun(Clone)")
            {
                Instantiate(ExplosionRouge, hit.point, Quaternion.identity);
                Destroy(hit.transform.gameObject);
                speed = speed + 5.0f;
                StartCoroutine(SoleilToucher());
            }

            if (hit.transform.CompareTag("Sun") && hit.transform.name == "YellowSun(Clone)")
            {
                Instantiate(ExplosionJaune, hit.point, Quaternion.identity);

                Destroy(hit.transform.gameObject);
                speed = speed + 10.0f;
                StartCoroutine(SoleilToucher());
            }

            if (hit.transform.CompareTag("Sun") && hit.transform.name == "BlueWhiteSun(Clone)")
            {
                Instantiate(ExplosionBlanche, hit.point, Quaternion.identity);

                Destroy(hit.transform.gameObject);
                speed = speed + 15.0f;
                StartCoroutine(SoleilToucher());
            }
        }
    }

    public void TimerComptage()
    {
        Timer = Mathf.FloorToInt(Time.time - startTime);



        Temps.GetComponent<TMP_Text>().text = "Temps : " + Timer.ToString();


    }

    public void DecreaseSpeed(float amount)
    {
        speed -= amount;
        StartCoroutine(SoleilRater());

    }

    public void DecreaseSpeedMoteur(float amount)
    {
        speed -= amount;
        

    }


    public void MusicHyperSpace()
    {
        if (speed > 0) 
        {
            SonSpaceTravel.SetActive(true);
          
        }
        if (speed <= 0)
        {
            SonSpaceTravel.SetActive(false);
          
        }
    }






}
