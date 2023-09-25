namespace Nivandria.UI.Gears
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Unity.VisualStudio.Editor;
    using UnityEngine;

    [Serializable]
    public class Gear
    {
        [Header("Detail Hero")]
        [SerializeField] private string nameHero;
        [SerializeField] private Image imageHero1;
        [SerializeField] private Image imageHero2;
        [SerializeField] private Image imageHero3;
        [SerializeField] private List<Image> imageHero;
        [SerializeField] private string descriptionHero;

        [Header("Detail Status")]
        [SerializeField] private string statusHealth;
        [SerializeField] private string statusAttack;
        [SerializeField] private string statusDefense;
        [SerializeField] private string statusCritical;
        [SerializeField] private string statusAgility;
        [SerializeField] private string statusEvasion;

        [Header("Detail Gears")]
        [SerializeField] private string listGears;
        [SerializeField] private Image imageGears;
        [SerializeField] private string gears;

        public Gear
        (
            string nameHero, 
            List<Image> imageHero, 
            string descriptionHero, 
            string statusHealth, 
            string statusAttack, 
            string statusDefense,
            string statusCritical,
            string statusAgility,
            string statusEvasion,
            string listGears,
            Image imageGears,
            string gears
        )
        {
            this.nameHero = nameHero;
            this.imageHero = imageHero;
            this.descriptionHero = descriptionHero;
            this.statusHealth = statusHealth;
            this.statusAttack = statusAttack;
            this.statusDefense = statusDefense;
            this.statusCritical = statusCritical;
            this.statusAgility = statusAgility;
            this.statusEvasion = statusEvasion;
            this.listGears = listGears;
            this.imageGears = imageGears;
            this.gears = gears;
        }

        public string GetNameHero() => nameHero;
        public List<Image> GetImageHero() => imageHero;
        public string GetDescriptionHero() => descriptionHero;
        public string GetStatusHealth() => statusHealth;
        public string GetStatusAttack() => statusAttack;
        public string GetStatusDefense() => statusDefense;
        public string GetStatusCritical() => statusCritical;
        public string GetStatusAgility() => statusAgility;
        public string GetStatusEvasion() => statusEvasion;
        public string GetListGears() => listGears;
        public Image GetImageGear() => imageGears;
        public string GetGears() => gears;


    }

}