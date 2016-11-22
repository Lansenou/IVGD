using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem instance;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip cheerSound;

    [SerializeField]
    private AudioClip stackSound;

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
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStackSound(bool cheer)
    {
        audioSource.pitch = (Random.Range(1.0f, 1.3f));
        audioSource.PlayOneShot(stackSound, 0.5f);
        if (cheer)
        {
            audioSource.pitch = 1.0f;
            audioSource.PlayOneShot(cheerSound, 0.3f);
        }
    }
}
