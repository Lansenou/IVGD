using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    public Camera mainCamera;

    public static CameraShake Instance()
    {
        return _instance;
    }

    private float _shakeLength = .3f;
    private static CameraShake _instance;
    private Vector3 _originalCameraPosition;
    private float _shakeAmt = 0;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void ScreenShake(float amount)
    {
        _shakeAmt = amount;
        InvokeRepeating("ShakeCamera", 0, .008f);
        Invoke("StopShaking", _shakeLength);
    }

    public void SetShakeLength(float length)
    {
        _shakeLength = length;
    }

    private void ShakeCamera()
    {
        //Store the camera original position
        _originalCameraPosition = mainCamera.transform.position;
        //Shake the camera with a bit of randomness so each shake wont be the same
        if (_shakeAmt > 0)
        {            
            float quakeAmt = Random.value * _shakeAmt * 2 - _shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    private void StopShaking()
    {
        CancelInvoke("ShakeCamera");
        //reset camera to original position 
        mainCamera.transform.position = _originalCameraPosition;
    }
}
