namespace Nivandria.UI.Archive
{
    using UnityEngine;
    using TMPro;

    public class ArchiveLog : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameArchive;

        private bool isBold = false;

        /*
        private Archive archive;

        public Archive GetArchive() => archive;

        public void SetArchiveDetail(Archive archive)
        {
            this.archive = archive;
            nameArchive.text = archive.GetTitle();
        }
        */

        public void SetNameArchiveLog(string titleName)
        {
            nameArchive.text = titleName;
        }

        public void SetBold(bool bold)
    {
        isBold = bold;
        nameArchive.fontStyle = bold ? FontStyles.Bold : FontStyles.Normal;
    }

        /*
        public void UpdateVisual()
        {
            bool status = ArchiveLogManager.Instance.GetSelectedArchiveLog() == this;
            if (status)
            {
                nameArchive.fontStyle = FontStyles.Bold;
                GetComponent<Outline>().enabled = true;
                GetComponent<Button>().Select();
            }
            else
            {
                nameArchive.fontStyle = FontStyles.Normal;
                // tambahin off outline
                GetComponent<Outline>().enabled = false;

            }
        }
        public void SetSelected()
        {
            ArchiveLogManager.Instance.SetSelectedArchiveLog(this);
            ArchiveLogManager.Instance.UpdateVisualArchiveLog();
        }
        */

        
    }
}