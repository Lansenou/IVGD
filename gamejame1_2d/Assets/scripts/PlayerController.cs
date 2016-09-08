using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private KeyCode actionKey = 0;
     
    void Awake()
    {
        if (actionKey == 0)
            actionKey = KeyCode.Space;
    }

    void Update()
    {
        if (Input.GetKeyDown(actionKey))
        {
            PlatformSpawner.Instance.ReleasePlatform();
            PlatformSpawner.Instance.IncreaseSpawnHeight(2);
            CameraController.Instance.IncreaseCameraHeight(1);
        }
    }
}
