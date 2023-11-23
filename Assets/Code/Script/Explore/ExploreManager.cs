namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Database;
    using System.IO;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.InputSystem;
    using Cinemachine;

    public class ExploreManager : MonoBehaviour
    {
        [Header("Input System Player")]
        [SerializeField] InputSystem inputSystem;
        [SerializeField] PlayerInput playerInput;

        [Header("Party Setup")]
        [SerializeField] Transform slot_leader;
        [SerializeField] GameObject panel_party;

        [Header("Scene Transisi dan Posisi")]
        [SerializeField] Transform players;
        [SerializeField] Animator transisi;

        [Header("UI Element")]
        [SerializeField] Text drakar;
        [SerializeField] Text level;
        [SerializeField] Text kentang;
        [SerializeField] int tambahdrakar;
        [SerializeField] int tambahlevel;

        [Header("Sensitiviy Camera")]
        [SerializeField] CinemachineFreeLook cinemachineFreeLook;
        [Range(50f, 300f)][SerializeField] float sen_x;
        [Range(0.1f, 2)][SerializeField] float sen_y;

        private static ExploreManager instance;
        private int set_lead;
        public static int set_lastScene;
        public int GetLead() => set_lead;

        public int Potato;
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
            SaveSystem.Init();
        }
        public static ExploreManager GetInstance()
        {
            return instance;
        }
        // Start is called before the first frame update
        void Start()
        {
            transisi.SetTrigger("Out");
            
            Load();
        }

        // Update is called once per frame
        void Update()
        {
            /*
            if (Input.GetKey(KeyCode.K))
            {
                Save();
            }
            if (Input.GetKey(KeyCode.L))
            {
                Load();
            }*/                   

           // ChangeTransisi();
        }
        /// <summary>
        /// Handle Camera Sens
        /// </summary>
        void CameraSens()
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = sen_x;
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = sen_y;
        }
        /// <summary>
        /// Open Panel Party
        /// </summary>
        public void PanelParty()
        {
            panel_party.SetActive(true);
        }
        /// <summary>
        /// Close Panel Party
        /// </summary>
        void ClosePanelParty()
        {
            panel_party.SetActive(false);
        }
        /// <summary>
        /// Untuk Mengatur Setup Party Secara Manual Dalam Explore 
        /// </summary>
        void SetUp()
        {
            string clone = "(Clone)";
            if (slot_leader.GetChild(0).gameObject.name == "Sacra" + clone)
            {
                set_lead = 0;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
                inputSystem.LockMouse(false);
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Vana" + clone)
            {
                set_lead = 1;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
                inputSystem.LockMouse(false);
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Lin" + clone)
            {
                set_lead = 2;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
                inputSystem.LockMouse(false);
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Guard" + clone)
            {
                set_lead = 3;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
                inputSystem.LockMouse(false);
            }
            else
            {
                Debug.Log("Belum Ada Leader");
            }
        }
        void Save()
        {
            Resource resource = new Resource
            {
                Drakar = tambahdrakar,
                Reputation = tambahlevel,
                SensX = sen_x,
                SensY = sen_y,
                Kentang = Potato
            };

            string JSON = JsonUtility.ToJson(resource);

            SaveSystem.Save(JSON);
            Debug.Log("SAVED" + JSON);
        }
        void Load()
        {
            string savestring = SaveSystem.Load();
            if (savestring != null)
            {
                Resource resource = JsonUtility.FromJson<Resource>(savestring);
                drakar.text = resource.Drakar.ToString();
                level.text = resource.Reputation.ToString();
                kentang.text = resource.Kentang.ToString();
                sen_x = resource.SensX;
                sen_y = resource.SensY;
                Potato = resource.Kentang;
            }
            Debug.Log("LOADED" + savestring);
        }
    }
}