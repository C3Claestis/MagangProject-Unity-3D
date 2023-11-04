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
        Vector3[] last_posisi = new Vector3[]
            {
            new Vector3(-4.7f, 1f, -11f), //Posisi dari luar ke dalam rumah
            new Vector3(0f, 1.8f, 15f), //Posisi dari dalam keluar rumah

            new Vector3(120f, 1.8f, 170f), //Posisi dari halaman rumah ke training ground
            new Vector3(45.5f, 1.8f, -75f), //Posisi dari training ground ke halaman rumah  

           // new Vector3(25f, 1.8f, 15f) //Posisi dari outskrit ke halaman rumah
            };

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
            if (Input.GetKey(KeyCode.K))
            {
                Save();
            }
            if (Input.GetKey(KeyCode.L))
            {
                Load();
            }
            ChangeTransisi();            
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
        /// Mengubah Index Sesuai Vector3 Array
        /// </summary>
        /// <param name="indexarray"></param>
        void SwitchPosition(int indexarray)
        {
            players.position = last_posisi[indexarray];
        }
        /// <summary>
        /// Mengatur Kembali Nilai Transisi Ke 0 Agar Tidak Terus Menerus Mengeksekusi Di ChangeTransisi
        /// </summary>
        void KembaliKeNol()
        {
            set_lastScene = 0;
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
        /// <summary>
        /// Untuk Mengatur Posisi Pada Scene Transisi
        /// </summary>
        void ChangeTransisi()
        {
            switch (set_lastScene)
            {
                case 1:
                    SwitchPosition(0);
                    Invoke(nameof(KembaliKeNol), 0.5f);
                    break;
                case 2:
                    SwitchPosition(1);
                    Invoke(nameof(KembaliKeNol), 0.5f);
                    break;
                case 3:
                    SwitchPosition(2);
                    Invoke(nameof(KembaliKeNol), 0.5f);
                    break;
                case 4:
                    SwitchPosition(3);
                    Invoke(nameof(KembaliKeNol), 0.5f);
                    break;
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