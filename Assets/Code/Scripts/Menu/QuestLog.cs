namespace Nivandria.UI
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class QuestLog : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameQuest;

        public void SetNameQuestLog(string titleName)
        {
            nameQuest.text = titleName;
        }
    }

}