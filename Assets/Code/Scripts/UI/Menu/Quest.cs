namespace Nivandria.UI.Quest
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class Quest
    {
        [SerializeField] private string title;
        [SerializeField] private QuestType type;
        [SerializeField] private string giver;
        [SerializeField] private string location;
        //[SerializeField] private Sprite Image;
        [SerializeField] private string description;
        [SerializeField] private List<string> objective; //Sementara
        [SerializeField] private List<string> reward; //Sementara
        [SerializeField] private bool IsCompleted;

        public Quest(string title, QuestType type, string giver, string location, string description, List<string> objective, List<string> reward){
            this.title = title;
            this.type = type;
            this.giver = giver;
            this.location = location;
            this.description = description;
            this.objective = objective;
            this.reward = reward;

        }
        public string GetTitle() => title;
        public QuestType GetQuestType() => type;
        public string GetGiver() => giver;
        public string GetLocation() => location;
        public string GetDescription() => description;
        public List<string> GetObjective() => objective;
        public List<string> GetReward() => reward;
    }

}