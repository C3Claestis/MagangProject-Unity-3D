namespace Nivandria.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HandleQuest : MonoBehaviour
    {        
        QuestManager questManager;
        public QuestData quest1, quest2;  
        // Start is called before the first frame update
        void Start()
        {
            questManager = GetComponent<QuestManager>();
            questManager.AddCurrentQuest(quest1);                       
            Debug.Log(questManager.GetCurrentObjective(quest1).Type);
            
            //Debug.Log(questManager.GetQuestList()[0]);            
        }

        // Update is called once per frame
        void Update()
        {

            if(Input.GetKeyDown(KeyCode.Alpha1)){
                questManager.CompleteObjectiveQuest(quest1, 0);
                Debug.Log(questManager.GetCurrentObjective(quest1).Type);
            }
        }
    }
}