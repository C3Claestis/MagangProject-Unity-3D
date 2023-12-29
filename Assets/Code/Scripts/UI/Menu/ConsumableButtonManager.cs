namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;
    using Nivandria.UI.Consumable;

    public class ConsumableButtonManager : MonoBehaviour
    {
        public static ConsumableButtonManager Instance { get; private set; }

        [Header("Image Button")]
        [SerializeField] private Image Consumable;
        [SerializeField] private Image Material;
        [SerializeField] private Image Ingredients;

        private Color activeColor = new Color(1f, 0.9411765f, 0.7843137f); // Warna FFF0C8
        private Color inactiveColor = new Color(0.8784314f, 0.8470588f, 0.772549f); // Warna E0D8C5
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            SetConsumableFirst();
        }

        public void ChangeTypeConsumable(int index)
        {

            switch (index)
            {
                case 1:
                    ConsumablesLogManager.Instance.consumableType = ConsumableType.Consumable;
                    break;
                case 2:
                    ConsumablesLogManager.Instance.consumableType = ConsumableType.Material;
                    break;
                case 3:
                    ConsumablesLogManager.Instance.consumableType = ConsumableType.Ingredients;
                    break;
                default:
                    Debug.Log("Panel number out of range.");
                    break;
            }

            UpdateButtonColors(index);
        }

        private void UpdateButtonColors(int activePanelNumber)
        {
            Consumable.GetComponent<Image>().color = (activePanelNumber == 1) ? activeColor : inactiveColor;
            Material.GetComponent<Image>().color = (activePanelNumber == 2) ? activeColor : inactiveColor;
            Ingredients.GetComponent<Image>().color = (activePanelNumber == 3) ? activeColor : inactiveColor;
        }

        public void SetConsumableFirst()
        {
            UpdateButtonColors(1);
        }
    }

}