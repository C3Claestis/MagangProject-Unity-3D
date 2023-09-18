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
        private int set_lead;        
        public int GetLead() => set_lead;
        // Start is called before the first frame update
        void Start()
        {

            
        }

        // Update is called once per frame
        void Update()
        {

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
            if(slot_leader.GetChild(0).gameObject.name == "Sacra" + clone)
            {
                set_lead = 0;
                ClosePanelParty();
                playerInput.SwitchCurrentActionMap("Player");
                inputSystem.SetIsSpawn(true);
            }
            else if(slot_leader.GetChild(0).gameObject.name == "Vana" + clone)
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