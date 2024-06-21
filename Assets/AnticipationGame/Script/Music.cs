using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    [SerializeField] private AudioSource Music01;
    [SerializeField] private GameObject Music02;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!Music01.isPlaying) 
        { 
            Music02.SetActive(true);
        }



    }
}
