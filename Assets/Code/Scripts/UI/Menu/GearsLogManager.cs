namespace Nivandria.UI.Gears
{
    using System;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.AI;
    using UnityEngine.UI;


    public class GearsLogManager : MonoBehaviour
    {
        public static GearsLogManager Instance { get; private set; }

        [Header("Gears Content Container")]
        [SerializeField] public Transform WeaponContentContainer;
        [SerializeField] public Transform ArmorContentContainer;
        [SerializeField] public Transform BootContentContainer;

        [Header("Detail Status Gear")]
        [SerializeField] TextMeshProUGUI health;
        [SerializeField] TextMeshProUGUI physicalAttack;
        [SerializeField] TextMeshProUGUI magicAttack;
        [SerializeField] TextMeshProUGUI physicalDefense;
        [SerializeField] TextMeshProUGUI magicDefense;
        [SerializeField] TextMeshProUGUI statusCritical;
        [SerializeField] TextMeshProUGUI statusAgility;
        [SerializeField] TextMeshProUGUI statusEvasion;

        [Header("Hero Name")]
        [SerializeField] private TextMeshProUGUI[] NickNameHeroes;
        [SerializeField] private TextMeshProUGUI FullNameHero;

        [Header("Base Status Hero")]
        [SerializeField] TextMeshProUGUI healthHero;
        [SerializeField] TextMeshProUGUI physicalAttackHero;
        [SerializeField] TextMeshProUGUI magicAttackHero;
        [SerializeField] TextMeshProUGUI physicalDefenseHero;
        [SerializeField] TextMeshProUGUI magicDefenseHero;
        [SerializeField] TextMeshProUGUI statusCriticalHero;
        [SerializeField] TextMeshProUGUI statusAgilityHero;
        [SerializeField] TextMeshProUGUI statusEvasionHero;



        [Header("Heroes List")]
        [SerializeField] List<Hero> HeroList = new List<Hero>();

        [Header("Gears List")]
        [SerializeField] List<Gears> gearList = new List<Gears>();
        [SerializeField] GameObject gearsLog;

        [Header("GearsType")]
        [SerializeField] public GearsType gearsType;
        [SerializeField] GearsType currentGearsType;

        [Header("Toggle Group Gears")]
        [SerializeField] ToggleGroup WeaponToggleGroup;
        [SerializeField] ToggleGroup ArmorToggleGroup;
        [SerializeField] ToggleGroup BootsToggleGroup;

        private Gears gearsPreview;
        private GameObject selectedGear = null;
        bool firstGears;
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }
        void Start()
        {
            GearsLogInitialization();
        }

        public void UpdateStatusHeroText(
            string heroName,
            int heroHealth,
            int heroPhysicalAttack,
            int heroMagicAttack,
            int heroPhysicalDefense,
            int heroMagicDefense,
            int heroCritical,
            int heroAgility,
            int heroEvasion
            )
        {
            FullNameHero.text = heroName;
            healthHero.text = heroHealth.ToString();
            physicalAttackHero.text = heroPhysicalAttack.ToString();
            magicAttackHero.text = heroMagicAttack.ToString();
            physicalDefenseHero.text = heroPhysicalDefense.ToString();
            magicDefenseHero.text = heroMagicDefense.ToString();
            statusCriticalHero.text = heroCritical.ToString();
            statusAgilityHero.text = heroAgility.ToString();
            statusEvasionHero.text = heroEvasion.ToString();
        }

        public void DeselectToggles()
        {
            DeselectToggleGroup(WeaponToggleGroup);
            DeselectToggleGroup(ArmorToggleGroup);
            DeselectToggleGroup(WeaponToggleGroup);
        }

        void DeselectToggleGroup(ToggleGroup toggleGroup)
        {
            foreach (var toggle in toggleGroup.ActiveToggles())
            {
                toggle.isOn = false;
            }
        }

        Gears GetHeroCurrentGear(Gears gear, Hero hero)
        {
            switch (gear.GetGearsType())
            {
                case GearsType.Weapons:
                    return hero.GetCurrentWeapon();
                case GearsType.Armor:
                    return hero.GetCurrentArmor();
                case GearsType.Boots:
                    return hero.GetCurrentBoot();
                default:
                    return null;
            }
        }

        public bool DisplaySpecialGearHero(Gears gear, Hero currentHero)
        {
            bool isSpecialGear = gear.GetGearsCategory() == GearsCategory.Special && gear.GetCategoryName() != currentHero.GetFullNameHero();
            return isSpecialGear;
        }

        public void DisplayEquippedGearHero(Gears gear, Hero currentHero, GameObject newGear)
        {
            var canvasGroup = newGear.transform.GetChild(4).GetComponent<CanvasGroup>();
            if (gear.GetNameGears() == currentHero.GetCurrentWeapon().GetNameGears())
            {
                ShowCanvasGroup(canvasGroup, true);
            }
        }

        void DisplayGeneralGearEquippedHero(Gears gear, Hero hero, GameObject newGear)
        {
            if (gear.GetNameGears() == GetHeroCurrentGear(gear, hero).GetNameGears())
            {
                CanvasGroup generalGearEquipped = newGear.transform.GetChild(3).GetComponent<CanvasGroup>();
                generalGearEquipped.alpha = 1;
                generalGearEquipped.interactable = false;
                generalGearEquipped.blocksRaycasts = false;
            }
        }

        void DisplayEquippedAndGeneralGear(GameObject newGear, Gears gear, Hero currentHero)
        {
            DisplayEquippedGearHero(gear, currentHero, newGear);

            foreach (Hero hero in HeroList)
            {
                if (hero == currentHero) continue;
                DisplayGeneralGearEquippedHero(gear, hero, newGear);
            }
        }

        public void DestroyContainerGear(Transform container)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                Destroy(container.GetChild(i).gameObject);
            }
        }

        public void DestroyAllContainerGears()
        {
            DestroyContainerGear(WeaponContentContainer);
            DestroyContainerGear(ArmorContentContainer);
            DestroyContainerGear(BootContentContainer);
        }

        GameObject InstantiateGear(Gears gear)
        {
            Transform container;
            switch (gear.GetGearsType())
            {
                case GearsType.Weapons:
                    container = WeaponContentContainer;
                    break;
                case GearsType.Armor:
                    container = ArmorContentContainer;
                    break;
                case GearsType.Boots:
                    container = BootContentContainer;
                    break;
                default:
                    Debug.LogError("Gear type not recognized");
                    return null;
            }

            return Instantiate(gearsLog, container);
        }

        public void GearsLogInitialization()
        {
            DestroyAllContainerGears();

            foreach (Gears gear in gearList)
            {
                int heroIndex = GearsButtonManager.Instance.GetHeroIndex();
                Hero currentHero = GetHero(heroIndex);

                if (DisplaySpecialGearHero(gear, currentHero)) continue;
                if (GearIsEquipped(gear, currentHero)) continue;
                GameObject newGear = InstantiateGear(gear);
                DisplayEquippedAndGeneralGear(newGear, gear, currentHero);

                // Retrieve UI Components

                // Get Image Gear
                Image iconArmor = newGear.transform.GetChild(0).GetComponent<Image>();
                iconArmor.sprite = gear.GetImageGear();

                // Get Name Gear
                TextMeshProUGUI gearName = newGear.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                gearName.text = gear.GetNameGears();

                // Get Icon Equipped Gear
                GameObject iconGearEquipped = newGear.transform.GetChild(4).gameObject;
                Image iconGearColor = iconGearEquipped.GetComponent<Image>();
                CanvasGroup iconEquipped = iconGearEquipped.GetComponent<CanvasGroup>();
                Button iconEquippedButton = iconGearEquipped.GetComponent<Button>();

                // Get Button Equip Gear
                GameObject gearEquipped = newGear.transform.GetChild(5).gameObject;
                Image gearColor = gearEquipped.GetComponent<Image>();
                CanvasGroup gearCanvasGroup = gearEquipped.GetComponent<CanvasGroup>();
                Button gearButton = gearEquipped.GetComponent<Button>();

                Toggle toggle = newGear.GetComponent<Toggle>();

                switch (gear.GetGearsType())
                {
                    case GearsType.Weapons:
                        toggle.group = WeaponToggleGroup;
                        break;
                    case GearsType.Armor:
                        toggle.group = ArmorToggleGroup;
                        break;
                    case GearsType.Boots:
                        toggle.group = BootsToggleGroup;
                        break;
                    default:
                        Debug.LogError("Gear tidak diketahui");
                        break;
                }

                // Tambahkan event listener untuk menangani perubahan nilai toggle
                toggle.onValueChanged.AddListener((value) =>
                {
                    HandleToggleValueChange(value, gear, currentHero, gearColor, gearCanvasGroup);

                    //Button Equip Gear
                    gearButton.onClick.AddListener(() =>
                    {
                        Debug.Log("Tes ini Button GearEquipped");
                        int heroIndex = GearsButtonManager.Instance.GetHeroIndex();
                        Hero currentHero = GetHero(heroIndex);
                        Gears UnEquipGear = gear;
                        UnEquipGear = new Gears(null, "", UnEquipGear.GetGearsType(), UnEquipGear.GetGearsCategory(), "", 0, 0, 0, 0, 0, 0, 0, 0);
                        Gears setGear = gear;

                        switch (setGear.GetGearsType())
                        {
                            case GearsType.Weapons:
                                HideUnequipButton(WeaponContentContainer);
                                foreach (Hero hero in HeroList)
                                {
                                    if (hero == currentHero) continue;
                                    if (gear.GetNameGears() != hero.GetCurrentWeapon().GetNameGears()) continue;
                                    {
                                        hero.SetCurrentWeapon(UnEquipGear);
                                    }
                                }
                                currentHero.SetCurrentWeapon(setGear);
                                break;
                            case GearsType.Armor:
                                HideUnequipButton(ArmorContentContainer);
                                foreach (Hero hero in HeroList)
                                {
                                    if (hero == currentHero) continue;
                                    if (gear.GetNameGears() != hero.GetCurrentArmor().GetNameGears()) continue;
                                    {
                                        hero.SetCurrentArmor(UnEquipGear);
                                    }
                                }
                                currentHero.SetCurrentArmor(setGear);
                                break;
                            case GearsType.Boots:
                                HideUnequipButton(BootContentContainer);
                                foreach (Hero hero in HeroList)
                                {
                                    if (hero == currentHero) continue;
                                    if (gear.GetNameGears() != hero.GetCurrentBoot().GetNameGears()) continue;
                                    {
                                        hero.SetCurrentBoot(UnEquipGear);
                                    }
                                }
                                currentHero.SetCurrentBoot(setGear);
                                break;
                        }
                        gearColor.color = new Color(1, 0, 0);
                        ShowCanvasGroup(iconEquipped, true);

                        GearsButtonManager.Instance.UpdateStatusHero(heroIndex);
                        UpdateColorPreviewStatusHero();
                        UpdateAllDeselectColorStatusPreview();
                        DeselectToggles();
                    });

                    //Button UnEquip Gear
                    iconEquippedButton.onClick.AddListener(() =>
                    {
                        Debug.Log("Tes ini button iconEquipped");
                        Gears setGear = gear;

                        setGear = new Gears(null, "", setGear.GetGearsType(), setGear.GetGearsCategory(), "", 0, 0, 0, 0, 0, 0, 0, 0);
                        ShowCanvasGroup(iconEquipped, false);

                        switch (setGear.GetGearsType())
                        {
                            case GearsType.Weapons:
                                currentHero.SetCurrentWeapon(setGear);
                                break;
                            case GearsType.Armor:
                                currentHero.SetCurrentArmor(setGear);
                                break;
                            case GearsType.Boots:
                                currentHero.SetCurrentBoot(setGear);
                                break;
                        }

                        GearsButtonManager.Instance.UpdateStatusHero(heroIndex);
                        UpdateColorPreviewStatusHero();
                        UpdateAllDeselectColorStatusPreview();
                        DeselectToggles();
                    });



                });
            }
        }

        private bool GearIsEquipped(Gears gear, Hero currentHero)
        {
            switch (gear.GetGearsType())
            {
                case GearsType.Weapons:
                    foreach (Hero hero in HeroList)
                    {
                        if (hero == currentHero) continue;
                        if (!(hero.GetCurrentWeapon() == gear)) continue;
                        return true;
                    }
                    break;
                case GearsType.Armor:
                    foreach (Hero hero in HeroList)
                    {
                        if (hero == currentHero) continue;
                        if (!(hero.GetCurrentArmor() == gear)) continue;
                        return true;
                    }
                    break;
                case GearsType.Boots:
                    foreach (Hero hero in HeroList)
                    {
                        if (hero == currentHero) continue;
                        if (!(hero.GetCurrentBoot() == gear)) continue;
                        return true;
                    }
                    break;
            }
            return false;
        }

        void HandleToggleValueChange(bool value, Gears gear, Hero currentHero, Image gearColor, CanvasGroup gearCanvasGroup)
        {
            Debug.Log($"{gear.GetNameGears()} Toggle isOn: {value}");

            // Get current and selected gears
            Gears currentWeapon = currentHero.GetCurrentWeapon();
            Gears currentArmor = currentHero.GetCurrentArmor();
            Gears currentBoot = currentHero.GetCurrentBoot();
            Gears setGear = gear;

            // Check toggle state
            if (value)
            {
                HandleToggleOn(setGear, currentWeapon, currentArmor, currentBoot, currentHero, gearColor, gearCanvasGroup);
            }
            else
            {
                HandleToggleOff(gear, gearColor, gearCanvasGroup);
            }
        }

        void HandleToggleOn(Gears gear, Gears currentWeapon, Gears currentArmor, Gears currentBoot, Hero currentHero, Image gearColor, CanvasGroup gearCanvasGroup)
        {
            Debug.Log($"{gear.GetNameGears()} is toggled ON!");
            Gears setGear = gear;

            if (setGear == currentWeapon || setGear == currentArmor || setGear == currentBoot)
            {
                SetGearColorAndVisibility(gearColor, gearCanvasGroup, false);
            }
            else
            {
                SetGearColorAndVisibility(gearColor, gearCanvasGroup, true);
            }

            gearsPreview = gear;

            // Update preview status
            UpdatePreviewStatus(gearsPreview, currentHero);

            // Update color preview status for hero
            UpdateColorPreviewStatusHero();
        }

        void HandleToggleOff(Gears gear, Image gearColor, CanvasGroup gearCanvasGroup)
        {
            Debug.Log($"{gear.GetNameGears()} is toggled OFF!");
            SetGearColorAndVisibility(gearColor, gearCanvasGroup, false);
            UpdateAllDeselectColorStatusPreview();
        }

        void SetGearColorAndVisibility(Image gearColor, CanvasGroup gearCanvasGroup, bool isVisible)
        {
            gearColor.color = isVisible ? Color.white : new Color(1, 0, 0);
            ShowCanvasGroup(gearCanvasGroup, isVisible);
        }

        void UpdatePreviewStatus(Gears gearsPreview, Hero currentHero)
        {
            int gearPreviewHealthStatus = 0;
            int gearPreviewPhysicalAttackStatus = 0;
            int gearPreviewMagicAttackStatus = 0;
            int gearPreviewPhysicalDefenseStatus = 0;
            int gearPreviewMagicDefenseStatus = 0;
            int gearPreviewCriticalStatus = 0;
            int gearPreviewAgilityStatus = 0;
            int gearPreviewEvasionStatus = 0;


            switch (gearsPreview.GetGearsType())
            {
                case GearsType.Weapons:
                    gearPreviewHealthStatus = currentHero.GetHeroHealthStatusWithoutWeapon() + gearsPreview.GetStatusHealth();
                    gearPreviewPhysicalAttackStatus = currentHero.GetHeroPhysicalAttackStatusWithoutWeapon() + gearsPreview.GetStatusPhysicalAttack();
                    gearPreviewMagicAttackStatus = currentHero.GetHeroMagicAttackStatusWithoutWeapon() + gearsPreview.GetStatusMagicAttack();
                    gearPreviewPhysicalDefenseStatus = currentHero.GetHeroPhysicalDefenseStatusWithoutWeapon() + gearsPreview.GetStatusPhysicalDefense();
                    gearPreviewMagicDefenseStatus = currentHero.GetHeroMagicDefenseStatusWithoutWeapon() + gearsPreview.GetStatusMagicDefense();
                    gearPreviewCriticalStatus = currentHero.GetHeroCriticalStatusWithoutWeapon() + gearsPreview.GetStatusCritical();
                    gearPreviewAgilityStatus = currentHero.GetHeroAgilityStatusWithoutWeapon() + gearsPreview.GetStatusAgility();
                    gearPreviewEvasionStatus = currentHero.GetHeroEvasionStatusWithoutWeapon() + gearsPreview.GetStatusEvasion();
                    break;
                case GearsType.Armor:
                    gearPreviewHealthStatus = currentHero.GetHeroHealthStatusWithoutArmor() + gearsPreview.GetStatusHealth();
                    gearPreviewPhysicalAttackStatus = currentHero.GetHeroPhysicalAttackStatusWithoutArmor() + gearsPreview.GetStatusPhysicalAttack();
                    gearPreviewMagicAttackStatus = currentHero.GetHeroMagicAttackStatusWithoutArmor() + gearsPreview.GetStatusMagicAttack();
                    gearPreviewPhysicalDefenseStatus = currentHero.GetHeroPhysicalDefenseStatusWithoutArmor() + gearsPreview.GetStatusPhysicalDefense();
                    gearPreviewMagicDefenseStatus = currentHero.GetHeroMagicDefenseStatusWithoutArmor() + gearsPreview.GetStatusMagicDefense();
                    gearPreviewCriticalStatus = currentHero.GetHeroCriticalStatusWithoutArmor() + gearsPreview.GetStatusCritical();
                    gearPreviewAgilityStatus = currentHero.GetHeroAgilityStatusWithoutArmor() + gearsPreview.GetStatusAgility();
                    gearPreviewEvasionStatus = currentHero.GetHeroEvasionStatusWithoutArmor() + gearsPreview.GetStatusEvasion();
                    break;
                case GearsType.Boots:
                    gearPreviewHealthStatus = currentHero.GetHeroHealthStatusWithoutBoot() + gearsPreview.GetStatusHealth();
                    gearPreviewPhysicalAttackStatus = currentHero.GetHeroPhysicalAttackStatusWithoutBoot() + gearsPreview.GetStatusPhysicalAttack();
                    gearPreviewMagicAttackStatus = currentHero.GetHeroMagicAttackStatusWithoutBoot() + gearsPreview.GetStatusMagicAttack();
                    gearPreviewPhysicalDefenseStatus = currentHero.GetHeroPhysicalDefenseStatusWithoutBoot() + gearsPreview.GetStatusPhysicalDefense();
                    gearPreviewMagicDefenseStatus = currentHero.GetHeroMagicDefenseStatusWithoutBoot() + gearsPreview.GetStatusMagicDefense();
                    gearPreviewCriticalStatus = currentHero.GetHeroCriticalStatusWithoutBoot() + gearsPreview.GetStatusCritical();
                    gearPreviewAgilityStatus = currentHero.GetHeroAgilityStatusWithoutBoot() + gearsPreview.GetStatusAgility();
                    gearPreviewEvasionStatus = currentHero.GetHeroEvasionStatusWithoutBoot() + gearsPreview.GetStatusEvasion();
                    break;
            }

            health.text = gearPreviewHealthStatus.ToString();
            physicalAttack.text = gearPreviewPhysicalAttackStatus.ToString();
            magicAttack.text = gearPreviewMagicAttackStatus.ToString();
            physicalDefense.text = gearPreviewPhysicalDefenseStatus.ToString();
            magicDefense.text = gearPreviewMagicDefenseStatus.ToString();
            statusCritical.text = gearPreviewCriticalStatus.ToString();
            statusAgility.text = gearPreviewAgilityStatus.ToString();
            statusEvasion.text = gearPreviewEvasionStatus.ToString();
        }

        Hero GetHeroWithGear(Gears gear)
        {
            // Mencari pahlawan yang sedang dilengkapi dengan gear tertentu

            return null;

        }

        public void HideUnequipButton(Transform container)
        {
            for (int i = 0; i < container.childCount; i++)
            {
                var canvasGroup = container.GetChild(i).GetChild(4).GetComponent<CanvasGroup>();
                ShowCanvasGroup(canvasGroup, false);
            }
        }

        public void ShowCanvasGroup(CanvasGroup canvasGroup, bool status)
        {
            canvasGroup.alpha = status ? 1 : 0;
            canvasGroup.blocksRaycasts = status;
            canvasGroup.interactable = status;
        }

        public void UpdateAllDeselectColorStatusPreview()
        {
            UpdateDeselectColorStatusPreview(health);
            UpdateDeselectColorStatusPreview(physicalAttack);
            UpdateDeselectColorStatusPreview(magicAttack);
            UpdateDeselectColorStatusPreview(physicalDefense);
            UpdateDeselectColorStatusPreview(magicDefense);
            UpdateDeselectColorStatusPreview(statusCritical);
            UpdateDeselectColorStatusPreview(statusAgility);
            UpdateDeselectColorStatusPreview(statusEvasion);
        }

        private void UpdateDeselectColorStatusPreview(TextMeshProUGUI textPreviewStatus)
        {
            TextMeshProUGUI textSimbol = textPreviewStatus.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            textSimbol.text = ">";
            textPreviewStatus.text = "0";
            textPreviewStatus.color = Color.black;
            textPreviewStatus.fontStyle = FontStyles.Normal;
            textPreviewStatus.fontSize = 16;
        }

        public void UpdateColorPreviewStatusHero()
        {
            Hero hero = HeroList[GearsButtonManager.Instance.GetHeroIndex()];
            //HeroHealth
            int healthPreview = 0;
            int currentHealth = 0;
            //HeroPhysicalAttack
            int physicalAttackPreview = 0;
            int currentPhysicalAttack = 0;
            //HeroMagicAttack
            int magicAttackPreview = 0;
            int currentMagicAttack = 0;
            //HeroPhysicalDefense
            int physicalDefensePreview = 0;
            int currentPhysicalDefense = 0;
            //HeroMagicDefense
            int magicDefensePreview = 0;
            int currentMagicDefense = 0;
            //HeroCritical
            int criticalPreview = 0;
            int currentCritical = 0;
            //HeroAgility
            int agilityPreview = 0;
            int currentAgility = 0;
            //HeroEvasion
            int evasionPreview = 0;
            int currentEvasion = 0;

            switch (gearsPreview.GetGearsType())
            {
                case GearsType.Weapons:
                    //HeroHealth
                    healthPreview = gearsPreview.GetStatusHealth() + hero.GetHeroHealthStatusWithoutWeapon();
                    currentHealth = hero.GetCurrentWeapon().GetStatusHealth() + hero.GetHeroHealthStatusWithoutWeapon();

                    //HeroPhysicalAttack
                    physicalAttackPreview = gearsPreview.GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutWeapon();
                    currentPhysicalAttack = hero.GetCurrentWeapon().GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutWeapon();

                    //HeroMagicAttack
                    magicAttackPreview = gearsPreview.GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutWeapon();
                    currentMagicAttack = hero.GetCurrentWeapon().GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutWeapon();

                    //HeroPhysicalDefense
                    physicalDefensePreview = gearsPreview.GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutWeapon();
                    currentPhysicalDefense = hero.GetCurrentWeapon().GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutWeapon();

                    //HeroMagicDefense
                    magicDefensePreview = gearsPreview.GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutWeapon();
                    currentMagicDefense = hero.GetCurrentWeapon().GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutWeapon();

                    //HeroCritical
                    criticalPreview = gearsPreview.GetStatusCritical() + hero.GetHeroCriticalStatusWithoutWeapon();
                    currentCritical = hero.GetCurrentWeapon().GetStatusCritical() + hero.GetHeroCriticalStatusWithoutWeapon();

                    //HeroAgility
                    agilityPreview = gearsPreview.GetStatusAgility() + hero.GetHeroAgilityStatusWithoutWeapon();
                    currentAgility = hero.GetCurrentWeapon().GetStatusAgility() + hero.GetHeroAgilityStatusWithoutWeapon();

                    //HeroEvasion
                    evasionPreview = gearsPreview.GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutWeapon();
                    currentEvasion = hero.GetCurrentWeapon().GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutWeapon();

                    break;

                case GearsType.Armor:
                    //HeroHealth
                    healthPreview = gearsPreview.GetStatusHealth() + hero.GetHeroHealthStatusWithoutArmor();
                    currentHealth = hero.GetCurrentArmor().GetStatusHealth() + hero.GetHeroHealthStatusWithoutArmor();

                    //HeroPhysicalAttack
                    physicalAttackPreview = gearsPreview.GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutArmor();
                    currentPhysicalAttack = hero.GetCurrentArmor().GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutArmor();

                    //HeroMagicAttack
                    magicAttackPreview = gearsPreview.GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutArmor();
                    currentMagicAttack = hero.GetCurrentArmor().GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutArmor();

                    //HeroPhysicalDefense
                    physicalDefensePreview = gearsPreview.GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutArmor();
                    currentPhysicalDefense = hero.GetCurrentArmor().GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutArmor();

                    //HeroMagicDefense
                    magicDefensePreview = gearsPreview.GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutArmor();
                    currentMagicDefense = hero.GetCurrentArmor().GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutArmor();

                    //HeroCritical
                    criticalPreview = gearsPreview.GetStatusCritical() + hero.GetHeroCriticalStatusWithoutArmor();
                    currentCritical = hero.GetCurrentArmor().GetStatusCritical() + hero.GetHeroCriticalStatusWithoutArmor();

                    //HeroAgility
                    agilityPreview = gearsPreview.GetStatusAgility() + hero.GetHeroAgilityStatusWithoutArmor();
                    currentAgility = hero.GetCurrentArmor().GetStatusAgility() + hero.GetHeroAgilityStatusWithoutArmor();

                    //HeroEvasion
                    evasionPreview = gearsPreview.GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutArmor();
                    currentEvasion = hero.GetCurrentArmor().GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutArmor();

                    break;

                case GearsType.Boots:
                    //HeroHealth
                    healthPreview = gearsPreview.GetStatusHealth() + hero.GetHeroHealthStatusWithoutBoot();
                    currentHealth = hero.GetCurrentBoot().GetStatusHealth() + hero.GetHeroHealthStatusWithoutBoot();

                    //HeroPhysicalAttack
                    physicalAttackPreview = gearsPreview.GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutBoot();
                    currentPhysicalAttack = hero.GetCurrentBoot().GetStatusPhysicalAttack() + hero.GetHeroPhysicalAttackStatusWithoutBoot();

                    //HeroMagicAttack
                    magicAttackPreview = gearsPreview.GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutBoot();
                    currentMagicAttack = hero.GetCurrentBoot().GetStatusMagicAttack() + hero.GetHeroMagicAttackStatusWithoutBoot();

                    //HeroPhysicalDefense
                    physicalDefensePreview = gearsPreview.GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutBoot();
                    currentPhysicalDefense = hero.GetCurrentBoot().GetStatusPhysicalDefense() + hero.GetHeroPhysicalDefenseStatusWithoutBoot();

                    //HeroMagicDefense
                    magicDefensePreview = gearsPreview.GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutBoot();
                    currentMagicDefense = hero.GetCurrentBoot().GetStatusMagicDefense() + hero.GetHeroMagicDefenseStatusWithoutBoot();

                    //HeroCritical
                    criticalPreview = gearsPreview.GetStatusCritical() + hero.GetHeroCriticalStatusWithoutBoot();
                    currentCritical = hero.GetCurrentBoot().GetStatusCritical() + hero.GetHeroCriticalStatusWithoutBoot();

                    //HeroAgility
                    agilityPreview = gearsPreview.GetStatusAgility() + hero.GetHeroAgilityStatusWithoutBoot();
                    currentAgility = hero.GetCurrentBoot().GetStatusAgility() + hero.GetHeroAgilityStatusWithoutBoot();

                    //HeroEvasion
                    evasionPreview = gearsPreview.GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutBoot();
                    currentEvasion = hero.GetCurrentBoot().GetStatusEvasion() + hero.GetHeroEvasionStatusWithoutBoot();
                    break;

            }
            UpdatePreviewTextColorStatus(health, healthPreview, currentHealth);
            UpdatePreviewTextColorStatus(physicalAttack, physicalAttackPreview, currentPhysicalAttack);
            UpdatePreviewTextColorStatus(magicAttack, magicAttackPreview, currentMagicAttack);
            UpdatePreviewTextColorStatus(physicalDefense, physicalDefensePreview, currentPhysicalDefense);
            UpdatePreviewTextColorStatus(magicDefense, magicDefensePreview, currentMagicDefense);
            UpdatePreviewTextColorStatus(statusCritical, criticalPreview, currentCritical);
            UpdatePreviewTextColorStatus(statusAgility, agilityPreview, currentAgility);
            UpdatePreviewTextColorStatus(statusEvasion, evasionPreview, currentEvasion);



        }

        private void UpdatePreviewTextColorStatus(TextMeshProUGUI textStatus, int statusPreview, int currentStatus)
        {
            TextMeshProUGUI textSimbol = textStatus.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
            Color TextColor;

            if (statusPreview > currentStatus)
            {
                TextColor = new Color(0, 1, 0);
                textStatus.fontStyle = FontStyles.Bold;
                textStatus.fontSize = 20;
                textSimbol.text = "<";
            }
            else if (statusPreview < currentStatus)
            {
                TextColor = new Color(1, 0, 0);
                textStatus.fontStyle = FontStyles.Bold;
                textStatus.fontSize = 20;
                textSimbol.text = ">";
            }
            else
            {
                TextColor = new Color(0, 0, 0);
                textStatus.fontStyle = FontStyles.Normal;
                textStatus.fontSize = 16;
                textSimbol.text = "=";
            }
            textStatus.color = TextColor;

        }

        public Hero GetHero(int index)
        {
            return HeroList[index];
        }

        #region GetSet
        public Sprite GearsImage(int index)
        {
            if (index >= 0 && index < gearList.Count)
            {
                Gears currentGears = gearList[index];
                if (currentGears != null && currentGears.GetImageGear() != null)
                {
                    Sprite gearImage = currentGears.GetImageGear();
                    return gearImage;
                }
            }
            return null;
        }

        public Sprite GetHeroImage(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetImageHero() != null)
                {
                    Sprite image = currentHero.GetImageHero(); // Mengakses objek Image
                    return image;
                }
            }
            return null;
        }

        public string GetFullNameHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetFullNameHero() != null)
                {
                    string name = currentHero.GetFullNameHero();
                    FullNameHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public void SetAllHeroNames()
        {
            for (int i = 0; i < HeroList.Count; i++)
            {
                Hero currentHero = HeroList[i];
                if (currentHero != null)
                {
                    string name = currentHero.GetNickNameHero();
                    NickNameHeroes[i].text = name;
                }
            }
        }

        public int GetStatusHealthHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetHealthHero = currentHero.GetHeroHealth();
                    return GetHealthHero;
                }
            }
            return 0;
        }

        public int GetStatusPhysicalAttackHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetPhysicalAttackHero = currentHero.GetPhysicalAttackHero();
                    return GetPhysicalAttackHero;
                }
            }
            return 0;
        }

        public int GetStatusMagicAttackHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetMagicAttackHero = currentHero.GetMagicAttackHero();
                    return GetMagicAttackHero;
                }
            }
            return 0;
        }

        public int GetStatusPhysicalDefenseHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetPhysicalDefenseHero = currentHero.GetPhysicalDefenseHero();
                    return GetPhysicalDefenseHero;
                }
            }
            return 0;
        }

        public int GetStatusMagicDefenseHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetMagicDefenseHero = currentHero.GetMagicDefenseHero();
                    return GetMagicDefenseHero;
                }
            }
            return 0;
        }

        public int GetStatusCriticalHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetCriticalHero = currentHero.GetCriticalHero();
                    return GetCriticalHero;
                }
            }
            return 0;
        }

        public int GetStatusAgilityHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetAgilityHero = currentHero.GetAgilityHero();
                    return GetAgilityHero;
                }
            }
            return 0;
        }

        public int GetStatusEvasionHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null)
                {
                    int GetEvasionHero = currentHero.GetEvasionHero();
                    return GetEvasionHero;
                }
            }
            return 0;
        }

        public Sprite GetIconGear(int index)
        {
            if (index >= 0 && index < gearList.Count)
            {
                Gears currentGearsImage = gearList[index];
                if (currentGearsImage != null && currentGearsImage.GetImageGear() != null)
                {
                    Sprite image = currentGearsImage.GetImageGear();
                    return image;
                }
            }
            return null;
        }

        public List<Gears> GetGearList() => gearList;

        /* void RemoveGearsLog()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }
        } */
        #endregion
    }

}