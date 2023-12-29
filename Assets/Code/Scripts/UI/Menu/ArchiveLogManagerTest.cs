namespace Nivandria.UI.Archive
{
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class ArchiveLogManagerTest : MonoBehaviour
    {
        public static ArchiveLogManagerTest Instance { get; private set; }
        [SerializeField] Transform contentContainer;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;

        [SerializeField] List<Archive> archiveList = new List<Archive>();
        [SerializeField] GameObject archiveLog;

        private List<ArchiveLog> archiveLogList;
        private ArchiveLog selectedArchiveLog;

        public ArchiveLog GetSelectedArchiveLog() => selectedArchiveLog;

        private GameObject activeArchive = null;
        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            InitializeArchiveLog();
            // ArchiveLogInitialization();
            // ShowDefaultDescription();
            // SetIndexBorderFirst();
        }

        public void SetIndexBorderFirst()
        {
            if (archiveList.Count > 0)
            {
                activeArchive = contentContainer.GetChild(0).gameObject;
                CanvasGroup canvasGroup = activeArchive.transform.GetChild(0).GetComponent<CanvasGroup>();
                canvasGroup.alpha = 1;
            }
        }

        public void InitializeArchiveLog()
        {
            GameObject newArchive;
            archiveLogList = new List<ArchiveLog>();

            for (int i = 0; i < archiveList.Count; i++)
            {
                newArchive = Instantiate(archiveLog, contentContainer);
                newArchive.GetComponent<ArchiveLog>().SetArchiveDetail(archiveList[i]);

                archiveLogList.Add(newArchive.GetComponent<ArchiveLog>());
            }

            SetSelectedArchiveLog(archiveLogList[0]);
        }


        /* public void ArchiveLogInitialization()
        {
            int index = 0;
            archiveLogList = new List<ArchiveLog>();

            foreach (Archive archive in archiveList)
            {
                index += 1;
                GameObject newArchive = Instantiate(archiveLog, contentContainer);
                TextMeshProUGUI archiveText = newArchive.GetComponentInChildren<TextMeshProUGUI>();
                Image archiveImage = newArchive.GetComponentInChildren<Image>();

                Archive currentArchive = archive;

                Button archiveButton = newArchive.GetComponent<Button>();
                archiveButton.onClick.AddListener(() =>
                {
                    SetDescription(currentArchive);
                    UpdateArchiveSelection(newArchive);
                }
                );

                SetArchiveText(archiveText, index, archive);
                
                // SetImageAlpha(archiveImage, 0f);
            }
        } */

        public void SetSelectedArchiveLog(ArchiveLog archiveLog)
        {
            selectedArchiveLog = archiveLog;
            title.text = selectedArchiveLog.GetArchive().GetTitle();
            description.text = selectedArchiveLog.GetArchive().GetDescription();
            UpdateVisualArchiveLog();
        }

        public void UpdateVisualArchiveLog()
        {
            foreach (ArchiveLog archive in archiveLogList)
            {
                archive.UpdateVisual();
            }
        }

        private void UpdateArchiveSelection(GameObject clickedArchive)
        {
            // Mengubah alpha gambar game object yang sedang aktif menjadi 0
            if (activeArchive != null)
            {
                Image activeImage = activeArchive.GetComponentInChildren<Image>();
                SetImageAlpha(activeImage, 0f);
            }

            // Mengubah alpha gambar game object yang diklik menjadi 255
            Image clickedImage = clickedArchive.GetComponentInChildren<Image>();
            SetImageAlpha(clickedImage, 1f);

            // Mengatur game object yang sedang aktif menjadi game object yang diklik
            activeArchive = clickedArchive;
        }

        private void SetArchiveText(TextMeshProUGUI archiveText, int index, Archive archive)
        {
            archiveText.text = $" {index}. {archive.GetTitle()}"; // Menampilkan indeks dan judul archive
        }

        private void SetDescription(Archive archive)
        {
            if (title != null && description != null)
            {
                title.text = archive.GetTitle();
                description.text = archive.GetDescription();
            }
        }

        private void ShowDefaultDescription()
        {
            if (archiveList.Count > 0)
            {
                SetDescription(archiveList[0]);
            }
        }

        private void SetImageAlpha(Image image, float alpha)
        {
            Color color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }

}