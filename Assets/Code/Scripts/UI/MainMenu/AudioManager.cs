namespace Nivandria.UI.Volume
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioManager : MonoBehaviour
    {

        [Header("------ Audio Source ------")]
        [SerializeField] AudioSource BGMSource;
        [SerializeField] AudioSource SFXSource;


        [Header("------ Audio Volume ------")]
        [Range(0, 1)][SerializeField] private float MasterVolume = 0.8f;
        [Range(0, 1)][SerializeField] private float BGMVolume = 0.4f;
        [Range(0, 1)][SerializeField] private float SFXVolume = 0.9f;

        public float GetMasterVolume() => MasterVolume;
        public float GetBGMVolume() => BGMVolume;
        public float GetSFXVolume() => SFXVolume;

        public AudioSource musicSource;
        public AudioClip backsoundMusic;

        public static AudioManager Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There's more than one AudioPlayer! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }

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