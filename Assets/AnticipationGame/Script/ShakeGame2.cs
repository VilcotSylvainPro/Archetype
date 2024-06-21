using System.Collections;
using UnityEngine;
using Cinemachine;

public class ShakeGame2 : MonoBehaviour
{
    public static ShakeGame2 Instance { get; private set; }
    private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] NoiseSettings Shake1;
    public float TimerShake = 1f; 
    private Coroutine shakeCoroutine;
    [SerializeField] GameObject DmgAlert;

    void Awake()
    {
        Instance = this;
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void ShakeCamera(float intensity, float frequency)
    {
        
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
        }

        
        shakeCoroutine = StartCoroutine(ShakeCameraCoroutine(intensity, frequency));
    }

    private IEnumerator ShakeCameraCoroutine(float intensity, float frequency)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_NoiseProfile = Shake1;
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        DmgAlert.SetActive(true);


        yield return new WaitForSeconds(TimerShake);

      
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0f;
        DmgAlert.SetActive(false);

    }
}