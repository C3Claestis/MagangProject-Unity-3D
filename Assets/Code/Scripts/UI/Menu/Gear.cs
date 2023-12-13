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
        [SerializeField] GearsCategory gearsCategory;
        [SerializeField] string categoryName;

        [Header("Detail Status Gear")]
        [SerializeField] private int health;
        [SerializeField] private int physicalAttack;
        [SerializeField] private int magicAttack;
        [SerializeField] private int physicalDefense;
        [SerializeField] private int magicDefense;
        [SerializeField] private int statusCritical;
        [SerializeField] private int statusAgility;
        [SerializeField] private int statusEvasion;

        public Gears
        (
            Sprite imageGears,
            string nameGears,
            GearsType type,
            GearsCategory category,
            string categoryName,
            int health,
            int physicalAttack,
            int magicAttack,
            int physicalDefense,
            int magicDefense,
            int statusCritical,
            int statusAgility,
            int statusEvasion
        )
        {
            this.imageGears = imageGears;
            this.nameGears = nameGears;
            this.type = type;
            this.gearsCategory = category;
            this.categoryName = categoryName;
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
        public GearsCategory GetGearsCategory() => gearsCategory;
        public string GetCategoryName() => categoryName;
        public int GetStatusHealth() => health;
        public int GetStatusPhysicalAttack() => physicalAttack;
        public int GetStatusMagicAttack() => magicAttack;
        public int GetStatusPhysicalDefense() => physicalDefense;
        public int GetStatusMagicDefense() => magicDefense;
        public int GetStatusCritical() => statusCritical;
        public int GetStatusAgility() => statusAgility;
        public int GetStatusEvasion() => statusEvasion;
    }

    [Serializable]
    public class Hero
    {
        [Header("Detail Hero")]
        [SerializeField] private string FullNameHero;
        [SerializeField] private string NickNameHero;
        [SerializeField] public Sprite imageHero;
        [SerializeField] private int healthHero;
        [SerializeField] private int physicalAttack;
        [SerializeField] private int magicAttack;
        [SerializeField] private int physicalDefense;
        [SerializeField] private int magicDefense;
        [SerializeField] private int statusCritical;
        [SerializeField] private int statusAgility;
        [SerializeField] private int statusEvasion;
        [SerializeField] private Gears currentWeapon;
        [SerializeField] private Gears currentArmor;
        [SerializeField] private Gears currentBoot;


        public Hero
        (
            string FullNameHero,
            string NickNameHero,
            Sprite imageHero,
            int healthHero,
            int physicalAttack,
            int magicAttack,
            int physicalDefense,
            int magicDefense,
            int statusCritical,
            int statusAgility,
            int statusEvasion
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
        public int GetHeroHealth() => healthHero;
        public int GetPhysicalAttackHero() => physicalAttack;
        public int GetMagicAttackHero() => magicAttack;
        public int GetPhysicalDefenseHero() => physicalDefense;
        public int GetMagicDefenseHero() => magicDefense;
        public int GetCriticalHero() => statusCritical;
        public int GetAgilityHero() => statusAgility;
        public int GetEvasionHero() => statusEvasion;
        public Gears GetCurrentWeapon() => currentWeapon;
        public void SetCurrentWeapon(Gears gears) => currentWeapon = gears;
        public Gears GetCurrentArmor() => currentArmor;
        public void SetCurrentArmor(Gears gears) => currentArmor = gears;
        public Gears GetCurrentBoot() => currentBoot;
        public void SetCurrentBoot(Gears gears) => currentBoot = gears;
        
        //WithoutWeapon
        public int GetHeroHealthStatusWithoutWeapon()
        {
            return currentArmor.GetStatusHealth() + currentBoot.GetStatusHealth() + healthHero;
        }

        public int GetHeroPhysicalAttackStatusWithoutWeapon()
        {
            return currentArmor.GetStatusPhysicalAttack() + currentBoot.GetStatusPhysicalAttack() + physicalAttack;
        }

        public int GetHeroMagicAttackStatusWithoutWeapon()
        {
            return currentArmor.GetStatusMagicAttack() + currentBoot.GetStatusMagicAttack() + magicAttack;
        }

        public int GetHeroPhysicalDefenseStatusWithoutWeapon()
        {
            return currentArmor.GetStatusPhysicalDefense() + currentBoot.GetStatusPhysicalDefense() + physicalDefense;
        }

        public int GetHeroMagicDefenseStatusWithoutWeapon()
        {
            return currentArmor.GetStatusMagicDefense() + currentBoot.GetStatusMagicDefense() + magicDefense;
        }

        public int GetHeroCriticalStatusWithoutWeapon()
        {
            return currentArmor.GetStatusCritical() + currentBoot.GetStatusCritical() + statusCritical;
        }

        public int GetHeroAgilityStatusWithoutWeapon()
        {
            return currentArmor.GetStatusAgility() + currentBoot.GetStatusAgility() + statusAgility;
        }

        public int GetHeroEvasionStatusWithoutWeapon()
        {
            return currentArmor.GetStatusEvasion() + currentBoot.GetStatusEvasion() + statusEvasion;
        }

        //WithoutArmor
        public int GetHeroHealthStatusWithoutArmor()
        {
            return currentWeapon.GetStatusHealth() + currentBoot.GetStatusHealth() + healthHero;
        }

        public int GetHeroPhysicalAttackStatusWithoutArmor()
        {
            return currentWeapon.GetStatusPhysicalAttack() + currentBoot.GetStatusPhysicalAttack() + physicalAttack;
        }

        public int GetHeroMagicAttackStatusWithoutArmor()
        {
            return currentWeapon.GetStatusMagicAttack() + currentBoot.GetStatusMagicAttack() + magicAttack;
        }

        public int GetHeroPhysicalDefenseStatusWithoutArmor()
        {
            return currentWeapon.GetStatusPhysicalDefense() + currentBoot.GetStatusPhysicalDefense() + physicalDefense;
        }

        public int GetHeroMagicDefenseStatusWithoutArmor()
        {
            return currentWeapon.GetStatusMagicDefense() + currentBoot.GetStatusMagicDefense() + magicDefense;
        }

        public int GetHeroCriticalStatusWithoutArmor()
        {
            return currentWeapon.GetStatusCritical() + currentBoot.GetStatusCritical() + statusCritical;
        }

        public int GetHeroAgilityStatusWithoutArmor()
        {
            return currentWeapon.GetStatusAgility() + currentBoot.GetStatusAgility() + statusAgility;
        }

        public int GetHeroEvasionStatusWithoutArmor()
        {
            return currentWeapon.GetStatusEvasion() + currentBoot.GetStatusEvasion() + statusEvasion;
        }

        //WithoutBoot
        public int GetHeroHealthStatusWithoutBoot()
        {
            return currentWeapon.GetStatusHealth() + currentArmor.GetStatusHealth() + healthHero;
        }

        public int GetHeroPhysicalAttackStatusWithoutBoot()
        {
            return currentWeapon.GetStatusPhysicalAttack() + currentArmor.GetStatusPhysicalAttack() + physicalAttack;
        }

        public int GetHeroMagicAttackStatusWithoutBoot()
        {
            return currentWeapon.GetStatusMagicAttack() + currentArmor.GetStatusMagicAttack() + magicAttack;
        }

        public int GetHeroPhysicalDefenseStatusWithoutBoot()
        {
            return currentWeapon.GetStatusPhysicalDefense() + currentArmor.GetStatusPhysicalDefense() + physicalDefense;
        }

        public int GetHeroMagicDefenseStatusWithoutBoot()
        {
            return currentWeapon.GetStatusMagicDefense() + currentArmor.GetStatusMagicDefense() + magicDefense;
        }

        public int GetHeroCriticalStatusWithoutBoot()
        {
            return currentWeapon.GetStatusCritical() + currentArmor.GetStatusCritical() + statusCritical;
        }

        public int GetHeroAgilityStatusWithoutBoot()
        {
            return currentWeapon.GetStatusAgility() + currentArmor.GetStatusAgility() + statusAgility;
        }

        public int GetHeroEvasionStatusWithoutBoot()
        {
            return currentWeapon.GetStatusEvasion() + currentArmor.GetStatusEvasion() + statusEvasion;
        }
    }
    

    [Serializable]
    public class BaseStatusHero
    {
        [Header("Detail Status")]
        [SerializeField] private int statusHealth;
        [SerializeField] private int statusAttack;
        [SerializeField] private int statusDefense;
        [SerializeField] private int statusCritical;
        [SerializeField] private int statusAgility;
        [SerializeField] private int statusEvasion;


        public BaseStatusHero
        (
            int statusHealth,
            int statusAttack,
            int statusDefense,
            int statusCritical,
            int statusAgility,
            int statusEvasion
        )
        {
            this.statusHealth = statusHealth;
            this.statusAttack = statusAttack;
            this.statusDefense = statusDefense;
            this.statusCritical = statusCritical;
            this.statusAgility = statusAgility;
            this.statusEvasion = statusEvasion;
        }

        public int GetBaseStatusHealthHero() => statusHealth;
        public int GetBaseStatusAttackHero() => statusAttack;
        public int GetBaseStatusDefenseHero() => statusDefense;
        public int GetBaseStatusCriticalHero() => statusCritical;
        public int GetBaseStatusAgilityHero() => statusAgility;
        public int GetBaseStatusEvasionHero() => statusEvasion;
    }

}