using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shake : MonoBehaviour
{


    public static Shake Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] NoiseSettings Shake1;
    [SerializeField] NoiseSettings Shake2;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (shakeTimer > 0) 
        {
            shakeTimer = shakeTimer - Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 1f;
            }

        }
        */
    }




    public void ShakeCamera(float intensity, float frenquency)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = Shake2;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frenquency;
        Debug.Log(cinemachineBasicMultiChannelPerlin.m_NoiseProfile);
    }

    public void ResetShakeCamera()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = Shake1;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 2f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0.75f;

    }
}
