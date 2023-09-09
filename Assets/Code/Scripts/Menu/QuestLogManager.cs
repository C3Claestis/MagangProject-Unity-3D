namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine.UI;

    public class QuestLogManager : MonoBehaviour
    {
        [SerializeField] Transform contentContainer;
        
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI giver;
        [SerializeField] TextMeshProUGUI location;
        [SerializeField] TextMeshProUGUI description;
        [SerializeField] TextMeshProUGUI objective;
        [SerializeField] TextMeshProUGUI reward;

        [SerializeField] QuestType questType = QuestType.Main;
        [SerializeField] QuestType currentQuestType;
        

        [SerializeField] List<Quest> questList = new List<Quest>();
        [SerializeField] GameObject questLog;
        // Start is called before the first frame update
        void Start()
        {

        }

        void Update()
        {
            if (questType == currentQuestType) return;
            currentQuestType = questType;
            RemoveQuestLog();
            QuestLogInitialization();
        }

        public void QuestLogInitialization()
        {
            int index = 0;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Quest quest in questList)
            {
                if (!(quest.GetQuestType() == questType)) continue;
                index += 1;
                GameObject newQuest = Instantiate(questLog, contentContainer);
                Button questButton = newQuest.GetComponent<Button>();
                Quest currentQuest = quest;
                TextMeshProUGUI questText = newQuest.GetComponentInChildren<TextMeshProUGUI>();
                Image questImage = newQuest.GetComponent<Image>();
                questButton.onClick.AddListener(() => 
                {
                    SetDescription(currentQuest);

                    if(selectedImage != null)
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
                newQuest.GetComponent<QuestLog>().SetNameQuestLog($" {index}. "+ quest.GetTitle());

                /*if (index != 1) continue;
                int indexList = questList.IndexOf(quest);
                SetDescription(questList[indexList]);
                questText.fontStyle = FontStyles.Bold;
                */

                if(index == 1)
                {
                    questImage.color = new Color(1f, 1f, 1f, 1f); // Ubah alpha menjadi 255
                    questText.fontStyle |= FontStyles.Bold; // Aktifkan bold pada teks
                    selectedImage = questImage; // Simpan gambar yang dipilih saat ini
                    selectedText = questText; // Simpan teks yang dipilih saat ini
                }
                else
                {
                    // Nonaktifkan gambar pada game objek kecuali yang pertama
                    questImage.color = new Color(1f, 1f, 1f, 0f); // Ubah alpha menjadi 0
                }
            }


        }

        void RemoveQuestLog()
        {
            foreach (Transform child in contentContainer)
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
                objective.text += objectivesList[i]+" <br>";
            }
            
            
            List<string> rewardsList = quest.GetReward();
            reward.text = "";
            for (int i = 0; i < rewardsList.Count; i++)
            {
                reward.text += rewardsList[i]+" <br>";
            }
        }


    }

}