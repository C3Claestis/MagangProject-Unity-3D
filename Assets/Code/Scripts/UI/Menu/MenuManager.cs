namespace Nivandria.UI.Menu
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        [Header("Content Container")]
        [SerializeField] public Transform MenuContentContainer;

        [Header("List Menu Button")]
        [SerializeField] List<Menu> MenuButtonList = new List<Menu>();

        [Header("Menu Button Log")]
        [SerializeField] GameObject menuButtonLog;

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
            MenuButtonLogInitialization();
        }

        public void MenuButtonLogInitialization()
        {
            foreach (Menu menu in MenuButtonList)
            {
                GameObject newMenuButton = Instantiate(menuButtonLog, MenuContentContainer);
                SetupMenuButton(newMenuButton, menu);
            }
        }

        private void SetupMenuButton(GameObject newMenuButton, Menu menu)
        {
            // Retrieve UI Components
            TextMeshProUGUI nameButton = newMenuButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();
            nameButton.text = menu.GetNameButton();

            Image iconLeftButton = newMenuButton.transform.GetChild(0).GetComponent<Image>();
            iconLeftButton.sprite = menu.GetIconLeftButton();

            Image iconRightButton = newMenuButton.transform.GetChild(1).GetComponent<Image>();
            iconRightButton.sprite = menu.GetIconLeftButton();

            // Set size and position icon based on the button name
            SetButtonPropertiesBasedOnName(newMenuButton, "ITEM", 33.97f, 45.49f, Vector2.zero, Vector2.zero);
            SetButtonPropertiesBasedOnName(newMenuButton, "SAVE GAME", 42.68f, 45f, Vector2.zero, Vector2.zero);
            SetButtonPropertiesBasedOnName(newMenuButton, "ACHIEVEMENT", 30.73f, 45f, new Vector2(-10f, 0f), new Vector2(10f, 0f));
            SetButtonPropertiesBasedOnName(newMenuButton, "SETTINGS", 46.77f, 45f, Vector2.zero, Vector2.zero);
            SetButtonPropertiesBasedOnName(newMenuButton, "ARCHIEVE", 57.93f, 45f, new Vector2(10f, 0f), new Vector2(-10f, 0f));
            SetButtonPropertiesBasedOnName(newMenuButton, "MAIN MENU", 47.72f, 45f, Vector2.zero, Vector2.zero);
        }

        private void SetIconButtonSize(Transform buttonTransform, float width, float height)
        {
            RectTransform rectTransform = buttonTransform.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(width, height);
            }
        }

        private void SetIconButtonPosition(Transform buttonTransform, Vector2 offset)
        {
            RectTransform rectTransform = buttonTransform.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = offset;
            }
        }

        private void SetButtonPropertiesBasedOnName(GameObject newMenuButton, string targetName, float width, float height, Vector2 offsetLeft, Vector2 offsetRight)
        {
            TextMeshProUGUI nameButton = newMenuButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

            if (nameButton.text.Trim().ToUpper() == targetName)
            {
                // Set size for both left and right icon
                SetIconButtonSize(newMenuButton.transform.GetChild(0), width, height);
                SetIconButtonSize(newMenuButton.transform.GetChild(1), width, height);

                // Set position for left and right icon
                SetIconButtonPosition(newMenuButton.transform.GetChild(0), offsetLeft);
                SetIconButtonPosition(newMenuButton.transform.GetChild(1), offsetRight);
            }
        }
    }

}