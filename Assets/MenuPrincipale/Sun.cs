using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Random.Range(0f, 70f) * Time.deltaTime, Random.Range(0f, 70f) * Time.deltaTime, Random.Range(0f, 70f) * Time.deltaTime, Space.Self);
    }
}
