namespace Nivandria.UI.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.UIElements;

    public class QuestToggle : MonoBehaviour
    {
        [SerializeField] GameObject _MainQuest;
        [SerializeField] GameObject _SideQuest;
        [SerializeField] GameObject _Commissions;

        [Header("Content Size Fitter")]
        [SerializeField] private ContentSizeFitter mainQuest;
        [SerializeField] private ContentSizeFitter sideQuest;
        [SerializeField] private ContentSizeFitter commissions;

        private bool mainQuestActive = true;
        private bool sideQuestActive = true;
        private bool commissionsActive = true;

        void Start()
        {
            //StartCoroutine(DelayedContentSizeFitterChange(2f));
        }

        IEnumerator DelayedContentSizeFitterChange(float delay)
        {
            yield return new WaitForSeconds(delay); // Tunggu selama 2 detik

            //MainQuestContentSizeFitter();
            //SideQuestContentSizeFitter();
            //CommissionsContentSizeFitter();
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


        /*
        public void MainQuestContentSizeFitter()
        {
            if (mainQuest == null)
            {
                mainQuest = GetComponent<ContentSizeFitter>();
                Debug.Log("Ada ContentSizeFitter Main Quest");
            }

            // Memeriksa apakah komponen ContentSizeFitter ditemukan
            if (mainQuest != null)
            {
                // Mengubah nilai Vertical Fit ke Min Size
                mainQuest.verticalFit = ContentSizeFitter.FitMode.MinSize;
            }
            else
            {
                Debug.LogError("ContentSizeFitter component not found on this MainQuest.");
            }
        }

        public void SideQuestContentSizeFitter()
        {
            if (sideQuest == null)
            {
                sideQuest = GetComponent<ContentSizeFitter>();
            }


            // Memeriksa apakah komponen ContentSizeFitter ditemukan
            if (sideQuest != null)
            {
                // Mengubah nilai Vertical Fit ke Min Size
                sideQuest.verticalFit = ContentSizeFitter.FitMode.MinSize;
            }
            else
            {
                Debug.LogError("ContentSizeFitter component not found on this SideQuest.");
            }
        }

        public void CommissionsContentSizeFitter()
        {
            if (commissions == null)
            {
                commissions = GetComponent<ContentSizeFitter>();
            }

            // Memeriksa apakah komponen ContentSizeFitter ditemukan
            if (commissions != null)
            {
                // Mengubah nilai Vertical Fit ke Min Size
                commissions.verticalFit = ContentSizeFitter.FitMode.MinSize;
            }
            else
            {
                Debug.LogError("ContentSizeFitter component not found on this Commissions.");
            }
        }
        */
    }

}