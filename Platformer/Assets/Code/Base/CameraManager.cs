using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour // Mort
{
    public static CameraManager Instance;

    [SerializeField] float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    [SerializeField] float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter

    private float ShakeElapsedTime = 0f;

    // Cinemachine Shake
    public CinemachineConfiner confiner;
    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    private void Awake()
    {
        Instance = this;
        virtualCamera =  GetComponentInChildren<CinemachineVirtualCamera>();
        confiner = virtualCamera.GetComponent<CinemachineConfiner>();
    }

    private void Start()
    {
        // If virtual camera exists, get camera noise component
        if (virtualCamera != null)
        {
            virtualCameraNoise = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }
    }

    /// <summary>
    /// Update confiner on the virtual camera
    /// </summary>
    /// <param name="collider"></param>
    public void UpdateConfiner(PolygonCollider2D collider)
    {
        confiner.m_BoundingShape2D = collider;
        confiner.InvalidatePathCache();
    }

    /// <summary>
    /// Shakes the camera with the following parameters: Duration, Amplitude, Frequency. -- Duration always needs to be given a value, amplitude and frequency will use default value when left at 0. Defaults: Amp(1.2f), Freq(2.0f).
    /// </summary>
    public void ShakeCamera(float duration, float amplitude, float frequency)
    {
        ShakeElapsedTime = duration / 10;

        // Set Cinemachine Camera Noise parameters
        if (amplitude <= 0)
        {
            virtualCameraNoise.m_AmplitudeGain = ShakeAmplitude;
        }
        else
        {
            virtualCameraNoise.m_AmplitudeGain = amplitude;
        }
        if (frequency <= 0)
        {
            virtualCameraNoise.m_FrequencyGain = ShakeFrequency;
        }
        else
        {
            virtualCameraNoise.m_FrequencyGain = frequency;
        }
    }

    private void Update()
    {
        // If the Cinemachine components are not set, avoid update
        if (virtualCamera != null || virtualCameraNoise != null)
        {
            // Set Cinemachine Camera Noise parameters
            if (ShakeElapsedTime > 0)
            {
                // Update Shake Timer
                ShakeElapsedTime -= Time.deltaTime;
            }
            else
            {
                // If Camera Shake effect is over, reset variables
                virtualCameraNoise.m_AmplitudeGain = 0f;
                ShakeElapsedTime = 0f;
            }
        }
    }
}
