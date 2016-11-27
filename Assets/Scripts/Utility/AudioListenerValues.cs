using UnityEngine;


namespace Assets.Scripts.Utility
{
    public class AudioListenerValues : MonoBehaviour
    {
        public void SetVolume(float volume)
        {
            AudioListener.volume = volume;
        }

        public void SetPaused(bool paused)
        {
            AudioListener.pause = paused;
        }
    }
}

