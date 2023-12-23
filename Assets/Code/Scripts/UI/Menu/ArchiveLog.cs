namespace Nivandria.UI.Archive
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class ArchiveLog : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameArchive;

        // private bool isBold = false;

        private Archive archive;
        public Archive GetArchive() => archive;

        public void SetArchiveDetail(Archive archive)
        {
            this.archive = archive;
            nameArchive.text = archive.GetTitle();
        }

        public void UpdateVisual()
        {
            bool status = ArchiveLogManagerTest.Instance.GetSelectedArchiveLog() == this;
            CanvasGroup canvasGroup = transform.GetChild(0).GetComponent<CanvasGroup>();
            if (status == true)
            {
                nameArchive.fontStyle = FontStyles.Bold;
                canvasGroup.alpha = 1;
                GetComponent<Button>().Select();
            }
            else
            {
                nameArchive.fontStyle = FontStyles.Normal;
                canvasGroup.alpha = 0;
            }
        }

        public void SetSelected()
        {
            ArchiveLogManagerTest.Instance.SetSelectedArchiveLog(this);
            ArchiveLogManagerTest.Instance.UpdateVisualArchiveLog();
        }

        public void SetNameArchiveLog(string titleName)
        {
            nameArchive.text = titleName;
        }

        // public void SetBold(bool bold)
        // {
        //     isBold = bold;
        //     nameArchive.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
        // }
    }
}