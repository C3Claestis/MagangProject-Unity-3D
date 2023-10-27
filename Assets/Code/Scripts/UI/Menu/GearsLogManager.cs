namespace Nivandria.UI.Gears
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;


    public class GearsLogManager : MonoBehaviour
    {
        public static GearsLogManager Instance{get; private set;}
        [SerializeField] Transform contentContainer;

        [SerializeField] GearsType gearsType = GearsType.Weapons;
        [SerializeField] GearsType currentGearsType;


        /*
        [Header("Detail Hero")]
        [SerializeField] TextMeshProUGUI nameHero;
        [SerializeField] GameObject imageHero;
        [SerializeField] TextMeshProUGUI descriptionHero;
        */

        //[SerializeField] Image imageGear;
        [Header("Detail Gears")]
        //[SerializeField] Image iconGear;
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



        [SerializeField] List<Gears> gearList = new List<Gears>();
        //[SerializeField] List<Hero> heroList = new List<Hero>();
        //[SerializeField] List<Status> statusList = new List<Status>();
        [SerializeField] GameObject gearsLog;

        void Awake()
        {
            if(Instance != null && Instance != this)
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