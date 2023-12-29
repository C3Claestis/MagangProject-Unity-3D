namespace Nivandria.UI
{
    using UnityEngine;

    public class SettingVariableManager : MonoBehaviour
    {
        public static SettingVariableManager Instance { get; private set; }

        [Header("Audio Source")]
        public int MasterVolume_Volume;
        public int BackgroundMusic_Volume;
        public int SFX_Volume;
        public int VoiceOver_Volume;

        [Header("Audio Clip")]
        [SerializeField] public AudioClip backsound;
        [SerializeField] public AudioClip VoiceOver;


        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            
        }
    }

}