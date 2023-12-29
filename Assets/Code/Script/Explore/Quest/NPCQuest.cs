namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Quest;
    using UnityEditor.Animations;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class NPCQuest : MonoBehaviour
    {
        float rotationSpeed = 5f;
        PlayerInput playerInput;
        Quaternion initialRotation;
        Animator transisi;
        private bool isTalk = false;
        private bool isInterect = false;
        private bool Index = false;

        [Header("Dialogue Value")]
        [SerializeField] TextAsset inkJSON;

        [Header("InkExternal")]
        private InkExternal inkExternal;

        [Header("Quest 1 Or Not?")]
        [SerializeField] QuestOneOrNot Quest;

        [Header("Object Destroy Or Not?")]
        [SerializeField] DestroyOrNot DestroyAfterDialogue;
        [SerializeField] bool EldriaFresnel;
        [SerializeField] int indexCutscene;

        [Header("Name Of Object")]
        [SerializeField] string Name;

        [Header("Display Taking On Canvas")]
        [SerializeField] GameObject display_taking;
        [SerializeField] GameObject icon;
        private bool isDetect = false;
        private bool hasTaken = false;
        private GameObject taking;

        #region Getter Setter
        public void SetTalk(bool talk) => this.isTalk = talk;
        public void SetInterect(bool interect) => this.isInterect = interect;
        public void SetIndex(bool index) => this.Index = index;
        public void SetIsDetect(bool detect) => this.isDetect = detect;
        public bool GetIsDetect() => isDetect;
        public bool GetIndex() => Index;
        public bool GetInterect() => isInterect;
        #endregion

        // Start is called before the first frame update
        void Start()
        {
            initialRotation = transform.rotation;
            inkExternal = FindAnyObjectByType<InkExternal>();
            playerInput = FindAnyObjectByType<PlayerInput>();
            if (transisi == null) { transisi = GameObject.Find("Transisi").GetComponent<Animator>(); }
            else { return; }

            if (Quest == QuestOneOrNot.Quest_1)
            {
                PlayerPrefs.SetInt("Quest", 0);
            }
            else
            {
                PlayerPrefs.SetInt("Quest", 1);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (isDetect && !hasTaken)
            {
                GameObject content = GameObject.Find("Content Taking").gameObject;
                taking = Instantiate(display_taking);
                taking.transform.SetParent(content.transform);
                // Dapatkan komponen Text dari child objek
                Text textComponent = taking.transform.Find("Name").GetComponent<Text>();

                // Ganti teks dengan nama yang sesuai
                if (textComponent != null)
                {
                    textComponent.text = Name; // Ganti dengan cara Anda mendapatkan nama yang sesuai
                }
                hasTaken = true;
            }
            if (!isDetect)
            {
                // Hancurkan objek yang di-instantiate saat pemain keluar dari trigger
                if (taking != null)
                {
                    Destroy(taking);
                    hasTaken = false;
                }
            }
            //Jika NPC sedang interaksi
            if (isTalk)
            {
                if (DestroyAfterDialogue == DestroyOrNot.Yes)
                {
                    if (Quest == QuestOneOrNot.Quest_1)
                    {
                        if (!EldriaFresnel)
                        {
                            SaveLastPosisi.GetInstance().SetSave(true);
                            Destroy(taking);
                            transisi.SetTrigger("Dialog");
                            HandleQuest.GetInstance().Mision1 = true;
                            playerInput.SwitchCurrentActionMap("Player");
                            QuestTriggerObject.GetInstance().SetIsQuest(false);                            
                            isTalk = false;
                            SceneManager.LoadScene(indexCutscene);
                            Destroy(gameObject);
                        }
                        else
                        {
                            SaveLastPosisi.GetInstance().SetSave(true);
                            Destroy(taking);
                            transisi.SetTrigger("Dialog");
                            HandleQuest.GetInstance().Mision1 = true;
                            playerInput.SwitchCurrentActionMap("Player");
                            QuestTriggerObject.GetInstance().SetIsQuest(false);                            
                            isTalk = false;
                            Destroy(gameObject);                            
                        }
                    }
                    else
                    {
                        SaveLastPosisi.GetInstance().SetSave(true);
                        Destroy(taking);
                        transisi.SetTrigger("Dialog");
                        HandleQuest.GetInstance().Mision2 = true;
                        playerInput.SwitchCurrentActionMap("Player");
                        QuestTriggerObject.GetInstance().SetIsQuest(false);                        
                        isTalk = false;
                        SceneManager.LoadScene(indexCutscene);
                        Destroy(gameObject);
                    }
                }

                else
                {
                    icon.SetActive(false);
                    isDetect = false;
                    WaitUI();
                }
            }

            //Jika sedang tidak ada player yang Raycast
            if (!isInterect)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
            }
        }
        void SearchImage(bool Image_1, bool Image_2)
        {
            GameObject image1 = GameObject.Find("Avatar-1");
            GameObject image2 = GameObject.Find("Avatar-2");

            // Cari parent GameObject yang berisi child bernama "Avatar-1"
            Transform childAvatar1 = image1.transform.Find("Avatar");
            Transform childAvatar2 = image2.transform.Find("Avatar");

            if (childAvatar1 != null)
            {
                childAvatar1.gameObject.SetActive(Image_1);
            }
            if (childAvatar2 != null)
            {
                childAvatar2.gameObject.SetActive(Image_2);
            }
        }

        void WaitUI()
        {
            DialogueManager.GetInstance().EnterDialogMode(inkJSON, 0);
            switch (inkExternal.GetAvatar())
            {
                case 1:
                    //Debug.Log("BERHASIL AMBIL " + 1);
                    SearchImage(true, false);
                    break;
                case 2:
                    //Debug.Log("BERHASIL AMBIL " + 2);
                    SearchImage(false, true);
                    break;
                case 3:
                    //Debug.Log("BERHASIL AMBIL " + 3);
                    SearchImage(false, false);
                    break;
            }
        }
    }
}