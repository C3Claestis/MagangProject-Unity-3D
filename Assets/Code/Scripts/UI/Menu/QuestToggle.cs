namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.UIElements;

    public class QuestToggle : MonoBehaviour
    {
        [Header("Content Container All Quest")]
        [SerializeField] GameObject _MainQuest;
        [SerializeField] GameObject _SideQuest;
        [SerializeField] GameObject _Commissions;

        [Header("Content Container All Chapter Main Quest")]
        [SerializeField] GameObject _Chapter0;
        [SerializeField] GameObject _Chapter1;
        [SerializeField] GameObject _Chapter2;
        [SerializeField] GameObject _Chapter3;

        [Header("Panel All Quest")]
        private bool mainQuestActive = true;
        private bool sideQuestActive = true;
        private bool commissionsActive = true;

        [Header("Panel All Chapter Main Quest")]
        private bool chapter0_Active= true;
        private bool chapter1_Active= true;
        private bool chapter2_Active= true;
        private bool chapter3_Active= true;

        void Start()
        {

        }

        public void ToggleMainQuest()
        {
            mainQuestActive = !mainQuestActive;
            _MainQuest.SetActive(mainQuestActive);
        }

        public void ToggleSideQuest()
        {
            sideQuestActive = !sideQuestActive;
            _SideQuest.SetActive(sideQuestActive);
        }

        public void ToggleCommissions()
        {
            commissionsActive = !commissionsActive;
            _Commissions.SetActive(commissionsActive);
        }

        public void ToggleChapter_0()
        {
            chapter0_Active = !chapter0_Active;
            _Chapter0.SetActive(chapter0_Active);
        }

        public void ToggleChapter_1()
        {
            chapter1_Active = !chapter1_Active;
            _Chapter1.SetActive(chapter1_Active);
        }

        public void ToggleChapter_2()
        {
            chapter2_Active = !chapter2_Active;
            _Chapter2.SetActive(chapter2_Active);
        }
        
        public void ToggleChapter_3()
        {
            chapter3_Active = !chapter3_Active;
            _Chapter3.SetActive(chapter3_Active);
        }

    }

}