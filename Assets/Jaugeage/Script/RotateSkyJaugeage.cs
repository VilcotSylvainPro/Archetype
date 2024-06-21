using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyJaugeage : MonoBehaviour
{

    [SerializeField] private float RotationSpeed;


    private void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * RotationSpeed);
    }

}