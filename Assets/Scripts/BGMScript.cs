using UnityEngine;
using System.Collections;

public class BGMScript : MonoBehaviour
{

    public AudioSource audioSource;
    public static GameObject bgmGameObject;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (bgmGameObject)
        {
            Destroy(gameObject);
            return;
        }
        audioSource.Play();
        bgmGameObject = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
