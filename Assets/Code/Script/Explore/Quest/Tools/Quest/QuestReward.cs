namespace Nivandria.Quest
{
    [System.Serializable]
    public class QuestReward
    {
        public QuestRewardType Type;
        public string ItemName;
        public int ItemQuantity;
        public string ReputationPlace;
        public int ReputationProgress;
    }
}