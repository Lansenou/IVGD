using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public Camera MainCamera;

    public static CameraShake Instance()
    {
        return instance;
    }

    private float shakeLength = .3f;
    private static CameraShake instance;
    private Vector3 originalCameraPosition;
    private float shakeAmt = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ScreenShake(float amount)
    {
        shakeAmt = amount;
        InvokeRepeating("ShakeCamera", 0, .008f);
        Invoke("StopShaking", shakeLength);
    }

    public void SetShakeLength(float length)
    {
        shakeLength = length;
    }

    private void ShakeCamera()
    {
        //Store the camera original position
        originalCameraPosition = MainCamera.transform.position;
        //Shake the camera with a bit of randomness so each shake wont be the same
        if (shakeAmt > 0)
        {            
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = MainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            MainCamera.transform.position = pp;
        }
    }

    private void StopShaking()
    {
        CancelInvoke("ShakeCamera");
        //reset camera to original position 
        MainCamera.transform.position = originalCameraPosition;
    }
}
