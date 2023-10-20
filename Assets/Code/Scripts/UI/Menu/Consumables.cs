namespace Nivandria.UI.Consumable
{
    using System;
    using UnityEngine;
    using Microsoft.Unity.VisualStudio.Editor;
    
    [Serializable]
    public class Consumables
    {
        [SerializeField] private Image iconConsumable;
        [SerializeField] private string title;
        [SerializeField] private string description;
        [SerializeField] private string status;
        [SerializeField] private int totalConsumable;

        public Consumables(Image iconConsumable, string title, string description, string status, int totalConsumable)
        {
            this.iconConsumable = iconConsumable;
            this.title = title;
            this.description = description;
            this.status = status;
            this.totalConsumable = totalConsumable;
        }

        public Image GetIcon() => iconConsumable;
        public string GetTitle() => title;
        public string GetDescription() => description;
        public string GetStatus() => status;
        public int GetTotal() => totalConsumable;


    }

}