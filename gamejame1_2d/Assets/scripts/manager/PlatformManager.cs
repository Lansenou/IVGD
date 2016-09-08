using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    private static PlatformManager _instance;
    public static PlatformManager Instance { get { return _instance; } }

    private List<GameObject> platformList = new List<GameObject>();

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

    public void AddPlatform(GameObject gameObject)
    {
        platformList.Add(gameObject);
    }
}
