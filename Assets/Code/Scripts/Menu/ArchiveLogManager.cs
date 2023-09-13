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
        [SerializeField] Transform contentContainer;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;


        [SerializeField] List<Archive> archiveList = new List<Archive>();
        [SerializeField] GameObject archiveLog;


        void Start()
        {
            ArchiveLogInitialization();
        }

        void Update()
        {

        }

        private bool isFullTextShown = false;

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

        void SetDescription(Archive archive)
        {
            title.text = archive.GetTitle();
            description.text = archive.GetDescription();
        }
    }

}