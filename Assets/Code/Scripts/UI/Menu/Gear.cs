namespace Nivandria.UI.Gears
{
    using System;
    using UnityEngine;

    [Serializable]
    public class Gears
    {
        [Header("Detail Gears")]
        [SerializeField] private Sprite imageGears;
        [SerializeField] private string nameGears;
        [SerializeField] GearsType type;

        [Header("Detail Status Gear")]
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
            GearsType type,
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
            this.type = type;
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
        public GearsType GetGearsType() => type;
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
        [SerializeField] private string FullNameHero;
        [SerializeField] private string NickNameHero;
        [SerializeField] public Sprite imageHero;
        [SerializeField] private string healthHero;
        [SerializeField] private string physicalAttack;
        [SerializeField] private string magicAttack;
        [SerializeField] private string physicalDefense;
        [SerializeField] private string magicDefense;
        [SerializeField] private string statusCritical;
        [SerializeField] private string statusAgility;
        [SerializeField] private string statusEvasion;
        [SerializeField] private Gears currentWeapon;
        [SerializeField] private Gears currentArmor;
        [SerializeField] private Gears currentBoot;


        public Hero
        (
            string FullNameHero,
            string NickNameHero,
            Sprite imageHero,
            string healthHero,
            string physicalAttack,
            string magicAttack,
            string physicalDefense,
            string magicDefense,
            string statusCritical,
            string statusAgility,
            string statusEvasion
        )
        {
            this.FullNameHero = FullNameHero;
            this.NickNameHero = NickNameHero;
            this.imageHero = imageHero;
            this.healthHero = healthHero;
            this.physicalAttack = physicalAttack;
            this.magicAttack = magicAttack;
            this.physicalDefense = physicalDefense;
            this.magicDefense = magicDefense;
            this.statusCritical = statusCritical;
            this.statusAgility = statusAgility;
            this.statusEvasion = statusEvasion;

            
        }

        public string GetFullNameHero() => FullNameHero;
        public string GetNickNameHero() => NickNameHero;
        public Sprite GetImageHero() => imageHero;
        public string GetHeroHealth() => healthHero;
        public string GetPhysicalAttackHero() => physicalAttack;
        public string GetMagicAttackHero() => magicAttack;
        public string GetPhysicalDefenseHero() => physicalDefense;
        public string GetMagicDefenseHero() => magicDefense;
        public string GetCriticalHero() => statusCritical;
        public string GetAgilityHero() => statusAgility;
        public string GetEvasionHero() => statusEvasion;
        public Gears GetCurrentWeapon() => currentWeapon;
        public void SetCurrentSword(Gears gears) => currentWeapon = gears;
        public Gears GetCurrentArmor() => currentArmor;
        public void SetCurrentArmor(Gears gears) => currentArmor = gears;
        public Gears GetCurrentBoot() => currentBoot;
        public void SetCurrentBoot(Gears gears) => currentBoot = gears;
        
    }

    [Serializable]
    public class BaseStatusHero
    {
        [Header("Detail Status")]
        [SerializeField] private string statusHealth;
        [SerializeField] private string statusAttack;
        [SerializeField] private string statusDefense;
        [SerializeField] private string statusCritical;
        [SerializeField] private string statusAgility;
        [SerializeField] private string statusEvasion;


        public BaseStatusHero
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

        public string GetBaseStatusHealthHero() => statusHealth;
        public string GetBaseStatusAttackHero() => statusAttack;
        public string GetBaseStatusDefenseHero() => statusDefense;
        public string GetBaseStatusCriticalHero() => statusCritical;
        public string GetBaseStatusAgilityHero() => statusAgility;
        public string GetBaseStatusEvasionHero() => statusEvasion;
    }

}