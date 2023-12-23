namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class QuestButtonManager : MonoBehaviour
    {
        public static QuestButtonManager Instance { get; private set; }

        [Header("Content Container All Quest")]
        [SerializeField] GameObject AllQuestContentContainer;

        [Header("Content Container Main Quest")]
        [SerializeField] GameObject MainQuestContentContainer;

        [Header("Content Container Side Quest & Commission")]
        [SerializeField] GameObject QuestContentContainer;

        [Header("Image Button")]
        [SerializeField] private Image _AllQuest;
        [SerializeField] private Image _MainQuest;
        [SerializeField] private Image _SideQuest;
        [SerializeField] private Image _Commission;

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

        void Start()
        {
            SetAllQuestFirst();
        }

        public void ChangeContentContainerQuest(int containerNumber)
        {
            AllQuestContentContainer.SetActive(false);
            MainQuestContentContainer.SetActive(false);
            QuestContentContainer.SetActive(false);

            switch (containerNumber)
            {
                case 1:
                    AllQuestContentContainer.SetActive(true);
                    break;
                case 2:
                    MainQuestContentContainer.SetActive(true);
                    break;
                case 3:
                    QuestContentContainer.SetActive(true);
                    QuestLogManager.Instance.questType = QuestType.Side;
                    break;
                case 4:
                    QuestContentContainer.SetActive(true);
                    QuestLogManager.Instance.questType = QuestType.Commission;
                    break;
                default:
                    Debug.Log("Panel number out of range.");
                    break;
            }

            UpdateButtonColors(containerNumber);
        }

        public void SetAllQuestFirst()
        {
            AllQuestContentContainer.SetActive(true);
            MainQuestContentContainer.SetActive(false);
            QuestContentContainer.SetActive(false);
            UpdateButtonColors(1);
        }

        private void UpdateButtonColors(int activePanelNumber)
        {
            // Mengatur warna tombol sesuai dengan panel yang aktif
            _AllQuest.GetComponent<Image>().color = (activePanelNumber == 1) ? activeColor : inactiveColor;
            _MainQuest.GetComponent<Image>().color = (activePanelNumber == 2) ? activeColor : inactiveColor;
            _SideQuest.GetComponent<Image>().color = (activePanelNumber == 3) ? activeColor : inactiveColor;
            _Commission.GetComponent<Image>().color = (activePanelNumber == 4) ? activeColor : inactiveColor;
        }
    }

}