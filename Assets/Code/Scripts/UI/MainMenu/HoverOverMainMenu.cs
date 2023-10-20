namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using TMPro;

    public class HoverOverMainMenu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject hoverObject;
        private TextMeshProUGUI text;
        private Color originalTextColor;
        private Vector3 originalScale;
        private Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1.1f);

        void Start()
        {
            // Cari komponen TextMeshProUGUI pada GameObject ini.
            text = GetComponentInChildren<TextMeshProUGUI>();

            // Simpan warna teks asli.
            originalTextColor = text.color;

            // Simpan skala asli.
            originalScale = transform.localScale;
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            text.color = new Color(0.858f, 0.992f, 0.0f, 1.0f);
            transform.localScale = hoverScale;
            hoverObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            text.color = originalTextColor;
            transform.localScale = originalScale;
            hoverObject.SetActive(false);
        }
    }

}