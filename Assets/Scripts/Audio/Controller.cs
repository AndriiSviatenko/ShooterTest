using UnityEngine;

namespace Audio
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        private AudioClip _currentClip;

        public void Play(AudioClip audioClip, float volume = 1)
        {
            if (source.isPlaying)
            {
                source.Stop();
                source.PlayOneShot(audioClip, volume);
            }
            else
            {
                source.PlayOneShot(audioClip, volume);
            }
            _currentClip = audioClip;

        }

        public void Stop()
        {
            source.Stop();
        }
    }
}