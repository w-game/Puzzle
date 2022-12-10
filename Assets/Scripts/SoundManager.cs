using UnityEngine;

namespace Common
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private AudioClip slideSound;
        [SerializeField] private AudioClip removeSound;
        [SerializeField] private AudioSource bgm;
        [SerializeField] private AudioSource soundEffect;

        private const float BgmRate = 0.4f;
        public float BgmVolume => bgm.volume / BgmRate;
        public float SoundEffectVolume => soundEffect.volume;

        public void Init()
        {
            bgm.volume = PlayerPrefs.GetFloat("BgmVolume", BgmRate);
            soundEffect.volume = PlayerPrefs.GetFloat("SoundEffectVolume", 1);
        }
        
        public void PlaySlideSound()
        {
            soundEffect.clip = slideSound;
            soundEffect.Play();
        }

        public void PlayRemoveSound()
        {
            soundEffect.clip = removeSound;
            soundEffect.Play();
        }

        public void ChangeBgmVolume(float value)
        {
            bgm.volume = value * BgmRate;
            PlayerPrefs.SetFloat("BgmVolume", value);
        }

        public void ChangeSoundEffectVolume(float value)
        {
            soundEffect.volume = value;
            PlayerPrefs.SetFloat("SoundEffectVolume", value);
        }
    }
}