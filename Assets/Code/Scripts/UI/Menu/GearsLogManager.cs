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

        [Header("Content Container")]
        [SerializeField] Transform contentContainer;

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

        private Image gearsImage;

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

        void Update()
        {
            if (gearsType == currentGearsType) return;
            currentGearsType = gearsType;
            RemoveGearsLog();
            GearsLogInitialization();
        }

        public void GearsLogInitialization()
        {
            foreach (Gears gears in gearList)
            {
                if (!(gears.GetGearsType() == gearsType)) continue;

                
                GameObject newGear = Instantiate(gearsLog, contentContainer);
                
                //Gears currentGear = gears;
                //Button gearButton = newGear.GetComponent<Button>();
                Image iconArmor = newGear.transform.GetChild(0).GetComponent<Image>();
                iconArmor.sprite = gears.GetImageGear();
                
                TextMeshProUGUI gearName = newGear.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
                gearName.text = gears.GetNameGears();
                

            }
        }

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
                    statusAgilityHero.text = name; // Mengisi TextMeshProUGUI yang sesuai
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

        void RemoveGearsLog()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(child.gameObject);
            }
        }

    }

}