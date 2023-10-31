namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class QuestLogManager : MonoBehaviour
    {
        public static QuestLogManager Instance { get; private set; }

        [Header("Content Container All Quest")]
        [SerializeField] Transform MainQuestContentContainer;
        [SerializeField] Transform SideQuestContentContainer;
        [SerializeField] Transform CommissionsContentContainer;

        [Header("Content Container All Chapter Main Quest")]
        [SerializeField] Transform Chapter0ContentContainer;
        [SerializeField] Transform Chapter1ContentContainer;
        [SerializeField] Transform Chapter2ContentContainer;
        [SerializeField] Transform Chapter3ContentContainer;

        [Header("Contetn Container Side Quest & Commission")]
        [SerializeField] Transform QuestContentContainer;

        [Header("Text")]
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI giver;
        [SerializeField] TextMeshProUGUI location;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI objective;
        [SerializeField] TextMeshProUGUI reward;

        [Header("Quest Type")]
        [SerializeField] public QuestType questType;
        [SerializeField] QuestType currentQuestType;

        [Header("Quest Chapter")]
        [SerializeField] public QuestChapter questChapter;
        [SerializeField] QuestChapter currentQuestChapter;

        [SerializeField] List<Quest> questList = new List<Quest>();
        [SerializeField] GameObject questLog;

        private GameObject selectedQuestObject = null;

        bool firstQuest;

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
        // Start is called before the first frame update
        void Start()
        {
            QuestLogInitializationAllQuest(QuestType.Main);
            QuestLogInitializationAllQuest(QuestType.Side);
            QuestLogInitializationAllQuest(QuestType.Commission);
            QuestLogInitializationMainQuest(QuestChapter.Chapter_0);
            QuestLogInitializationMainQuest(QuestChapter.Chapter_1);
            QuestLogInitializationMainQuest(QuestChapter.Chapter_2);
            QuestLogInitializationMainQuest(QuestChapter.Chapter_3);
            QuestLogInitializationQuest();
        }

        void Update()
        {
            if (questType == currentQuestType) return;
            currentQuestType = questType;
            RemoveQuestLog();
            QuestLogInitializationQuest();
        }

        public void QuestLogInitializationAllQuest(QuestType questType)
        {
            int index = 1;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Quest quest in questList)
            {
                if (quest.GetQuestType() != questType) continue;

                GameObject newQuest = null;
                switch (quest.GetQuestType())
                {
                    case QuestType.Main:
                        newQuest = Instantiate(questLog, MainQuestContentContainer);
                        break;
                    case QuestType.Side:
                        newQuest = Instantiate(questLog, SideQuestContentContainer);
                        break;
                    case QuestType.Commission:
                        newQuest = Instantiate(questLog, CommissionsContentContainer);
                        break;
                }
                Button questButton = newQuest.GetComponent<Button>();
                Quest currentQuest = quest;
                TextMeshProUGUI questText = newQuest.GetComponentInChildren<TextMeshProUGUI>();
                Image questImage = newQuest.GetComponent<Image>();
                questButton.onClick.AddListener(() =>
                {
                    if (selectedQuestObject != null)
                    {
                        // Matikan game object yang dipilih sebelumnya
                        selectedQuestObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                        selectedQuestObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle &= ~FontStyles.Bold;
                    }

                    // Aktifkan game object yang baru
                    SetDescription(currentQuest);
                    questImage.color = new Color(1f, 1f, 1f, 1f);
                    questText.fontStyle |= FontStyles.Bold;

                    selectedQuestObject = newQuest; // Atur game object yang baru sebagai yang dipilih
                });

                string questTitle = quest.GetTitle();
                int maxTitleLenght = 25;
                if (questTitle.Length > maxTitleLenght)
                {
                    questTitle = questTitle.Substring(0, maxTitleLenght) + "...";
                }
                newQuest.GetComponent<QuestLog>().SetNameQuestLog($" {index}. " + questTitle);

                if (!firstQuest)
                {
                    questImage.color = new Color(1f, 1f, 1f, 1f); // Ubah alpha menjadi 255
                    questText.fontStyle |= FontStyles.Bold; // Aktifkan bold pada teks
                    selectedImage = questImage; // Simpan gambar yang dipilih saat ini
                    selectedText = questText; // Simpan teks yang dipilih saat ini
                    selectedQuestObject = newQuest; // Atur game object yang baru sebagai yang dipilih
                    firstQuest = true;
                }
                else
                {
                    // Nonaktifkan gambar pada game objek kecuali yang pertama
                    questImage.color = new Color(1f, 1f, 1f, 0f); // Ubah alpha menjadi 0
                }

                index++;
            }
        }

        public void QuestLogInitializationMainQuest(QuestChapter questChapter)
        {
            int index = 1;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Quest quest in questList)
            {
                if (quest.GetQuestChapter() != questChapter) continue;

                GameObject newQuest = null;
                switch (quest.GetQuestChapter())
                {
                    case QuestChapter.Chapter_0:
                        newQuest = Instantiate(questLog, Chapter0ContentContainer);
                        break;
                    case QuestChapter.Chapter_1:
                        newQuest = Instantiate(questLog, Chapter1ContentContainer);
                        break;
                    case QuestChapter.Chapter_2:
                        newQuest = Instantiate(questLog, Chapter2ContentContainer);
                        break;
                    case QuestChapter.Chapter_3:
                        newQuest = Instantiate(questLog, Chapter3ContentContainer);
                        break;
                }
                Button questButton = newQuest.GetComponent<Button>();
                Quest currentQuest = quest;
                TextMeshProUGUI questText = newQuest.GetComponentInChildren<TextMeshProUGUI>();
                Image questImage = newQuest.GetComponent<Image>();
                questButton.onClick.AddListener(() =>
                {
                    if (selectedQuestObject != null)
                    {
                        // Matikan game object yang dipilih sebelumnya
                        selectedQuestObject.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
                        selectedQuestObject.GetComponentInChildren<TextMeshProUGUI>().fontStyle &= ~FontStyles.Bold;
                    }

                    // Aktifkan game object yang baru
                    SetDescription(currentQuest);
                    questImage.color = new Color(1f, 1f, 1f, 1f);
                    questText.fontStyle |= FontStyles.Bold;

                    selectedQuestObject = newQuest; // Atur game object yang baru sebagai yang dipilih
                });

                string questTitle = quest.GetTitle();
                int maxTitleLenght = 25;
                if (questTitle.Length > maxTitleLenght)
                {
                    questTitle = questTitle.Substring(0, maxTitleLenght) + "...";
                }
                newQuest.GetComponent<QuestLog>().SetNameQuestLog($" {index}. " + questTitle);

                if (!firstQuest)
                {
                    questImage.color = new Color(1f, 1f, 1f, 1f); // Ubah alpha menjadi 255
                    questText.fontStyle |= FontStyles.Bold; // Aktifkan bold pada teks
                    selectedImage = questImage; // Simpan gambar yang dipilih saat ini
                    selectedText = questText; // Simpan teks yang dipilih saat ini
                    selectedQuestObject = newQuest; // Atur game object yang baru sebagai yang dipilih
                    firstQuest = true;
                }
                else
                {
                    // Nonaktifkan gambar pada game objek kecuali yang pertama
                    questImage.color = new Color(1f, 1f, 1f, 0f); // Ubah alpha menjadi 0
                }

                index++;
            }
        }

        public void QuestLogInitializationQuest()
        {
            int index = 1;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Quest quest in questList)
            {
                if (!(quest.GetQuestType() == questType)) continue;

                GameObject newQuest = Instantiate(questLog, QuestContentContainer);
                Quest currentQuest = quest;
                Button questButton = newQuest.GetComponent<Button>();
                TextMeshProUGUI questText = newQuest.GetComponentInChildren<TextMeshProUGUI>();
                Image questImage = newQuest.GetComponent<Image>();

                questButton.onClick.AddListener(() =>
                {
                    SetDescription(currentQuest);
                    if (selectedImage != null)
                    {
                        selectedImage.color = new Color(1f, 1f, 1f, 0f);
                    }

                    questImage.color = new Color(1f, 1f, 1f, 1f);
                    if (selectedText != null)
                    {
                        selectedText.fontStyle &= ~FontStyles.Bold;
                    }
                    questText.fontStyle |= FontStyles.Bold;
                    selectedImage = questImage;
                    selectedText = questText;
                });

                string questTitle = quest.GetTitle();
                int maxTitleLenght = 25;
                if (questTitle.Length > maxTitleLenght)
                {
                    questTitle = questTitle.Substring(0, maxTitleLenght) + "...";
                }
                newQuest.GetComponent<QuestLog>().SetNameQuestLog($" {index}. " + questTitle);

                if (!firstQuest)
                {
                    questImage.color = new Color(1f, 1f, 1f, 1f); // Ubah alpha menjadi 255
                    questText.fontStyle |= FontStyles.Bold; // Aktifkan bold pada teks
                    selectedImage = questImage; // Simpan gambar yang dipilih saat ini
                    selectedText = questText; // Simpan teks yang dipilih saat ini
                    selectedQuestObject = newQuest; // Atur game object yang baru sebagai yang dipilih
                }
                else
                {
                    // Nonaktifkan gambar pada game objek kecuali yang pertama
                    questImage.color = new Color(1f, 1f, 1f, 0f); // Ubah alpha menjadi 0
                }

                index++;

            }
        }

        void RemoveQuestLog()
        {
            foreach (Transform child in QuestContentContainer)
            {
                Destroy(child.gameObject);
            }
        }

        void SetDescription(Quest quest)
        {
            title.text = quest.GetTitle();
            giver.text = quest.GetGiver();
            location.text = quest.GetLocation();
            description.text = quest.GetDescription();

            List<string> objectivesList = quest.GetObjective();
            objective.text = "";
            for (int i = 0; i < objectivesList.Count; i++)
            {
                objective.text += objectivesList[i] + " <br>";
            }


            List<string> rewardsList = quest.GetReward();
            reward.text = "";
            for (int i = 0; i < rewardsList.Count; i++)
            {
                reward.text += rewardsList[i] + " <br>";
            }
        }


    }

}