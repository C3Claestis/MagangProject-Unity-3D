namespace Nivandria.UI.Volume
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        /*
        [Header("Audio Source")]
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource sfxSource;
        [SerializeField] AudioSource voiceOverSource;

        [Header("Audio Clip")]
        [SerializeField] public AudioClip backsound;
        [SerializeField] public AudioClip buttonOnClick;
        
        
        [SerializeField] public AudioClip hoverOverSound;
        [SerializeField] public AudioClip buttonOnClick;
        [SerializeField] public AudioClip loadingScreen;
        */
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /*
        private void Start()
        {
            musicSource.clip = backsound;
            musicSource.Play();
        }

        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }

        public void PlayVoiceOver()
        {

        }

        public void MusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
        */
        
        public AudioSource musicSource;
        public AudioClip backsoundMusic;

        public float SetMusicVolume(float volume)
        {
            if (musicSource != null)
            {
                musicSource.volume = volume;
            }

            return volume;
        }

        public AudioSource GetMusicSource()
        {
            return musicSource;
        }

        public AudioClip GetBacksoundClip()
        {
            return backsoundMusic;
        }

        public void PlayBackgroundMusic()
        {
            // Pastikan Anda sudah mengatur AudioClip di Inspector Unity
            if (musicSource != null && backsoundMusic != null)
            {
                musicSource.clip = backsoundMusic; // Mengatur AudioClip yang akan digunakan
                musicSource.loop = true; // Mengaktifkan mode loop (musik akan terus berulang)
                musicSource.Play(); // Memulai pemutaran musik
            }
        }
    }
}