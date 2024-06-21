using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{

    [SerializeField] Camera sunCamera;



    private bool isVisible;



    private void Awake()
    {
        
        sunCamera = GameObject.FindGameObjectWithTag("SunCamera").GetComponent<Camera>();
    }



    void Start()
    {
        StartCoroutine(DestroyAfterDelay());
    }

    private IEnumerator DestroyAfterDelay()
    {
        yield return new WaitForSeconds(2.0f);

        
        Player.Instance.DecreaseSpeed(10.0f);

        Destroy(gameObject);
    }


    private void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(sunCamera);
        isVisible = GeometryUtility.TestPlanesAABB(planes, GetComponent<Collider>().bounds);

        if (!isVisible)
        {
            Player.Instance.DecreaseSpeed(10.0f);
            Destroy(gameObject);
        }
    }
}
