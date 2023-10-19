namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;

    public class PickupItem : MonoBehaviour
    {
        [SerializeField] string Name;
        public GameObject display_taking;
        private GameObject taking, geting;
        public bool isTake = false;
        private bool isTaking = false;
        public void SetTaking(bool take) => this.isTaking = take;
        public void Update()
        {
            if (isTaking)
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
}