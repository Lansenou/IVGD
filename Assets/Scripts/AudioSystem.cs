using UnityEngine;
using System.Collections;

public class AudioSystem : MonoBehaviour {

    public static AudioSystem instance;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip perfectSound;

    [SerializeField]
    private AudioClip goodSound;

    [SerializeField]
    private AudioClip OkSound;

    [SerializeField]
    private AudioClip BadSound;

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
        DontDestroyOnLoad(transform.root.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPerfectSound()
    {
        audioSource.PlayOneShot(perfectSound);
    }

    public void PlayGoodSound()
    {
        audioSource.PlayOneShot(goodSound);
    }

    public void PlayOkSound()
    {
        audioSource.PlayOneShot(OkSound);
    }

    public void PlayBadSound()
    {
        audioSource.PlayOneShot(BadSound);
    }
}
