using UnityEngine;

namespace Common
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private AudioClip slideSound;
        [SerializeField] private AudioClip removeSound;
        private AudioSource _audioSource;
        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlaySlideSound()
        {
            _audioSource.clip = slideSound;
            _audioSource.Play();
        }

        public void PlayRemoveSound()
        {
            _audioSource.clip = removeSound;
            _audioSource.Play();
        }
    }
}