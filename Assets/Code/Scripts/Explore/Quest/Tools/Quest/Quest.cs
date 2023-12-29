using System;

namespace Nivandria.Quest
{
    [Serializable]
    public class Quest
    {
        public QuestData data;
        public QuestProgress[] progressList;
        public bool isComplete;
    }
}