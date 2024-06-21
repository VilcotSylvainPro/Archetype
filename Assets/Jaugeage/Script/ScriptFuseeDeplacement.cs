using Cinemachine;

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ScriptFuseeDeplacement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public float speed = 0.0f;
    [SerializeField] private float maxSpeed = 150.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleration = 10.0f;
    [SerializeField] private float minRotationSpeed = 1.0f;
    [SerializeField] private float maxRotationSpeed = 5.0f;
    //public float rotationAutreSpeed = 20.0f;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject SonFuseeAmbiant;
    [SerializeField] private GameObject SonPropulseur;
    [SerializeField] private GameObject RewindSound;
    [SerializeField] private GameObject RewindSound2;
    [SerializeField] private AudioSource SonValidationCercle;
    [SerializeField] private GameObject circlePrefab;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private GameObject EnergyMotor;
    [SerializeField] private float minDistance = 400f;
    [SerializeField] private float maxDistance = 750f;
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // La caméra Cinemachine
    [SerializeField] private float minAmplitude = 1.0f; 
    [SerializeField] private float maxAmplitude = 4.0f; 
    public float rewindSpeed = 2.0f;
    //private List<Vector3> positions; 

    [SerializeField] private TextMeshProUGUI SpeedText;
    [SerializeField] private TextMeshProUGUI CircleText;
    [SerializeField] private TextMeshProUGUI Temps;



    private List<GameState> gameStates;
    private GameObject[] circles;
    private bool RetourTemps;
    public int CercleToucher = 0;

    //public float startTime;
    private float timer;


    [SerializeField] private ParticleSystem leftThruster;
    [SerializeField] private ParticleSystem rightThruster;
    [SerializeField] private ParticleSystem upThruster;
    [SerializeField] private ParticleSystem downThruster;

    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;


    void Start()
    {
        
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        RetourTemps = false;
        //positions = new List<Vector3>();
        timer = 0.0f;
        gameStates = new List<GameState>();
        
        //startTime = Time.time;
    }





    void Update()
    {
        Debug.Log(RetourTemps);
        circles = GameObject.FindGameObjectsWithTag("Circle");
        //gameObject.transform.Rotate(0.0f, 0.0f, rotationAutreSpeed * Time.deltaTime);
        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        ControleFusee();
        VitesseFusee();

        RetourArriere();
        SonFuseeDeplacement();
        UiManagement();
        TimerComptage();

    }


    void OnTriggerEnter(Collider collision)
    {
        
        if (collision.gameObject.CompareTag("Circle") && RetourTemps == false)
        {

            Instantiate(Explosion, collision.transform.position, collision.transform.rotation);


            
            Destroy(collision.gameObject);
            CercleToucher = CercleToucher + 1;
            SonValidationCercle.Play();


            Vector3 randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0; 
            float randomDistance = Random.Range(minDistance, maxDistance);
            Vector3 randomPosition = transform.position + randomDirection.normalized * randomDistance;

            
            Instantiate(circlePrefab, randomPosition, Quaternion.Euler(-90f,0f,0f));
        }
    }


    public void ControleFusee()
    {
        
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.y - transform.position.y));

        
        Vector3 direction = (mousePos - transform.position).normalized;

        
        float rotationSpeed = Mathf.Lerp(maxRotationSpeed, minRotationSpeed, speed / maxSpeed);

        if (virtualCameraNoise != null)
        {
            virtualCameraNoise.m_AmplitudeGain = Mathf.Lerp(minAmplitude, maxAmplitude, speed / maxSpeed);
        }

        Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);


        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.Rotate(new Vector3(-mouseY, mouseX, 0) * rotationSpeed);
        //Debug.Log(rotationSpeed);


        if (mouseX > 0)
        {
            leftThruster.Play();
            rightThruster.Stop();
        }
        else if (mouseX < 0)
        {
            rightThruster.Play();
            leftThruster.Stop();
        }

        if (mouseY > 0)
        {
            downThruster.Play();
            upThruster.Stop();
        }
        else if (mouseY < 0)
        {
            upThruster.Play();
            downThruster.Stop();
        }

    }


    public void VitesseFusee()
    {
        
        if (Input.GetMouseButton(0))
        {
            EnergyMotor.SetActive(true);
            SonPropulseur.SetActive(true);


            if (speed < maxSpeed)
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        
        else
        {
            EnergyMotor.SetActive(false);
            SonPropulseur.SetActive(false);

            if (speed > 0)
            {
                speed -= deceleration * Time.deltaTime;
            }
        }

        
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void RetourArriere()
    {


        if (Input.GetMouseButton(1))
        {
            RetourTemps = true;
            if (gameStates.Count > 0)
            {
                RewindSound.SetActive(true);
                RewindSound2.SetActive(true);
                GameState lastState = gameStates[gameStates.Count - 1];
                transform.position = lastState.rocketPosition;
                transform.rotation = lastState.rocketRotation;
                speed = lastState.rocketSpeed;
                timer = lastState.time;
                CercleToucher = lastState.CirclePastRegistered;
                for (int i = 0; i < lastState.circleStates.Count; i++)
                {
                    CircleState circleState = lastState.circleStates[i];
                    GameObject circle = circles[i];
                    circle.transform.position = circleState.position;
                    circle.SetActive(circleState.isActive);

                }
                gameStates.RemoveAt(gameStates.Count - 1);
            }
        }
        else
        {
            
            gameStates.Add(new GameState(this, new List<GameObject>(circles), timer));
            RewindSound.SetActive(false);
            RewindSound2.SetActive(false);
            RetourTemps = false;
        }
    }

    public void SonFuseeDeplacement()
    {
        if (speed > 0 && RetourTemps == false)
        {
            SonFuseeAmbiant.SetActive(true);
        }
        else if (speed < 0 || RetourTemps == true)
        {
            SonFuseeAmbiant.SetActive(false);
            
        }
    }


    public void UiManagement()
    {
        SpeedText.GetComponent<TMP_Text>().text = "Vitesse : " + Mathf.RoundToInt(speed).ToString();
        CircleText.GetComponent<TMP_Text>().text = "Cercle : " + CercleToucher.ToString();

    }




    public void TimerComptage()
    {


        timer += Time.deltaTime;



        Temps.GetComponent<TMP_Text>().text = "Temps : " + Mathf.RoundToInt(timer).ToString();

        
    }




}







[System.Serializable]
public class CircleState
{
    public Vector3 position;
    public bool isActive;

    public CircleState(Vector3 position, bool isActive)
    {
        this.position = position;
        this.isActive = isActive;
    }
}


[System.Serializable]
public class GameState
{
    public Vector3 rocketPosition;
    public Quaternion rocketRotation;
    public int CirclePastRegistered;
    public float time;
    public float rocketSpeed;
    public List<CircleState> circleStates;

    public GameState(ScriptFuseeDeplacement rocket, List<GameObject> circles, float time)
    {
        rocketPosition = rocket.transform.position;
        rocketRotation = rocket.transform.rotation;
        CirclePastRegistered = rocket.CercleToucher;
        rocketSpeed = rocket.speed;
        this.time = time;
        circleStates = new List<CircleState>();
        foreach (GameObject circle in circles)
        {
            circleStates.Add(new CircleState(circle.transform.position, circle.activeSelf));
        }
    }
}



