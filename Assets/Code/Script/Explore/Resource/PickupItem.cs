namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;

    public class PickupItem : MonoBehaviour
    {
        [Header("Consumable Or Objective Quest?")]
        [SerializeField] ObjectiveQuestOrNot objectiveQuestOrNot = ObjectiveQuestOrNot.Consumable;

        [Header("Name Of Object")]
        [SerializeField] string Name;

        [Header("Display Taking On Canvas")]
        [SerializeField] GameObject display_taking;

        private GameObject taking, geting;
        private bool isTake = false;
        private bool isTaking = false;
        public void SetTaking(bool take) => this.isTaking = take;
        public bool GetTake() => isTake;
        public void Update()
        {
            if (isTaking)
            {
                if (objectiveQuestOrNot == ObjectiveQuestOrNot.Consumable)
                {
                    GettingItem();
                    // Hancurkan objek yang di-instantiate saat pemain keluar dari trigger
                    if (taking != null)
                    {
                        Destroy(taking);
                    }
                    ExploreManager.GetInstance().Potato += 1;
                    Destroy(gameObject);
                }
                else
                {
                    ObjectiveQuest objectiveQuest = FindObjectOfType<ObjectiveQuest>();
                    
                    switch (ObjectiveQuest.GetInstance().GetPotatoAndWater())
                    {
                        case 0:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+1);
                            break;
                        case 1:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+2);
                            break;
                        case 2:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+3);
                            break;
                        case 3:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+4);
                            break;
                        case 4:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+5);
                            break;
                        case 5:
                            ObjectiveQuest.GetInstance().SetPotatoAndWater(+6);
                            break;
                    }
                    GettingItem();
                    // Hancurkan objek yang di-instantiate saat pemain keluar dari trigger
                    if (taking != null)
                    {
                        Destroy(taking);
                    }
                    Destroy(gameObject);
                }
            }
        }
        void GettingItem()
        {
            GameObject content = GameObject.Find("Content Geting").gameObject;
            geting = Instantiate(display_taking);
            geting.transform.SetParent(content.transform);
            // Dapatkan komponen Text dari child objek
            Text textComponent = geting.transform.Find("Name").GetComponent<Text>();

            // Ganti teks dengan nama yang sesuai
            if (textComponent != null)
            {
                textComponent.text = "Get " + Name + " x 1"; // Ganti dengan cara Anda mendapatkan nama yang sesuai
            }
            Destroy(geting, 2f);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameObject content = GameObject.Find("Content Taking").gameObject;
                taking = Instantiate(display_taking);
                taking.transform.SetParent(content.transform);
                // Dapatkan komponen Text dari child objek
                Text textComponent = taking.transform.Find("Name").GetComponent<Text>();

                // Ganti teks dengan nama yang sesuai
                if (textComponent != null)
                {
                    textComponent.text = Name; // Ganti dengan cara Anda mendapatkan nama yang sesuai
                }
                isTake = true;
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Hancurkan objek yang di-instantiate saat pemain keluar dari trigger
                if (taking != null)
                {
                    Destroy(taking);
                }
                isTake = false;
            }
        }
    }
    public enum ObjectiveQuestOrNot
    {
        Quest,
        Consumable
    }
}