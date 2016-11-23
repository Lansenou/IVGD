using UnityEngine;
using Assets.Scripts.Utility;

public class AudioSystem : Singleton<AudioSystem> {

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip cheerSound;

    [SerializeField]
    private AudioClip stackSound;

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
