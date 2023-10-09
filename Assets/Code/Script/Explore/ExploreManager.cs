namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
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

        [SerializeField] InputSystem inputSystem;
        [SerializeField] PlayerInput playerInput;
        [SerializeField] Transform slot_leader;
        [SerializeField] GameObject panel_party;
        [SerializeField] Transform players;
        [SerializeField] Animator transisi;
        
        private static ExploreManager instance;
        private int set_lead;
        public static int set_lastScene;
        public int GetLead() => set_lead;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
        public static ExploreManager GetInstance()
        {
            return instance;
        }
        // Start is called before the first frame update
        void Start()
        {
            transisi.SetTrigger("Out");
        }

        // Update is called once per frame
        void Update()
        {
            ChangeTransisi();
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
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Vana" + clone)
            {
                set_lead = 1;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Lin" + clone)
            {
                set_lead = 2;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
            }
            else if (slot_leader.GetChild(0).gameObject.name == "Guard" + clone)
            {
                set_lead = 3;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
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
    }
}