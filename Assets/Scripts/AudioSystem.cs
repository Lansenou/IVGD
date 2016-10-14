using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem instance;
    private AudioSource audioSource;
    public AudioClip clip;

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
    public void PlayPerfectSound()
    {
        audioSource.pitch = 1.0f;
        audioSource.PlayOneShot(clip);
    }
    public void PlayGoodSound()
    {
        audioSource.pitch = 0.8f;
        audioSource.PlayOneShot(clip);
    }
    public void PlayOkSound()
    {
        audioSource.pitch = 0.6f;
        audioSource.PlayOneShot(clip);
    }
    public void PlayBadSound()
    {
        audioSource.pitch = 0.4f;
        audioSource.PlayOneShot(clip);
    }
}
