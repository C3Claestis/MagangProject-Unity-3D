namespace Nivandria.UI.Gears
{
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

        [Header("Detail Gears")]
        private TextMeshProUGUI nameGear;

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
            GearsLogInitialization(GearsType.Weapons);
            GearsLogInitialization(GearsType.Armor);
            GearsLogInitialization(GearsType.Boots);
        }

        void Update()
        {
            if (gearsType == currentGearsType) return;
            currentGearsType = gearsType;
            /* RemoveGearsLog(); */

        }



        public void GearsLogInitialization(GearsType gearsType)
        {
            foreach (Gears gear in gearList)
            {
                if (!(gear.GetGearsType() == gearsType)) continue;


                GameObject newGear = null;
                switch (gear.GetGearsType())
                {
                    case GearsType.Weapons:
                        newGear = Instantiate(gearsLog, WeaponContentContainer);
                        break;
                    case GearsType.Armor:
                        newGear = Instantiate(gearsLog, ArmorContentContainer);
                        break;
                    case GearsType.Boots:
                        newGear = Instantiate(gearsLog, BootContentContainer);
                        break;
                }


                //Gears currentGear = gears;
                Image iconArmor = newGear.transform.GetChild(0).GetComponent<Image>();
                iconArmor.sprite = gear.GetImageGear();

                TextMeshProUGUI gearName = newGear.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                gearName.text = gear.GetNameGears();

                GameObject gearEquipped = newGear.transform.GetChild(3).gameObject;
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
                        Debug.LogError("Gear tidak diketahuin");
                        break;
                }
                int heroIndex = GearsButtonManager.Instance.GetHeroIndex();
                Hero hero = HeroList[heroIndex];

                // Tambahkan event listener untuk menangani perubahan nilai toggle
                toggle.onValueChanged.AddListener((value) =>
                {
                    // Ketika nilai toggle berubah
                    Debug.Log($"{gear.GetNameGears()} Toggle isOn: {value}");

                    // Lakukan sesuatu berdasarkan nilai toggle
                    if (value)
                    {
                        Debug.Log($"{gear.GetNameGears()} is toggled ON!");
                        gearEquipped.SetActive(true);
                        gearsPreview = gear;

                        health.text = (int.Parse(gearsPreview.GetStatusHealth()) + int.Parse(hero.GetHeroHealth())).ToString();
                        physicalAttack.text = (int.Parse(gearsPreview.GetStatusPhysicalAttack()) + int.Parse(hero.GetPhysicalAttackHero())).ToString();
                        magicAttack.text = (int.Parse(gearsPreview.GetStatusMagicAttack()) + int.Parse(hero.GetMagicAttackHero())).ToString();
                        physicalDefense.text = (int.Parse(gearsPreview.GetStatusPhysicalDefense()) + int.Parse(hero.GetPhysicalDefenseHero())).ToString();
                        magicDefense.text = (int.Parse(gearsPreview.GetStatusMagicDefense()) + int.Parse(hero.GetMagicDefenseHero())).ToString();
                        statusCritical.text = (int.Parse(gearsPreview.GetStatusCritical()) + int.Parse(hero.GetCriticalHero())).ToString();
                        statusAgility.text = (int.Parse(gearsPreview.GetStatusAgility()) + int.Parse(hero.GetAgilityHero())).ToString();
                        statusEvasion.text = (int.Parse(gearsPreview.GetStatusEvasion()) + int.Parse(hero.GetEvasionHero())).ToString();

                    }
                    else
                    {
                        Debug.Log($"{gear.GetNameGears()} is toggled OFF!");
                        gearEquipped.SetActive(false);

                        health.text = "0";
                        physicalAttack.text = "0";
                        magicAttack.text = "0";
                        physicalDefense.text = "0";
                        magicDefense.text = "0";
                        statusCritical.text = "0";
                        statusAgility.text = "0";
                        statusEvasion.text = "0";
                    }

                    TextMeshProUGUI currentHealthHero = healthHero;

                    gearButton.onClick.AddListener(() =>
                    {
                        Debug.Log("Tes ini Button GearEquipped");
                        Gears currentWeapon = hero.GetCurrentWeapon();
                        Gears currentArmor = hero.GetCurrentArmor();
                        Gears currentBoot = hero.GetCurrentBoot();

                        switch (gear.GetGearsType())
                        {
                            case GearsType.Weapons:
                                hero.SetCurrentSword(gear);
                                break;
                            case GearsType.Armor:
                                hero.SetCurrentArmor(gear);
                                break;
                            case GearsType.Boots:
                                hero.SetCurrentBoot(gear);
                                break;
                        }
                    });

                });
            }
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

        public string GetStatusHealthHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetHeroHealth() != null)
                {
                    string name = currentHero.GetHeroHealth();
                    healthHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusPhysicalAttackHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetPhysicalAttackHero() != null)
                {
                    string name = currentHero.GetPhysicalAttackHero();
                    physicalAttackHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusMagicAttackHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetMagicAttackHero() != null)
                {
                    string name = currentHero.GetMagicAttackHero();
                    magicAttackHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusPhysicalDefenseHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetPhysicalDefenseHero() != null)
                {
                    string name = currentHero.GetPhysicalDefenseHero();
                    physicalDefenseHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusMagicDefenseHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetMagicDefenseHero() != null)
                {
                    string name = currentHero.GetMagicDefenseHero();
                    magicDefenseHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusCriticalHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetCriticalHero() != null)
                {
                    string name = currentHero.GetCriticalHero();
                    statusCriticalHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusAgilityHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetAgilityHero() != null)
                {
                    string name = currentHero.GetAgilityHero();
                    statusAgilityHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
        }

        public string GetStatusEvasionHero(int index)
        {
            if (index >= 0 && index < HeroList.Count)
            {
                Hero currentHero = HeroList[index];
                if (currentHero != null && currentHero.GetEvasionHero() != null)
                {
                    string name = currentHero.GetEvasionHero();
                    statusEvasionHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
                    return name;
                }
            }
            return "";
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