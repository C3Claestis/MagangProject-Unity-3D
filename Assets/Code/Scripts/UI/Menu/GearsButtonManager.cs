namespace Nivandria.UI.Gears
{
    using UnityEngine;
    using UnityEngine.UI;

    public class GearsButtonManager : MonoBehaviour
    {
        public static GearsButtonManager Instance { get; private set; }

        GearsLogManager gearsLogManager;
        //private int currentIndex = 0;

        [Header("Hero Image")]
        [SerializeField] private Image image;

        [Header("Button Image")]
        [SerializeField] private Image hero1;
        [SerializeField] private Image hero2;
        [SerializeField] private Image hero3;
        [SerializeField] private Image hero4;


        private Color activeColor = new Color(0.16f, 0.58f, 0.70f); // Warna 2995B2
        private Color inactiveColor = new Color(0.93f, 0.13f, 0.40f); // Warna EE3166

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
                if (image != null)
                {
                    image.sprite = sprite; // Menetapkan Sprite ke Image
                }
            }
        }

        public void GetDescriptionHero(int index)
        {
            string heroName = GearsLogManager.Instance.GetFullNameHero(index);
            string healthHero = GearsLogManager.Instance.GetStatusHealthHero(index);
            string physicalAttackHero = GearsLogManager.Instance.GetStatusPhysicalAttackHero(index);
            string magicAttackHero = GearsLogManager.Instance.GetStatusMagicAttackHero(index);
            string physicalDefenseHero = GearsLogManager.Instance.GetStatusPhysicalDefenseHero(index);
            string magicDefenseHero = GearsLogManager.Instance.GetStatusMagicDefenseHero(index);
            string statusCriticalHero = GearsLogManager.Instance.GetStatusCriticalHero(index);
            string statusAgilityHero = GearsLogManager.Instance.GetStatusAgilityHero(index);
            string statusEvasionHero = GearsLogManager.Instance.GetStatusEvasionHero(index);
        }

        public void ButtonOnClick(int index)
        {
            GetHeroImage(index);
            GetDescriptionHero(index);
        }

        public void SetFirst()
        {
            UpdateButtonColors(1);
            HeroDescription();
            NameHero();
        }

        public void HeroDescription()
        {
            if (gearsLogManager != null)
            {
                gearsLogManager.SetInitialHeroDescription();
            }
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