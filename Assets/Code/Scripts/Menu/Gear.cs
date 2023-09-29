namespace Nivandria.UI.Gears
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.Unity.VisualStudio.Editor;
    using UnityEngine;

    [Serializable]
    public class Gears
    {
        [Header("Detail Gears")]
        [SerializeField] private Sprite imageGears;
        [SerializeField] private string nameGears;
        [SerializeField] private string descriptionGears;

        [Header("Detail Status")]
        [SerializeField] private string health;
        [SerializeField] private string physicalAttack;
        [SerializeField] private string magicAttack;
        [SerializeField] private string physicalDefense;
        [SerializeField] private string magicDefense;
        [SerializeField] private string statusCritical;
        [SerializeField] private string statusAgility;
        [SerializeField] private string statusEvasion;

        public Gears
        (
            Sprite imageGears,
            string nameGears,
            string descriptionGears,
            string health,
            string physicalAttack,
            string magicAttack,
            string physicalDefense,
            string magicDefense,
            string statusCritical,
            string statusAgility,
            string statusEvasion
        )
        {
            this.imageGears = imageGears;
            this.nameGears = nameGears;
            this.descriptionGears = descriptionGears;
            this.health = health;
            this.physicalAttack = physicalAttack;
            this.magicAttack = magicAttack;
            this.physicalDefense = physicalDefense;
            this.magicDefense = magicDefense;
            this.statusCritical = statusCritical;
            this.statusAgility = statusAgility;
            this.statusEvasion = statusEvasion;
        }
        public Sprite GetImageGear() => imageGears;
        public string GetNameGears() => nameGears;
        public string GetDescriptionGear() => descriptionGears;
        public string GetStatusHealth() => health;
        public string GetStatusPhysicalAttack() => physicalAttack;
        public string GetStatusMagicAttack() => magicAttack;
        public string GetStatusPhysicalDefense() => physicalDefense;
        public string GetStatusMagicDefense() => magicDefense;
        public string GetStatusCritical() => statusCritical;
        public string GetStatusAgility() => statusAgility;
        public string GetStatusEvasion() => statusEvasion;
    }

    [Serializable]
    public class Hero
    {
        [Header("Detail Hero")]
        [SerializeField] private string nameHero;
        [SerializeField] private Sprite imageHero;
        [SerializeField] private string descriptionHero;

        public Hero
        (
            string nameHero,
            Sprite imageHero,
            string descriptionHero
        )
        {
            this.nameHero = nameHero;
            this.imageHero = imageHero;
            this.descriptionHero = descriptionHero;
        }

        public string GetNameHero() => nameHero;
        public Sprite GetImageHero() => imageHero;
        public string GetDescriptionHero() => descriptionHero;
    }

    [Serializable]
    public class Status
    {
        [Header("Detail Status")]
        [SerializeField] private string statusHealth;
        [SerializeField] private string statusAttack;
        [SerializeField] private string statusDefense;
        [SerializeField] private string statusCritical;
        [SerializeField] private string statusAgility;
        [SerializeField] private string statusEvasion;


        public Status
        (
            string statusHealth,
            string statusAttack,
            string statusDefense,
            string statusCritical,
            string statusAgility,
            string statusEvasion
        )
        {
            this.statusHealth = statusHealth;
            this.statusAttack = statusAttack;
            this.statusDefense = statusDefense;
            this.statusCritical = statusCritical;
            this.statusAgility = statusAgility;
            this.statusEvasion = statusEvasion;
        }

        public string GetStatusHealth() => statusHealth;
        public string GetStatusAttack() => statusAttack;
        public string GetStatusDefense() => statusDefense;
        public string GetStatusCritical() => statusCritical;
        public string GetStatusAgility() => statusAgility;
        public string GetStatusEvasion() => statusEvasion;
    }

}