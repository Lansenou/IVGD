using System;
using UnityEngine;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;

    public static CameraController Instance
    {
        get { return _instance; }
    }

    [SerializeField] private float speed = 2f;

    private float cameraHeight;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        cameraHeight = transform.position.y;
    }

    void Update()
    {
        float f = Mathf.Lerp(transform.position.y, cameraHeight, Time.deltaTime*speed);
        transform.position = new Vector3(transform.position.x, f, transform.position.z);
    }

    public void IncreaseCameraHeight(float val)
    {
        cameraHeight += val;
        Debug.Log("Increasing camera height to: " + cameraHeight);
    }
}