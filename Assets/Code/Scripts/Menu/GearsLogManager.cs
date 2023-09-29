namespace Nivandria.UI.Gears
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;


    public class GearsLogManager : MonoBehaviour
    {
        [SerializeField] Transform contentContainer;


        /*
        [Header("Detail Hero")]
        [SerializeField] TextMeshProUGUI nameHero;
        [SerializeField] GameObject imageHero;
        [SerializeField] TextMeshProUGUI descriptionHero;
        */

        //[SerializeField] Image imageGear;
        [Header("Detail Gears")]
        //[SerializeField] Image iconGear;
        [SerializeField] TextMeshProUGUI nameGear;
        [SerializeField] TextMeshProUGUI descriptionGear;


        [Header("Detail Status")]
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

        void Start()
        {

            GearsLogInitialization();
        }


        public void GearsLogInitialization()
        {
            int index = 0;

            foreach (Gears gears in gearList)
            {
                index += 1;
                GameObject newGear = Instantiate(gearsLog, contentContainer);
                Image iconGear = GetIconGear(newGear);
                TextMeshProUGUI gearName = newGear.GetComponentInChildren<TextMeshProUGUI>();

                Gears currentGears = gears;

                Button gearButton = newGear.GetComponent<Button>();
                gearButton.onClick.AddListener(() =>
                {
                    if (nameGear != null && descriptionGear != null)
                    {
                        nameGear.text = gears.GetNameGears();
                        descriptionGear.text = gears.GetDescriptionGear();
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
                SetGearIcon(iconGear, index, gears);
                SetGearName(gearName, index, gears);
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
        private void SetGearIcon(Image iconGear, int index, Gears gears)
        {
            iconGear.sprite = gears.GetImageGear();
        }
        private void SetGearName(TextMeshProUGUI gearName, int index, Gears gears)
        {
            gearName.text = gears.GetNameGears();
        }

    }

}