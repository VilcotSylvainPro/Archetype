using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    [SerializeField] GameObject Cross1;
    [SerializeField] GameObject Cross2;
    [SerializeField] GameObject Cross3;
    [SerializeField] GameObject Cross4;


    private Vector3 screenPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        screenPosition = Input.mousePosition;

       
        
        Cross1.transform.position = new Vector3(screenPosition.x+10, screenPosition.y, screenPosition.z);
        Cross2.transform.position = new Vector3(screenPosition.x - 10, screenPosition.y, screenPosition.z);
        Cross3.transform.position = new Vector3(screenPosition.x, screenPosition.y+10, screenPosition.z);
        Cross4.transform.position = new Vector3(screenPosition.x, screenPosition.y-10, screenPosition.z);
    }
}
