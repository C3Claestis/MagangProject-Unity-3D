namespace Nivandria.UI.Volume
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.SceneManagement;


    public class AudioSettings : MonoBehaviour
    {
        public TextMeshProUGUI masterVolumeText;
        public TextMeshProUGUI bgmVolumeText;
        public TextMeshProUGUI sfxVolumeText;

        [Range(0, 1)][SerializeField] private float masterVolumeLevel = 0.7f;
        [Range(0, 1)][SerializeField] private float bgmVolumeLevel = 0.8f;
        [Range(0, 1)][SerializeField] private float sfxVolumeLevel = 0.5f;

        public AudioSource bgmAudioSource;
        public AudioSource sfxAudioSource;

        public AudioClip bgmClip; // Assign the BGM audio clip in the Inspector.
        public AudioClip sfxClip; // Assign the SFX audio clip in the Inspector.

        public Button decreaseMasterButton;
        public Button increaseMasterButton;

        public Button decreaseBGMButton;
        public Button increaseBGMButton;

        public Button decreaseSFXButton;
        public Button increaseSFXButton;

        public static AudioSettings Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogWarning("There's more than one AudioPlayer! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            SetUITextFields();
        }

        private void OnDestroy()
        {
            // Unsubscribe from the sceneLoaded event when the object is destroyed
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Reconnect button listeners on scene change
            UpdateButtonInteractability();
            SetUITextFields();
        }

        private void SetUITextFields()
        {
            // Find UI TextMeshPro components in the scene and assign them to the fields
            masterVolumeText = GameObject.Find("MasterVolumeText").GetComponent<TextMeshProUGUI>();
            bgmVolumeText = GameObject.Find("BGMVolumeText").GetComponent<TextMeshProUGUI>();
            sfxVolumeText = GameObject.Find("SFXVolumeText").GetComponent<TextMeshProUGUI>();

            // Update the text fields with the current volume levels
            UpdateVolumeText();
        }

        private void Start()
        {
            UpdateVolumeText();

            // Assign audio clips to AudioSource components.
            if (bgmAudioSource != null && bgmClip != null)
            {
                bgmAudioSource.clip = bgmClip;
                bgmAudioSource.Play();
                bgmAudioSource.loop = true;
            }

            if (sfxAudioSource != null && sfxClip != null)
            {
                sfxAudioSource.clip = sfxClip;
            }
        }

        public void IncreaseMasterVolume()
        {
            masterVolumeLevel = Mathf.Clamp01(masterVolumeLevel + 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(bgmAudioSource, bgmVolumeLevel * masterVolumeLevel);
            UpdateAudioVolume(sfxAudioSource, sfxVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        public void DecreaseMasterVolume()
        {
            masterVolumeLevel = Mathf.Clamp01(masterVolumeLevel - 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(bgmAudioSource, bgmVolumeLevel * masterVolumeLevel);
            UpdateAudioVolume(sfxAudioSource, sfxVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        public void IncreaseBGMVolume()
        {
            bgmVolumeLevel = Mathf.Clamp01(bgmVolumeLevel + 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(bgmAudioSource, bgmVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        public void DecreaseBGMVolume()
        {
            bgmVolumeLevel = Mathf.Clamp01(bgmVolumeLevel - 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(bgmAudioSource, bgmVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        public void IncreaseSFXVolume()
        {
            sfxVolumeLevel = Mathf.Clamp01(sfxVolumeLevel + 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(sfxAudioSource, sfxVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        public void DecreaseSFXVolume()
        {
            sfxVolumeLevel = Mathf.Clamp01(sfxVolumeLevel - 0.1f);
            UpdateVolumeText();
            UpdateAudioVolume(sfxAudioSource, sfxVolumeLevel * masterVolumeLevel);
            UpdateButtonInteractability(); // Update button interactability for all types
        }

        private void UpdateVolumeText()
        {
            if (masterVolumeText != null)
            {
                masterVolumeText.text = Mathf.Round(masterVolumeLevel * 100) + "%";
            }

            if (bgmVolumeText != null)
            {
                bgmVolumeText.text = Mathf.Round(bgmVolumeLevel * 100) + "%";
            }

            if (sfxVolumeText != null)
            {
                sfxVolumeText.text = Mathf.Round(sfxVolumeLevel * 100) + "%";
            }
        }

        private void UpdateButtonInteractability()
        {
            UpdateButtonVolume(decreaseMasterButton, increaseMasterButton, masterVolumeLevel);
            UpdateButtonVolume(decreaseBGMButton, increaseBGMButton, bgmVolumeLevel);
            UpdateButtonVolume(decreaseSFXButton, increaseSFXButton, sfxVolumeLevel);
        }

        private void UpdateButtonVolume(Button decreaseButton, Button increaseButton, float volumeLevel)
        {
            decreaseButton.interactable = volumeLevel > 0;
            increaseButton.interactable = volumeLevel < 1;
        }

        private void UpdateAudioVolume(AudioSource audioSource, float volumeLevel)
        {
            if (audioSource != null)
            {
                audioSource.volume = volumeLevel;
            }
        }
    }

}