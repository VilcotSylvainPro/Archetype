using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraTrolly : MonoBehaviour
{

    [SerializeField] private GameObject Fin;
    [SerializeField] private AudioSource Reparation;

    [SerializeField] private CinemachineSmoothPath DollyTrack1;
    [SerializeField] private CinemachineDollyCart DollyCart1;


    [SerializeField] private CinemachineSmoothPath DollyTrack2;
    [SerializeField] private CinemachineDollyCart DollyCart2;
    [SerializeField] private GameObject Cible1;
    [SerializeField] private GameObject Cible2;
    [SerializeField] private GameObject Cible3;
    private bool Cible1Activer = false;
    [SerializeField] private Transform DollyTrack2Transform;

    [SerializeField] private CinemachineSmoothPath DollyTrack3;
    [SerializeField] private CinemachineDollyCart DollyCart3;
    [SerializeField] private GameObject Cible4;
    [SerializeField] private GameObject Cible5;
    [SerializeField] private GameObject Cible6;
    private bool Cible2Activer = false;
    [SerializeField] private Transform DollyTrack3Transform;





    [SerializeField] private CinemachineSmoothPath DollyTrack4;
    [SerializeField] private CinemachineDollyCart DollyCart4;
    [SerializeField] private GameObject Cible7;
    [SerializeField] private GameObject Cible8;
    [SerializeField] private GameObject Cible9;
    private bool Cible3Activer = false;
    [SerializeField] private Transform DollyTrack4Transform;




    [SerializeField] private CinemachineSmoothPath DollyTrack5;
    [SerializeField] private CinemachineDollyCart DollyCart5;
    [SerializeField] private GameObject Cible10;
    [SerializeField] private GameObject Cible11;
    [SerializeField] private GameObject Cible12;
    private bool Cible4Activer = false;
    [SerializeField] private Transform DollyTrack5Transform;




    [SerializeField] private CinemachineSmoothPath DollyTrack6;
    [SerializeField] private CinemachineDollyCart DollyCart6;
    [SerializeField] private GameObject Cible13;
    [SerializeField] private GameObject Cible14;
    [SerializeField] private GameObject Cible15;
    private bool Cible5Activer = false;
    [SerializeField] private Transform DollyTrack6Transform;



    [SerializeField] private CinemachineSmoothPath DollyTrack7;
    [SerializeField] private CinemachineDollyCart DollyCart7;
    [SerializeField] private GameObject Cible16;
    [SerializeField] private GameObject Cible17;
    [SerializeField] private GameObject Cible18;
    [SerializeField] private GameObject Cible19;
    private bool Cible6Activer = false;
    [SerializeField] private Transform DollyTrack7Transform;


    private bool Actif1 = false;
    private bool Actif2 = false;
    private bool Actif3 = false;
    private bool Actif4 = false;
    private bool Actif5 = false;
    private bool Actif6 = false;
    


    private bool Actif1Vali = false;
    private bool Actif2Vali = false;
    private bool Actif3Vali = false;
    private bool Actif4Vali = false;
    private bool Actif5Vali = false;
    private bool Actif6Vali = false;
   


    [SerializeField] private CinemachineVirtualCamera VirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        
        Cible1.SetActive(false); Cible2.SetActive(false); Cible3.SetActive(false); Cible4.SetActive(false); Cible5.SetActive(false); Cible6.SetActive(false); Cible7.SetActive(false); Cible8.SetActive(false);
        Cible9.SetActive(false); Cible10.SetActive(false); Cible11.SetActive(false); Cible12.SetActive(false); Cible13.SetActive(false); Cible14.SetActive(false); Cible15.SetActive(false); Cible16.SetActive(false);  
        Cible17.SetActive(false); Cible18.SetActive(false); Cible19.SetActive(false);
    }

    //Organiser pour que la caméra navigue entre les carts après que toute les cibles soit touché 


    // Update is called once per frame
    void Update()
    {
        if(DollyCart1.m_Position == DollyTrack1.PathLength && Cible1Activer == false) 
        {
            Cible1Activer = true;
            Cible1.SetActive(true); Cible2.SetActive(true); Cible3.SetActive(true);

        }

        if( !Cible1.activeSelf && !Cible2.activeSelf && !Cible3.activeSelf && DollyCart1.m_Position == DollyTrack1.PathLength)
        {
            Debug.Log("Chnagement vers dolly2");
            /*Destroy(DollyCart1);
            Destroy(DollyTrack1);*/

            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack2Transform;

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack2;
            //VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            


            DollyCart2.m_Speed = 10;
            

        }


        if (!Cible1.activeSelf && !Cible2.activeSelf && !Cible3.activeSelf && Actif1Vali == false && Cible1Activer == true) 
        {

            Actif1 = true;

            SonReparation(Actif1, Actif1Vali);
            Actif1Vali = true;
        }




        if (!Cible1.activeSelf && !Cible2.activeSelf && !Cible3.activeSelf && DollyCart2.m_Position == DollyTrack2.PathLength && Cible2Activer == false)
        {
            Cible2Activer = true;
            Cible4.SetActive(true); Cible5.SetActive(true); Cible6.SetActive(true);
            
        }

        

        if (!Cible4.activeSelf && !Cible5.activeSelf && !Cible6.activeSelf && DollyCart2.m_Position == DollyTrack2.PathLength && Cible2Activer == true)
        {
            Debug.Log("Chnagement vers dolly3");
            //Destroy(DollyCart1);
            //Destroy(DollyTrack1);

            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack3Transform;

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack3;
            //VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            


            DollyCart3.m_Speed = 10;

        }


        if (!Cible4.activeSelf && !Cible5.activeSelf && !Cible6.activeSelf && Actif2Vali == false && Cible2Activer == true)
        {

            Actif2 = true;

            SonReparation(Actif2, Actif2Vali);
            Actif2Vali = true;
        }




        if (!Cible4.activeSelf && !Cible5.activeSelf && !Cible6.activeSelf && DollyCart3.m_Position == DollyTrack3.PathLength && Cible3Activer == false && Cible2Activer == true)
        {
            Cible3Activer = true;
            Cible7.SetActive(true); Cible8.SetActive(true); Cible9.SetActive(true);

        }

        

        if (!Cible7.activeSelf && !Cible8.activeSelf && !Cible9.activeSelf && DollyCart3.m_Position == DollyTrack3.PathLength && Cible3Activer == true)
        {
            Debug.Log("Chnagement vers dolly4");
            //Destroy(DollyCart1);
            //Destroy(DollyTrack1);

            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack4Transform;

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack4;
            //VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            


            DollyCart4.m_Speed = 10;
           

            
        }

        if (!Cible7.activeSelf && !Cible8.activeSelf && !Cible9.activeSelf && Actif3Vali == false && Cible3Activer == true)
        {

            Actif3 = true;

            SonReparation(Actif3, Actif3Vali);
            Actif3Vali = true;
        }



        if (!Cible7.activeSelf && !Cible8.activeSelf && !Cible9.activeSelf && DollyCart4.m_Position == DollyTrack4.PathLength && Cible4Activer == false && Cible3Activer == true)
        {
            Cible4Activer = true;
            Cible10.SetActive(true); Cible11.SetActive(true); Cible12.SetActive(true);
        }

        

        if (!Cible10.activeSelf && !Cible11.activeSelf && !Cible12.activeSelf && DollyCart4.m_Position == DollyTrack4.PathLength && Cible4Activer == true)
        {
            Debug.Log("Chnagement vers dolly5");
            //Destroy(DollyCart1);
            //Destroy(DollyTrack1);
            SonReparation(Actif4, Actif4Vali);

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack5;
            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack5Transform;


            DollyCart5.m_Speed = 10;
        }


        if (!Cible10.activeSelf && !Cible11.activeSelf && !Cible12.activeSelf && Actif4Vali == false && Cible4Activer == true)
        {

            Actif4 = true;

            SonReparation(Actif4, Actif4Vali);
            Actif4Vali = true;
        }




        if (!Cible10.activeSelf && !Cible11.activeSelf && !Cible12.activeSelf && DollyCart5.m_Position == DollyTrack5.PathLength && Cible5Activer == false && Cible4Activer == true)
        {
            Cible5Activer = true;
            Cible13.SetActive(true); Cible14.SetActive(true); Cible15.SetActive(true);
        }

        

        if (!Cible13.activeSelf && !Cible14.activeSelf && !Cible15.activeSelf && DollyCart5.m_Position == DollyTrack5.PathLength && Cible5Activer == true)
        {
            Debug.Log("Chnagement vers dolly6");
            //Destroy(DollyCart1);
            //Destroy(DollyTrack1);
            SonReparation(Actif5, Actif5Vali);

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack6;
            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack6Transform;


            DollyCart6.m_Speed = 10;
        }


        if (!Cible13.activeSelf && !Cible14.activeSelf && !Cible15.activeSelf && Actif5Vali == false && Cible5Activer == true)
        {

            Actif5 = true;

            SonReparation(Actif5, Actif5Vali);
            Actif5Vali = true;
        }



        if (!Cible13.activeSelf && !Cible14.activeSelf && !Cible15.activeSelf && DollyCart6.m_Position == DollyTrack6.PathLength && Cible6Activer == false)
        {
            Cible6Activer = true;
            Cible16.SetActive(true); Cible17.SetActive(true); Cible18.SetActive(true);
        }

        

        if (!Cible16.activeSelf && !Cible17.activeSelf && !Cible18.activeSelf && DollyCart6.m_Position == DollyTrack6.PathLength)
        {
            Debug.Log("Chnagement vers dolly7");
            //Destroy(DollyCart1);
            //Destroy(DollyTrack1);
            SonReparation(Actif6, Actif6Vali);

            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_Path = DollyTrack7;
            VirtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>().m_PositionUnits = 0;


            //Changer le follow de caméra ici : 
            VirtualCamera.m_Follow = DollyTrack7Transform;


            DollyCart7.m_Speed = 10;
        }

        if (!Cible16.activeSelf && !Cible17.activeSelf && !Cible18.activeSelf && Actif6Vali == false && Cible6Activer == true)
        {

            Actif6 = true;

            SonReparation(Actif6, Actif6Vali);
            Actif6Vali = true;
        }


        if (!Cible11.activeSelf && !Cible12.activeSelf && !Cible13.activeSelf && Actif5Vali == false && Cible5Activer == true)
        {

            Actif5 = true;

            SonReparation(Actif5, Actif5Vali);
            Actif5Vali = true;
        }


        if (DollyCart7.m_Position == DollyTrack7.PathLength)
        {
            Fin.gameObject.SetActive(true);
            Cible19.SetActive(true);
        }



    }
    public void SonReparation(bool Actif, bool ActifVali)
    {
        if (Actif == true && ActifVali == false)
        {
            Reparation.Play();
            
        }
        ActifVali = true;


    }
}
