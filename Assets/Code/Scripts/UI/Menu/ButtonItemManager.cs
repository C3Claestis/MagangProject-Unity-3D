namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ButtonItemManager : MonoBehaviour
    {

        [SerializeField] private GameObject panelConsumable;
        [SerializeField] private GameObject panelGears;
        [SerializeField] private GameObject panelArchive;
        [SerializeField] private GameObject panelKey;

        [SerializeField] private Image consumable;
        [SerializeField] private Image gears;
        [SerializeField] private Image archive;
        [SerializeField] private Image key;
        

        private Color activeColor = new Color(0.16f, 0.58f, 0.70f); // Warna 2995B2
        private Color inactiveColor = new Color(0.93f, 0.13f, 0.40f); // Warna EE3166

        // Start is called before the first frame update
        void Start()
        {
            SetGearsFirst();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowPanel(int panelNumber)
        {
            panelGears.SetActive(false);
            panelConsumable.SetActive(false);
            panelArchive.SetActive(false);
            panelKey.SetActive(false);


            switch (panelNumber)
            {
                case 1:
                    panelGears.SetActive(true);
                    break;
                case 2:
                    panelConsumable.SetActive(true);
                    break;
                case 3:
                    panelArchive.SetActive(true);
                    break;
                case 4:
                    panelKey.SetActive(true);
                    break;
                default:
                    Debug.Log("Panel number out of range.");
                    break;
            }

            UpdateButtonColors(panelNumber);

        }

        public void SetGearsFirst()
        {
            panelGears.SetActive(true);
            panelConsumable.SetActive(false);
            panelArchive.SetActive(false);
            panelKey.SetActive(false);

            UpdateButtonColors(1);
        }

        private void UpdateButtonColors(int activePanelNumber)
        {
            // Mengatur warna tombol sesuai dengan panel yang aktif
            gears.GetComponent<Image>().color = (activePanelNumber == 1) ? activeColor : inactiveColor;
            consumable.GetComponent<Image>().color = (activePanelNumber == 2) ? activeColor : inactiveColor;
            archive.GetComponent<Image>().color = (activePanelNumber == 3) ? activeColor : inactiveColor;
            key.GetComponent<Image>().color = (activePanelNumber == 4) ? activeColor : inactiveColor;
        }
    }

}