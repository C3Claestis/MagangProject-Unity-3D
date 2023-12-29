namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class ItemPanelController : MonoBehaviour
    {
        [SerializeField] public Text itemNameText;
        private List<string> itemNames = new List<string> { "Armors", "Swords", "Boots" };
        private int currentItemIndex = 0;

        private void Start()
        {
            // Mengatur nama item awal ke "Armors"
            itemNameText.text = itemNames[currentItemIndex];
        }

        public void OnLeftArrowClick()
        {
            // Mengurangi indeks item saat tombol kiri ditekan
            currentItemIndex = (currentItemIndex - 1 + itemNames.Count) % itemNames.Count;
            // Mengganti teks nama item
            itemNameText.text = itemNames[currentItemIndex];
            // Mengatur daftar item sesuai dengan nama item yang baru
        }

        public void OnRightArrowClick()
        {
            // Menambah indeks item saat tombol kanan ditekan
            currentItemIndex = (currentItemIndex + 1) % itemNames.Count;
            // Mengganti teks nama item
            itemNameText.text = itemNames[currentItemIndex];
            // Mengatur daftar item sesuai dengan nama item yang baru
        }
    }

}