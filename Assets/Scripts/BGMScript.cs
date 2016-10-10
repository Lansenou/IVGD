using UnityEngine;
using System.Collections;

public class BGMScript : MonoBehaviour
{
    public AudioSource audioSource;

    private static BGMScript instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
