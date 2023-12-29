namespace Nivandria.UI.Volume
{
    using UnityEngine;
    using UnityEngine.Audio;
    using UnityEngine.UI;
    using TMPro;

    public class VolumeSettings : MonoBehaviour
    {
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private TextMeshProUGUI _textVolume;
        private float _volumeMusic;

        [SerializeField] private Button decreaseButton;
        [SerializeField] private Button increaseButton;

        void Start()
        {
            InitializeMusicSourceVolume();
            AudioManager.Instance.PlayBackgroundMusic();
            UpdateButtonVolume();
        }

        private void InitializeMusicSourceVolume()
        {
            //AudioSource initialVolumeMusic = AudioManager.Instance.GetMusicSource();
            //_volumeMusic = initialVolumeMusic.volume;
            _SetMusicVolume(_volumeMusic);
            
            // Mengatur teks volume sesuai dengan volume awal
            UpdateMusicVolumeText();
        }

        public void DecreaseMusicVolume()
        {
            _volumeMusic = Mathf.Max(0, _volumeMusic - 10); // Mengurangi volume sebanyak 10%
            UpdateMusicVolumeText();
            ApplyMusicVolume();
            UpdateButtonVolume();
            _SetMusicVolume(_volumeMusic/100f);
        }

        public void IncreaseMusicVolume()
        {
            _volumeMusic = Mathf.Min(100, _volumeMusic + 10); // Menambah volume sebanyak 10%
            UpdateMusicVolumeText();
            ApplyMusicVolume();
            UpdateButtonVolume();
            _SetMusicVolume(_volumeMusic/100f);
        }

        private void UpdateMusicVolumeText()
        {
            _textVolume.text = _volumeMusic.ToString("") + "%";
            // Terapkan logika pemutaran suara atau simpan volume ke dalam preferensi di sini
        }


        private void ApplyMusicVolume()
        {
            AudioSource _audioSource = AudioManager.Instance.GetMusicSource();
            if (_audioSource != null)
            {
                float volumePercentage = _volumeMusic / 100f;
                _audioSource.volume = volumePercentage;

                AudioManager.Instance.SetMusicVolume(volumePercentage);
            }
            else
            {
                AudioClip _audioClip = AudioManager.Instance.GetBacksoundClip();
            }
        }

        private void UpdateButtonVolume()
        {
            decreaseButton.interactable = _volumeMusic > 0;
            increaseButton.interactable = _volumeMusic < 100;
        }



        public void _SetMusicVolume(float volume)
        {
            // Mengatur volume grup "Music" pada Audio Mixer
            _audioMixer.SetFloat("Music", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
        }

        public void _SetSFXVolume(float volume)
        {
            // Mengatur volume grup "SFX" pada Audio Mixer
            _audioMixer.SetFloat("SFX", Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20);
        }

        
    }

}