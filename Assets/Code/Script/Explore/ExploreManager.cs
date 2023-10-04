namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class ExploreManager : MonoBehaviour
    {        
        [SerializeField] InputSystem inputSystem;
        [SerializeField] PlayerInput playerInput;
        [SerializeField] Transform slot_leader;
        [SerializeField] GameObject panel_party;
        [SerializeField] Transform players;
        [SerializeField] Animator transisi;
        Vector3[] last_posisi = new Vector3[]
            {
            new Vector3(-4.7f, 1f, -11f), //Posisi dari luar ke dalam rumah
            new Vector3(0f, 1.8f, 15f), //Posisi dari dalam keluar rumah

            new Vector3(25f, 1.8f, 15f), //Posisi dari halaman rumah ke training ground
            new Vector3(0f, 1.8f, -10f), //Posisi dari training ground ke halaman rumah           
           // new Vector3(25f, 1.8f, 15f) //Posisi dari outskrit ke halaman rumah
            };
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
            switch (set_lastScene)
            {
                case 1:                    
                    SwitchPosition(0);
                    Invoke("KembaliKeNol", 0.5f);
                    break;
                case 2:
                    SwitchPosition(1);
                    Invoke("KembaliKeNol", 0.5f);
                    break;
                case 3:
                    SwitchPosition(2);
                    Invoke("KembaliKeNol", 0.5f);
                    break;
                case 4:
                    SwitchPosition(3);
                    Invoke("KembaliKeNol", 0.5f);
                    break;
            }

            Debug.Log(set_lastScene);
        }
        void SwitchPosition(int indexarray)
        {
            players.position = last_posisi[indexarray];
        }
        void KembaliKeNol()
        {
            set_lastScene = 0;
        }
        public void PanelParty()
        {
            panel_party.SetActive(true);
        }
        public void ClosePanelParty()
        {
            panel_party.SetActive(false);
        }
        public void SetUp()
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
    }
}