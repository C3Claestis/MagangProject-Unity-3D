namespace Nivandria.UI.Gears
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;


    public class GearsLogManager : MonoBehaviour
    {
        public static GearsLogManager Instance { get; private set; }

        [Header("Content Container")]
        [SerializeField] Transform contentContainer;

        [Header("GearsType")]
        [SerializeField] public GearsType gearsType;
        [SerializeField] GearsType currentGearsType;

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

        [Header("Heroes List")]
        [SerializeField] List<Hero> HeroList = new List<Hero>();

        [Header("Gears List")]
        [SerializeField] List<Gears> gearList = new List<Gears>();
        [SerializeField] GameObject gearsLog;

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

        public void SetInitialHeroName()
        {
            if (HeroList.Count >= 0)
            {
                string initialHeroName = GetFullNameHero(0); // Mendapatkan nama pahlawan dari indeks pertama
                FullNameHero.text = initialHeroName; // Mengatur variabel nameHero dengan nama pahlawan pertama
            }
        }

        public void GearsLogInitialization()
        {
            int index = 0;

            foreach (Gears gears in gearList)
            {
                if (!(gears.GetGearsType() == gearsType)) continue;
                index += 1;
                GameObject newGear = Instantiate(gearsLog, contentContainer);
                Image iconGear = GetIconGear(newGear);
                TextMeshProUGUI gearName = newGear.GetComponentInChildren<TextMeshProUGUI>();
                Button gearButton = newGear.GetComponent<Button>();
                Gears currentGears = gears;

                gearButton.onClick.AddListener(() =>
                {
                    if (nameGear != null)
                    {
                        health.text = gears.GetStatusHealth();
                        physicalAttack.text = gears.GetStatusPhysicalAttack();
                        magicAttack.text = gears.GetStatusMagicAttack();
                        physicalDefense.text = gears.GetStatusPhysicalDefense();
                        magicDefense.text = gears.GetStatusMagicDefense();
                        statusCritical.text = gears.GetStatusCritical();
                        statusAgility.text = gears.GetStatusAgility();
                        statusEvasion.text = gears.GetStatusEvasion();
                    }
                }
                );
            }
        }


        private Image GetIconGear(GameObject newGear)
        {
            for (int i = 0; i < newGear.transform.childCount; i++)
            {
                Transform iconTransform = newGear.transform.GetChild(i);
                if (iconTransform.GetComponent<Image>())
                {
                    return iconTransform.GetComponent<Image>();
                }
            }
            return null;
        }

        void RemoveGearsLog()
        {
            foreach (Transform child in contentContainer)
            {
                Destroy(gameObject);
            }
        }

    }

}