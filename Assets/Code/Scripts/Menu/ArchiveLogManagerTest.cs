namespace Nivandria.UI.Archive
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;

    public class ArchiveLogManagerTest : MonoBehaviour
    {
        [SerializeField] Transform contentContainer;

        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI description;

        [SerializeField] List<Archive> archiveList = new List<Archive>();
        [SerializeField] GameObject archiveLog;


        // Start is called before the first frame update
        void Start()
        {
            ArchiveLogInitialization();
            ShowDefaultDescription();
            SetIndexBorderFirst();
        }

        // Update is called once per frame
        void Update()
        {

        }


        private GameObject activeArchive = null;

        public void SetIndexBorderFirst()
        {
            if (archiveList.Count > 0)
            {
                activeArchive = contentContainer.GetChild(0).gameObject;
                Image activeImage = activeArchive.GetComponentInChildren<Image>();
                SetImageAlpha(activeImage, 1f); // Mengatur alpha menjadi 255
            }
        }

        public void ArchiveLogInitialization()
        {
            int index = 0;

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

                SetImageAlpha(archiveImage, 0f);



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