using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionSouris : MonoBehaviour
{
    public Vector3 screenPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;
    }
}
