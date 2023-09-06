namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class EquippedItem : MonoBehaviour
    {
        [SerializeField] private Image itemIcon;
        [SerializeField] private Text itemName;

        public void UpdateItemDetails(Sprite icon, string name)
        {
            if (icon != null)
            {
                itemIcon.sprite = icon;
            }
            else
            {
                // Jika ikon null, tampilkan ikon garis atau ikon default.
                // Ganti kode berikut dengan ikon garis atau ikon default yang Anda miliki.
                itemIcon.sprite = GetDefaultIcon();
            }

            itemName.text = name;
        }

        private Sprite GetDefaultIcon()
        {
            // Gantilah ini dengan kode yang mengembalikan ikon garis atau ikon default.
            // Anda dapat mengambilnya dari Resources, SpriteAtlas, atau cara lain yang Anda inginkan.
            // Sebagai contoh, kita akan mencari ikon garis dari Resources.
            Sprite defaultIcon = Resources.Load<Sprite>("DefaultIcon");

            return defaultIcon;
        }
    }

}