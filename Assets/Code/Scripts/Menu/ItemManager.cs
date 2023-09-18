namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemManager : MonoBehaviour
    {

        public GameObject panelConsumable;
        public GameObject panelGears;
        public GameObject panelArchive;
        public GameObject panelKey;
        // Start is called before the first frame update
        void Start()
        {
            SetConsumableFirst();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ShowPanel(int panelNumber)
        {
            panelConsumable.SetActive(false);
            panelGears.SetActive(false);
            panelArchive.SetActive(false);
            panelKey.SetActive(false);


            switch (panelNumber)
            {
                case 1:
                    panelConsumable.SetActive(true);
                    break;
                case 2:
                    panelGears.SetActive(true);
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

        }

        public void SetConsumableFirst()
        {
            panelConsumable.SetActive(true);
            panelGears.SetActive(false);
            panelArchive.SetActive(false);
            panelKey.SetActive(false);
        }
    }

}