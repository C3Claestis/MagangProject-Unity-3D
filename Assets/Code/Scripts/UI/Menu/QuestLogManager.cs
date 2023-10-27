namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class QuestLogManager : MonoBehaviour
    {
        [SerializeField] Transform MainQuestContentContainer;
        [SerializeField] Transform SideQuestContentContainer;
        [SerializeField] Transform CommissionsContentContainer;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI giver;
        [SerializeField] TextMeshProUGUI location;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI objective;
        [SerializeField] TextMeshProUGUI reward;

        [SerializeField] List<Quest> questList = new List<Quest>();
        [SerializeField] GameObject questLog;

        private GameObject selectedQuestObject = null;

        bool firstQuest;
        // Start is called before the first frame update
        void Start()
        {
            QuestLogInitialization(QuestType.Main);
            QuestLogInitialization(QuestType.Side);
            QuestLogInitialization(QuestType.Commission);
        }

        public void QuestLogInitialization(QuestType questType)
        {
            int index = 1;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Quest quest in questList)
            {
                if (quest.GetQuestType() != questType) continue;
                //index += 1;

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