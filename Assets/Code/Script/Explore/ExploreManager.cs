namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ExploreManager : MonoBehaviour
    {
        [SerializeField] GameObject panel_party, panel_explore;
        private int set_lead;
        public void SetLead(int lead) => this.set_lead = lead;
        public int GetLead() => set_lead;
        // Start is called before the first frame update
        void Start()
        {
            if(set_lead == 0)
            {
                set_lead = 1;
            }            
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void PanelParty()
        {
            panel_party.SetActive(true);
            panel_explore.SetActive(false);
        }
        public void ClosePanelParty()
        {
            panel_party.SetActive(false);
            panel_explore.SetActive(true);
        }
    }
}