using System;
using UnityEngine;
using System.Collections;

public class PlatformSpawner : MonoBehaviour
{  
    private static PlatformSpawner _instance;
    public static PlatformSpawner Instance { get { return _instance; } }

    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float speed = 2f;

    private float spawnHeight;
    private Platform currentPlatform;

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

        if (spawnObject == null)
        {
            throw new NullReferenceException("Missing spawn object, please provide a gameobject through the inspector");
        }
    }

    void Start()
    {
        if (!HasPlatform())
        {
            SpawnPlatform();
        }

        spawnHeight = transform.position.y;
    }

    void Update()
    {
        float f = Mathf.Lerp(transform.position.y, spawnHeight, Time.deltaTime * speed);
        transform.position = new Vector3(transform.position.x, f, transform.position.z);
    }

    private bool HasPlatform()
    {
        return currentPlatform != null;
    }

    private void SpawnPlatform()
    {
        GameObject platform = (GameObject) GameObject.Instantiate(spawnObject);
        currentPlatform = platform.GetComponent<Platform>();
        currentPlatform.Attach(this.gameObject);
    }
   
    public void ReleasePlatform()
    {
        if (currentPlatform != null)
        {
            currentPlatform.Release();
            currentPlatform = null;
            Invoke("SpawnPlatform", 2);
        }
    }

    public void IncreaseSpawnHeight(float val)
    {
        spawnHeight += val;
        Debug.Log("Increasing spawn height to: " + spawnHeight);
    }
}
