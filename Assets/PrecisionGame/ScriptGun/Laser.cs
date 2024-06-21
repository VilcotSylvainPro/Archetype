using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    [SerializeField] private LineRenderer _beam;
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private Transform _Pistol;
    [SerializeField] private float  _MaxLength;
    [SerializeField] private TextMeshProUGUI _Score;

    [SerializeField] private GameObject EnsembleCible1;

    [SerializeField] private AudioSource TirerLaser;
    [SerializeField] private AudioSource LaserCibleTir;
    [SerializeField] private bool TirLaserBool;

    [SerializeField] private ParticleSystem ParticuleCanon;
    [SerializeField] private ParticleSystem ParticuleImpact;
    [SerializeField] private GameObject ParticulesExplosion;

    private int nbBleu = 0;
    private int nbRouge = 0;
    private int nbJaune = 0;
    private int nbOrange = 0;


    [SerializeField] private GameObject Cible1;
    [SerializeField] private GameObject Cible1Rouge;
    [SerializeField] private GameObject Cible1Jaune;
    [SerializeField] private GameObject Cible1Orange;
    [SerializeField] private GameObject Cible1Bleu;

    [SerializeField] private GameObject ReparationCible1;

    [SerializeField] private GameObject Cible2;
    [SerializeField] private GameObject ReparationCible2;

    [SerializeField] private GameObject Cible3;
    [SerializeField] private GameObject ReparationCible3;

    [SerializeField] private GameObject Cible4;
    [SerializeField] private GameObject ReparationCible4;

    [SerializeField] private GameObject Cible5;
    [SerializeField] private GameObject ReparationCible5;

    [SerializeField] private GameObject Cible6;
    [SerializeField] private GameObject ReparationCible6;

    [SerializeField] private GameObject Cible7;
    [SerializeField] private GameObject ReparationCible7;

    [SerializeField] private GameObject Cible8;
    [SerializeField] private GameObject ReparationCible8;

    [SerializeField] private GameObject Cible9;
    [SerializeField] private GameObject ReparationCible9;

    [SerializeField] private GameObject Cible10;
    [SerializeField] private GameObject ReparationCible10;

    [SerializeField] private GameObject Cible11;
    [SerializeField] private GameObject ReparationCible11;

    [SerializeField] private GameObject Cible12;
    [SerializeField] private GameObject ReparationCible12;

    [SerializeField] private GameObject Cible13;
    [SerializeField] private GameObject ReparationCible13;

    [SerializeField] private GameObject Cible14;
    [SerializeField] private GameObject ReparationCible14;

    [SerializeField] private GameObject Cible15;
    [SerializeField] private GameObject ReparationCible15;

    [SerializeField] private GameObject Cible16;
    [SerializeField] private GameObject ReparationCible16;

    [SerializeField] private GameObject Cible17;
    [SerializeField] private GameObject ReparationCible17;

    [SerializeField] private GameObject Cible18;
    [SerializeField] private GameObject ReparationCible18;

    [SerializeField] private GameObject Cible19;


    private int Score = 0;
 




    public Vector3 screenPosition;



    private void Awake()
    {
        TirerLaser.gameObject.SetActive(false);
        TirLaserBool = false;
        _beam.enabled = false;
        Cursor.visible = false;
    }
    private void Activate()
    {
        _beam.enabled = true;
        ParticuleImpact.Play();
        ParticuleCanon.Play();
    }
    private void Desactivate()
    {
        _beam.enabled = false;
        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, _muzzlePoint.position);
        ParticuleImpact.Stop();
        ParticuleCanon.Stop();
    }

    
    private void Update()
    {
        _Score.text = Score.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            Activate();
            TirerLaser.gameObject.SetActive(true);
            TirLaserBool = true;
            PlayMusic(TirLaserBool);
            Shake.Instance.ShakeCamera(2f, 2f);
        }
            
        else if (Input.GetMouseButtonUp(0))
        {
            Desactivate();
            TirerLaser.gameObject.SetActive(false);
            Shake.Instance.ResetShakeCamera();
        }
           


      

        
    }


    private void FixedUpdate()
    {
        screenPosition = Input.mousePosition;

        Ray Mouseray = Camera.main.ScreenPointToRay(screenPosition);
        Physics.Raycast(Mouseray, out RaycastHit hit2);
        _Pistol.transform.LookAt(hit2.point);

        if (!_beam.enabled) return;



        Ray ray = new Ray(_muzzlePoint.position, _muzzlePoint.forward);
        bool cast = Physics.Raycast(ray, out RaycastHit hit, _MaxLength);
        Vector3 hitPosition = cast ? hit.point : _muzzlePoint.position + _muzzlePoint.forward * _MaxLength;
        ParticuleImpact.transform.position = hitPosition;
        {


            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible1)
            {
                if (hit.transform.gameObject ==  Cible1Rouge)
                {
                    nbRouge = nbRouge + 1;
                    Debug.Log("Nombre Cible rouge : " + nbRouge);
                }
                if (hit.transform.gameObject == Cible1Jaune)
                {
                    nbJaune = nbJaune + 1;
                    Debug.Log("Nombre Cible jaune : " + nbJaune);
                }
                if (hit.transform.gameObject == Cible1Orange)
                {
                    nbOrange = nbOrange + 1;
                    Debug.Log("Nombre Cible Orange : " + nbOrange);
                }
                if (hit.transform.gameObject == Cible1Bleu)
                {
                    nbBleu = nbBleu + 1;
                    Debug.Log("Nombre Cible bleu : " + nbBleu);
                }
                hit.transform.gameObject.SetActive(false);
                ReparationCible1.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y , hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible2)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible2.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible3)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible3.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible4)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible4.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible5)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible5.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible6)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible6.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }


            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible7)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible7.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }


            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible8)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible8.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }


            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible9)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible9.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible10)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible10.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible11)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible11.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible12)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible12.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible13)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible13.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible14)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible14.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }
            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible15)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible15.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }
            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible16)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible16.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }
            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible17)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible17.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }
            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible18)
            {
                hit.transform.gameObject.SetActive(false);
                ReparationCible18.SetActive(true);
                SonLaserCible();
                Debug.Log(hit.transform.gameObject + " C'est une cible");

                GameObject fx = Instantiate(ParticulesExplosion, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), Quaternion.identity);
                Score = Score + 100;
                Destroy(fx, 4f);


            }

            if (hit.transform.gameObject.CompareTag("Cible") && hit.transform.gameObject == Cible19)
            {
                SceneManager.LoadScene("SampleScene");

            }
            //Debug.Log(hit.transform.gameObject);

        }

        

        _beam.SetPosition(0, _muzzlePoint.position);
        _beam.SetPosition(1, hitPosition);
    }

    public void PlayMusic(bool vrai)
    {
        if (vrai == true)
        {
            TirerLaser.Play();
            TirLaserBool = false;
        }
    }

    public void SonLaserCible()
    {
        LaserCibleTir.Play();
    }

}
