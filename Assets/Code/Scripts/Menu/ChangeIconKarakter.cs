namespace Nivandria.UI.Gears
{
    using System.Collections;
    using System.Collections.Generic;
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.UI;

    public class ChangeIconKarakter : MonoBehaviour
    {
        public Sprite[] sprites; // Buat array sprite untuk menyimpan sprite karakter
        private int currentIndex = 0;
        private Image image;
        [SerializeField] private Image hero1;
        [SerializeField] private Image hero2;
        [SerializeField] private Image hero3;
        [SerializeField] private Image hero4;


        private Color activeColor = new Color(0.16f, 0.58f, 0.70f); // Warna 2995B2
        private Color inactiveColor = new Color(0.93f, 0.13f, 0.40f); // Warna EE3166

        private void Start()
        {
            image = GetComponent<Image>();


            if (image != null && sprites.Length > 0)
            {
                image.sprite = sprites[currentIndex]; // Atur sprite pada komponen Image
            }
        }

        private void UpdateButtonColors(int activePanelNumber)
        {
            // Mengatur warna tombol sesuai dengan panel yang aktif
            hero1.GetComponent<Image>().color = (activePanelNumber == 1) ? activeColor : inactiveColor;
            hero2.GetComponent<Image>().color = (activePanelNumber == 2) ? activeColor : inactiveColor;
            hero3.GetComponent<Image>().color = (activePanelNumber == 3) ? activeColor : inactiveColor;
            hero4.GetComponent<Image>().color = (activePanelNumber == 4) ? activeColor : inactiveColor;
        }

        public void OnButton1Click()
        {
            currentIndex = 0;
            if (image != null && currentIndex < sprites.Length)
            {
                image.sprite = sprites[currentIndex];
            }
            UpdateButtonColors(1);
        }

        public void OnButton2Click()
        {
            currentIndex = 1;
            if (image != null && currentIndex < sprites.Length)
            {
                image.sprite = sprites[currentIndex];
            }
            UpdateButtonColors(2);
        }

        public void OnButton3Click()
        {
            currentIndex = 2;
            if (image != null && currentIndex < sprites.Length)
            {
                image.sprite = sprites[currentIndex];
            }
            UpdateButtonColors(3);
        }

        public void OnButton4Click()
        {
            currentIndex = 3;
            if (image != null && currentIndex < sprites.Length)
            {
                image.sprite = sprites[currentIndex];
            }
            UpdateButtonColors(4);
        }
    }

}