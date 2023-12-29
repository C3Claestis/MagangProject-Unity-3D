namespace Nivandria.UI.Consumable
{
    using System;
    using Nivandria.UI.Quest;
    using UnityEngine;
    using UnityEngine.UI;
    
    [Serializable]
    public class Consumables
    {
        [SerializeField] private Image iconConsumable;
        [SerializeField] private string title;
        [SerializeField] private ConsumableType type;
        [SerializeField] private string description;
        [SerializeField] private string status;
        [SerializeField] private int totalConsumable;

        public Consumables(Image iconConsumable, string title, ConsumableType type, string description, string status, int totalConsumable)
        {
            this.iconConsumable = iconConsumable;
            this.title = title;
            this.type = type;
            this.description = description;
            this.status = status;
            this.totalConsumable = totalConsumable;
        }

        public Image GetIcon() => iconConsumable;
        public string GetTitle() => title;
        public ConsumableType GetConsumableType() => type;
        public string GetDescription() => description;
        public string GetStatus() => status;
        public int GetTotal() => totalConsumable;


    }

}