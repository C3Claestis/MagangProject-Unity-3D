namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using Nivandria.Quest;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;

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

        [Header("IndexValueCutscene For Switch Scene")]
        [SerializeField] int indexCutscene;

        [Header("Quest 1 Or Not?")]
        [SerializeField] QuestOneOrNot Quest;

        [Header("Object Destroy Or Not?")]
        [SerializeField] DestroyOrNot DestroyAfterDialogue;        

        #region Getter Setter
        public void SetTalk(bool talk) => this.isTalk = talk;
        public void SetInterect(bool interect) => this.isInterect = interect;
        public void SetIndex(bool index) => this.Index = index;
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
        }

        // Update is called once per frame
        void Update()
        {
            //Jika NPC sedang interaksi
            if (isTalk)
            {
                if (DestroyAfterDialogue == DestroyOrNot.Yes)
                {
                    if (Quest == QuestOneOrNot.Quest_1)
                    {
                        HandleQuest.GetInstance().Mision1 = true;
                        Index = true;
                    }
                    else
                    {
                        HandleQuest.GetInstance().Mision2 = true;
                    }

                    if (transisi != null)
                    {
                        transisi.SetTrigger("Dialog");
                        playerInput.SwitchCurrentActionMap("Player");
                        QuestTriggerObject.GetInstance().SetIsQuest(false);
                        SaveLastPosisi.GetInstance().SetSave(true);
                        isTalk = false;
                    }
                }

                else
                {
                    WaitUI();
                }
            }

            //Jika sedang tidak ada player yang Raycast
            if (!isInterect)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * rotationSpeed);
            }

            if (Index)
            {
                Invoke("SwitchScene", 1f);
                Destroy(gameObject);
            }
        }

        void SwitchScene()
        {
            SceneManager.LoadScene(indexCutscene);
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
    public enum DestroyOrNot
    {
        Yes,
        No
    }
    public enum QuestOneOrNot{
        Quest_1,
        Quest_2
    }
}