namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using System;

    public class ListItem : MonoBehaviour
    {
        [Serializable]
        [SerializeField]
        private struct Items
        {
            [SerializeField] public Sprite Icon;
            [SerializeField] public string Name;

        }
        [SerializeField] Items[] allItem;

        void Start()
        {
            ListItems();
        }

        // Update is called once per frame
        public void ListItems()
        {
            GameObject ItemTemplate = transform.GetChild(0).gameObject;
            GameObject Item;

            int CountItem = allItem.Length;

            for (int i = 0; i < CountItem; i++)
            {
                Item = Instantiate(ItemTemplate, transform);
                Item.transform.GetChild(0).GetComponent<Image>().sprite = allItem[i].Icon;
                Item.transform.GetChild(1).GetComponent<Text>().text = allItem[i].Name;
            }

            Destroy(ItemTemplate);
        }
    }

}