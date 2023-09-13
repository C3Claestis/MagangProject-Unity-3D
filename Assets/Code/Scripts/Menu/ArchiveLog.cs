namespace Nivandria.UI.Archive
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class ArchiveLog : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameArchive;

        public void SetNameArchiveLog(string titleName)
        {
            nameArchive.text = titleName;
        }
    }
}