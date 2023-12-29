namespace Nivandria.UI.MainMenu
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AudioPlayer : MonoBehaviour
    {
        [Header("------ Audio Source ------")]
        [SerializeField] AudioSource BGMSource;
        [SerializeField] AudioSource SFXSource;

        [Header("------ Audio Volume ------")]
        [Range(0, 1)][SerializeField] private float MasterVolume = 0.8f;
        [Range(0, 1)][SerializeField] private float BGMVolume = 0.4f;
        [Range(0, 1)][SerializeField] private float SFXVolume = 0.9f;

        [Header("------ BGM List ------")]
        [SerializeField] private AudioClip mainMenuBGM;
        [SerializeField] private AudioClip gameplayBGM;

        [Header("------ SFX List ------")]
        [SerializeField] private List<AudioClip> clickButtonSFX;
        // [SerializeField] private List<AudioClip> fruitMergeSFX;
        // [SerializeField] private AudioClip lastFruitMergeSFX;
        // [SerializeField] private AudioClip dropFruitSFX;


        public static AudioPlayer Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There's more than one AudioPlayer! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void OnValidate()
        {
            BGMSource.volume = BGMVolume * MasterVolume;
            SFXSource.volume = SFXVolume * MasterVolume;
        }

        public void PlayMainMenuBGM()
        {
            BGMSource.clip = mainMenuBGM;
            BGMSource.Play();
        }

        public void PlayGamePlayBGM()
        {
            BGMSource.clip = gameplayBGM;
            BGMSource.Play();
        }

        public void PlayRandomButtonSFX()
        {
            int randomValue = Random.Range(0, 3);
            SFXSource.PlayOneShot(clickButtonSFX[randomValue]);
        }

        // public void PlayRandomFruitMergeSFX()
        // {
        //     int randomValue = Random.Range(0, 2);
        //     // SFXSource.PlayOneShot(fruitMergeSFX[randomValue]);
        // }

        // public void PlayLastFruitMergeSFX() => SFXSource.PlayOneShot(lastFruitMergeSFX);
        // public void PlayDropFruitSFX() => SFXSource.PlayOneShot(dropFruitSFX);

        public float GetMasterVolume() => MasterVolume;
        public float GetBGMVolume() => BGMVolume;
        public float GetSFXVolume() => SFXVolume;

        public void SetMasterVolume(float volume)
        {
            MasterVolume = volume;
            BGMSource.volume = BGMVolume * MasterVolume;
            SFXSource.volume = SFXVolume * MasterVolume;
        }

        public void SetBGMVolume(float volume)
        {
            BGMVolume = volume;
            BGMSource.volume = BGMVolume * MasterVolume;
        }

        public void SetSFXVolume(float volume)
        {
            SFXVolume = volume;
            SFXSource.volume = SFXVolume * MasterVolume;
        }
    }

}