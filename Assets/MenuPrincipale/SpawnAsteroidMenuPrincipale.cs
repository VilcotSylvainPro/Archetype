using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroidMenuPrincipale : MonoBehaviour
{


    [SerializeField] GameObject Asteroid;
    private int Timer = 5;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Timer >= 0) 
        {
            
            Timer = Timer - 1;
        }

        
        else if(Timer < 0) 
        {
          SpawnAsteroidLol();
          Timer = 4;
        }
        
    }



    public void SpawnAsteroidLol()
    {

        int RandomInt = 0;

        RandomInt = Random.Range(1, 4);

        if (RandomInt == 1) 
        {
            Instantiate(Asteroid, new Vector3(Random.Range(-650f, 650f), 200, 320f), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (RandomInt == 2) 
        {
            Instantiate(Asteroid, new Vector3( -650, 200, Random.Range(-320f, 320f)), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (RandomInt == 3)
        {
            Instantiate(Asteroid, new Vector3(Random.Range(-400f, 400f), 200, -320f), Quaternion.Euler(0f, 0f, 0f));
        }
        else if (RandomInt == 4)
        {
            Instantiate(Asteroid, new Vector3(650f, 200, Random.Range(-320f, 320f)), Quaternion.Euler(0f, 0f, 0f));
        }


    }

    


}
