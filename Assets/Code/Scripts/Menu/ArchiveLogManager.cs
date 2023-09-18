namespace Nivandria.UI.Archive
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using Unity.VisualScripting;
    using UnityEngine.UI;

    public class ArchiveLogManager : MonoBehaviour
    {
        //public static ArchiveLogManager Instance { get; private set; }
        [SerializeField] Transform contentContainer;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;


        [SerializeField] List<Archive> archiveList = new List<Archive>();
        [SerializeField] GameObject archiveLog;

        private ArchiveLog selectedArchiveLog;

        /*
        private List<ArchiveLog> keyLogList;
        private ArchiveLog selectedArchiveLog;

        public ArchiveLog GetSelectedArchiveLog() => selectedArchiveLog;

        
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
        */


        void Start()
        {
            ArchiveLogInitialization();
        }

        void Update()
        {

        }

        private bool isFullTextShown = false;

        Image selectedImage = null;
        TextMeshProUGUI selectedText = null;

        public void ArchiveLogInitialization()
        {
            int index = 0;


            foreach (Archive archive in archiveList)
            {
                index += 1;
                GameObject newArchive = Instantiate(archiveLog, contentContainer);
                ArchiveLog archiveLogComponent = newArchive.GetComponent<ArchiveLog>();
                TextMeshProUGUI archiveText = newArchive.GetComponentInChildren<TextMeshProUGUI>();
                Image archiveImage = newArchive.GetComponent<Image>();

                Archive currentArchive = archive;
                Button archiveButton = newArchive.GetComponent<Button>();
                archiveButton.onClick.AddListener(() =>
                {
                    SetDescription(currentArchive);
                    UpdateArchiveSelection(selectedImage, archiveImage, selectedText, archiveText, index);
                });

                InitializeArchiveLogUI(newArchive, index, archive, archiveImage, archiveText, selectedImage, selectedText);

                if (index == 1)
                {
                    SelectFirstArchive(newArchive, archiveImage, archiveText, selectedImage, selectedText);
                }
                else
                {
                    archiveImage.color = new Color(1f, 1f, 1f, 0f);
                }
            }
        }

        public void UpdateArchiveSelection(Image selectedImage, Image archiveImage, TextMeshProUGUI selectedText, TextMeshProUGUI archiveText, int index)
        {
            if (selectedImage != null)
            {
                //Color selectedImageColor = selectedImage.color;
                //selectedImageColor.a = 0f; // Atur alpha menjadi 0
                //selectedImage.color = selectedImageColor;
                selectedImage.color = new Color(1f, 1f, 1f, 0f);
            }

            //Color archiveImageColor = archiveImage.color;
            //archiveImageColor.a = 1f; // Atur alpha menjadi 1 (tidak transparan)
            //archiveImage.color = archiveImageColor;
            archiveImage.color = new Color(1f, 1f, 1f, 1f);

            if (selectedText != null)
            {
                selectedText.fontStyle &= ~FontStyles.Bold;

                if (isFullTextShown)
                {
                    int maxTitleLength = 25;
                    string archiveTitle = archiveText.text;
                    if (archiveTitle.Length > maxTitleLength)
                    {
                        archiveTitle = archiveTitle.Substring(0, maxTitleLength) + "...";
                    }

                    selectedText.text = $" {index}. " + archiveTitle;
                    isFullTextShown = false;
                }
            }

            archiveText.fontStyle |= FontStyles.Bold;

            selectedImage = archiveImage;
            selectedText = archiveText;

            if (selectedArchiveLog != null)
            {
                ArchiveLog archiveLogComponent = selectedArchiveLog.GetComponent<ArchiveLog>();
                archiveLogComponent.SetBold(false);
            }

            // Set efek bold untuk item yang dipilih saat ini
            ArchiveLog currentArchiveLog = archiveText.GetComponentInParent<ArchiveLog>();
            currentArchiveLog.SetBold(true);

            // Simpan item yang dipilih saat ini
            selectedArchiveLog = currentArchiveLog;
        }

        public void InitializeArchiveLogUI(GameObject newArchive, int index, Archive archive, Image archiveImage, TextMeshProUGUI archiveText, Image selectedImage, TextMeshProUGUI selectedText)
        {
            string archiveTitle = archive.GetTitle();
            int maxTitleLength = 25;
            if (archiveTitle.Length > maxTitleLength)
            {
                archiveTitle = archiveTitle.Substring(0, maxTitleLength) + "...";
            }
            newArchive.GetComponent<ArchiveLog>().SetNameArchiveLog($" {index}. " + archiveTitle);
        }

        public void SelectFirstArchive(GameObject newArchive, Image archiveImage, TextMeshProUGUI archiveText, Image selectedImage, TextMeshProUGUI selectedText)
        {
            archiveImage.color = new Color(1f, 1f, 1f, 1f);
            archiveText.fontStyle |= FontStyles.Bold;
            selectedImage = archiveImage;
            selectedText = archiveText;
        }

        public void DisableArchiveImage(Image archiveImage)
        {
            archiveImage.color = new Color(1f, 1f, 1f, 0f);
        }

        private void SetDescription(Archive archive)
        {
            title.text = archive.GetTitle();
            description.text = archive.GetDescription();
        }



        /*
        public void ArchiveLogInitialization()
        {
            int index = 0;
            Image selectedImage = null;
            TextMeshProUGUI selectedText = null;

            foreach (Archive archive in archiveList)
            {
                index += 1;
                GameObject newArchive = Instantiate(archiveLog, contentContainer);
                Button archiveButton = newArchive.GetComponent<Button>();
                Archive currentArchive = archive;
                TextMeshProUGUI archiveText = newArchive.GetComponentInChildren<TextMeshProUGUI>();
                Image archiveImage = newArchive.GetComponent<Image>();
                archiveButton.onClick.AddListener(() =>
                {
                    SetDescription(currentArchive);

                    if (selectedImage != null)
                    {
                        selectedImage.color = new Color(1f, 1f, 1f, 0f);
                    }

                    archiveImage.color = new Color(1f, 1f, 1f, 1f);

                    if (selectedText != null)
                    {
                        selectedText.fontStyle &= ~FontStyles.Bold;

                        if (isFullTextShown)
                        {
                            int maxTitleLenght = 25;
                            string archiveTitle = currentArchive.GetTitle();
                            if (archiveTitle.Length > maxTitleLenght)
                            {
                                archiveTitle = archiveTitle.Substring(0, maxTitleLenght) + "...";
                            }

                            selectedText.text = $" {index}. " + archiveTitle;
                            isFullTextShown = false;
                        }
                    }

                    archiveText.fontStyle |= FontStyles.Bold;

                    selectedImage = archiveImage;
                    selectedText = archiveText;
                });

                string archiveTitle = archive.GetTitle();
                int maxTitleLenght = 25;
                if (archiveTitle.Length > maxTitleLenght)
                {
                    archiveTitle = archiveTitle.Substring(0, maxTitleLenght) + "...";
                }

                newArchive.GetComponent<ArchiveLog>().SetNameArchiveLog($" {index}. " + archiveTitle);

                if (index == 1)
                {
                    archiveImage.color = new Color(1f, 1f, 1f, 1f); // Ubah alpha menjadi 255
                    archiveText.fontStyle |= FontStyles.Bold; // Aktifkan bold pada teks
                    selectedImage = archiveImage; // Simpan gambar yang dipilih saat ini
                    selectedText = archiveText; // Simpan teks yang dipilih saat ini
                }
                else
                {
                    // Nonaktifkan gambar pada game objek kecuali yang pertama
                    archiveImage.color = new Color(1f, 1f, 1f, 0f); // Ubah alpha menjadi 0
                }
            }
        }
        */




        /*

        public void UpdateVisualArchiveLog()
        {
            foreach (ArchiveLog key in keyLogList)
            {
                key.UpdateVisual();
            }
        }

        public void SetSelectedArchiveLog(ArchiveLog archiveLog)
        {
            selectedArchiveLog = archiveLog;
            title.text = selectedArchiveLog.GetArchive().GetTitle();
            description.text = selectedArchiveLog.GetArchive().GetDescription();
            UpdateVisualArchiveLog();
        }
        */
    }

}