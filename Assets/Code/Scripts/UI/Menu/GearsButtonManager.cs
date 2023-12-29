namespace Nivandria.UI.Gears
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class GearsButtonManager : MonoBehaviour
    {
        public static GearsButtonManager Instance { get; private set; }

        GearsLogManager gearsLogManager;

        [Header("Content Container Gears")]
        [SerializeField] GameObject WeaponContentContainer;
        [SerializeField] GameObject ArmorContentContainer;
        [SerializeField] GameObject BootsContentContainer;

        [Header("Hero Image")]
        [SerializeField] private Image heroPictures;

        [Header("Button Image")]
        [SerializeField] private Image hero1;
        [SerializeField] private Image hero2;
        [SerializeField] private Image hero3;
        [SerializeField] private Image hero4;

        [Header("Gears Name")]
        [SerializeField] private TextMeshProUGUI titleGears;

        [Header("Toggles")]
        [SerializeField] private Toggle[] toggleArray;

        private int currentGearIndex;
        private int currentHeroIndex;
        private int maxIndex = 2;

        public int GetHeroIndex() => currentHeroIndex;


        private Color activeColor = new Color(1f, 0.9411765f, 0.7843137f); // Warna FFF0C8
        private Color inactiveColor = new Color(0.8784314f, 0.8470588f, 0.772549f); // Warna E0D8C5

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
        private void Start()
        {
            gearsLogManager = FindObjectOfType<GearsLogManager>();
            SetFirst();
            currentGearIndex = 0;
            ChangeTypeGears(currentGearIndex);
        }


        public void ChangeTypeGears(int index)
        {
            WeaponContentContainer.SetActive(false);
            ArmorContentContainer.SetActive(false);
            BootsContentContainer.SetActive(false);

            Debug.Log("ini index: " + index);
            switch (index)
            {
                case 0:
                    WeaponContentContainer.SetActive(true);
                    titleGears.text = "Weapon";
                    Debug.Log("Ini Weapon");
                    break;
                case 1:
                    ArmorContentContainer.SetActive(true);
                    titleGears.text = "Armor";
                    Debug.Log("Ini Armor");
                    break;
                case 2:
                    BootsContentContainer.SetActive(true);
                    titleGears.text = "Boots";
                    Debug.Log("Ini Boots");
                    break;
                default:
                    Debug.Log("Tidak ada type yang sesuai");
                    break;
            }
            toggleArray[index].isOn = true;
            GearsLogManager.Instance.DeselectToggles();
            GearsLogManager.Instance.UpdateAllDeselectColorStatusPreview();
        }

        public void OnLeftButtonClick()
        {
            currentGearIndex--;
            if (currentGearIndex < 0)
            {
                currentGearIndex = maxIndex;
            }
            ChangeTypeGears(currentGearIndex);
        }

        public void OnRightButtonClick()
        {
            currentGearIndex++;
            if (currentGearIndex > maxIndex)
            {
                currentGearIndex = 0;
            }
            ChangeTypeGears(currentGearIndex);
        }

        public void ChangeCharacterDescription(int index)
        {
            switch (index)
            {
                case 1:
                    ButtonOnClick(0);
                    Debug.Log("Index 0");
                    break;
                case 2:
                    ButtonOnClick(1);
                    Debug.Log("Index 1");
                    break;
                case 3:
                    ButtonOnClick(2);
                    Debug.Log("Index 2");
                    break;
                case 4:
                    ButtonOnClick(3);
                    Debug.Log("Index 3");
                    break;
                default:
                    Debug.Log("Panel number out of range.");
                    break;
            }

            UpdateButtonColors(index);
        }

        private void UpdateButtonColors(int activePanelNumber)
        {
            // Mengatur warna tombol sesuai dengan panel yang aktif
            hero1.GetComponent<Image>().color = (activePanelNumber == 1) ? activeColor : inactiveColor;
            hero2.GetComponent<Image>().color = (activePanelNumber == 2) ? activeColor : inactiveColor;
            hero3.GetComponent<Image>().color = (activePanelNumber == 3) ? activeColor : inactiveColor;
            hero4.GetComponent<Image>().color = (activePanelNumber == 4) ? activeColor : inactiveColor;
        }

        public void GetHeroImage(int index)
        {
            Sprite sprite = GearsLogManager.Instance.GetHeroImage(index);

            if (sprite != null)
            {
                if (heroPictures != null)
                {
                    heroPictures.sprite = sprite; // Menetapkan Sprite ke Image
                }
            }
        }

        public void GetGearsImage(int index)
        {
            Sprite sprite = GearsLogManager.Instance.GearsImage(index);

            if (sprite != null)
            {
                if (heroPictures != null)
                {
                    heroPictures.sprite = sprite;
                }
            }
        }

        public void UpdateStatusHero(int index)
        {
            currentHeroIndex = index;
            Hero hero = GearsLogManager.Instance.GetHero(index);

            string heroName = hero.GetFullNameHero();

            int heroHealth = hero.GetHeroHealth()
            + hero.GetCurrentWeapon().GetStatusHealth()
            + hero.GetCurrentArmor().GetStatusHealth()
            + hero.GetCurrentBoot().GetStatusHealth();

            int heroPhysicalAttack = hero.GetPhysicalAttackHero() 
            + hero.GetCurrentWeapon().GetStatusPhysicalAttack()
            + hero.GetCurrentArmor().GetStatusPhysicalAttack()
            + hero.GetCurrentBoot().GetStatusPhysicalAttack();

            int heroMagicAttack = hero.GetMagicAttackHero() 
            + hero.GetCurrentWeapon().GetStatusMagicAttack()
            + hero.GetCurrentArmor().GetStatusMagicAttack()
            + hero.GetCurrentBoot().GetStatusMagicAttack();

            int heroPhysicalDefense = hero.GetPhysicalDefenseHero() 
            + hero.GetCurrentWeapon().GetStatusPhysicalDefense()
            + hero.GetCurrentArmor().GetStatusPhysicalDefense()
            + hero.GetCurrentBoot().GetStatusPhysicalDefense();

            int heroMagicDefense = hero.GetMagicDefenseHero() 
            + hero.GetCurrentWeapon().GetStatusMagicDefense()
            + hero.GetCurrentArmor().GetStatusMagicDefense()
            + hero.GetCurrentBoot().GetStatusMagicDefense();

            int heroCritical = hero.GetCriticalHero() 
            + hero.GetCurrentWeapon().GetStatusCritical()
            + hero.GetCurrentArmor().GetStatusCritical()
            + hero.GetCurrentBoot().GetStatusCritical();

            int heroAgility = hero.GetAgilityHero() 
            + hero.GetCurrentWeapon().GetStatusAgility()
            + hero.GetCurrentArmor().GetStatusAgility()
            + hero.GetCurrentBoot().GetStatusAgility();

            int heroEvasion = hero.GetEvasionHero() 
            + hero.GetCurrentWeapon().GetStatusEvasion()
            + hero.GetCurrentArmor().GetStatusEvasion()
            + hero.GetCurrentBoot().GetStatusEvasion();

            GearsLogManager.Instance.UpdateStatusHeroText(
                heroName,
                heroHealth,
                heroPhysicalAttack,
                heroMagicAttack,
                heroPhysicalDefense,
                heroMagicDefense,
                heroCritical,
                heroAgility,
                heroEvasion
                );
        }

        public void ButtonOnClick(int index)
        {
            GetHeroImage(index);
            UpdateStatusHero(index);
            GearsLogManager.Instance.GearsLogInitialization();
            GearsLogManager.Instance.UpdateAllDeselectColorStatusPreview();
        }

        public void SetFirst()
        {
            GearsLogManager.Instance.GearsLogInitialization();
            UpdateButtonColors(1);
            HeroDescription();
            NameHero();
            GetHeroImage(0);
        }

        public void HeroDescription()
        {
            InitializeFirstHero();
        }

        private void InitializeFirstHero()
        {
            UpdateStatusHero(0);
        }

        public void NameHero()
        {
            if (gearsLogManager != null)
            {
                gearsLogManager.SetAllHeroNames();
            }
        }
    }
}