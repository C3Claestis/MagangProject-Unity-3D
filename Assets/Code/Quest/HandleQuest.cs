namespace Nivandria.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class HandleQuest : MonoBehaviour
    {
        private static HandleQuest instance;
        private bool hasSpawned = true;
        QuestManager questManager;
        bool isNonQuest = false;

        //Komponen Quest Data
        [Header("Quest Data")]
        [SerializeField] QuestData quest1;
        [SerializeField] QuestData quest2;
        [HideInInspector] public bool Mision1, Mision2;

        //Komponen UI Panel Quest
        [Header("UI Panel Quest")]
        [SerializeField] Text quest_description;
        [SerializeField] Text quest_type_and_tittle;
        [SerializeField] GameObject canvas_dialogue;

        #region Componen Quest          
        [Header("Object Non Quest")]
        [SerializeField] GameObject Quest_Pointer_ToHouseYard;
        [SerializeField] GameObject Quest_Pointer_ToHouse;
        [SerializeField] GameObject Quest_Pointer_ToTrainingGround;
        [SerializeField] GameObject Quest_Pointer_ToHouseYardFromTrainingGround;

        [Header("Component Quest 1")]
        [SerializeField] GameObject teleport_house_yard;
        [SerializeField] GameObject MQ1_0;

        [Header("Component Quest 2")]
        [SerializeField] Transform pintu_kamar_vana;
        [SerializeField] GameObject MQ1_1;

        [Header("Component Quest 3")]
        [SerializeField] GameObject MQ1_2;

        [Header("Component Quest 4")]
        [SerializeField] GameObject MQ1_3;

        [Header("Component Quest 5")]
        [SerializeField] GameObject MQ1_4;

        [Header("Component Quest 6")]
        [SerializeField] GameObject MQ1_5;

        [Header("Component Quest 7")]
        [SerializeField] GameObject MQ1_6;

        [Header("Component Quest 8")]
        [SerializeField] GameObject MQ1_7;

        [Header("Component Quest 9")]
        [SerializeField] GameObject MQ1_8;

        [Header("Component Quest 10")]
        [SerializeField] GameObject MQ1_9;

        [Header("Component Quest 11")]
        [SerializeField] GameObject MQ2_1;

        [Header("Component Quest 12")]
        [SerializeField] GameObject MQ2_2;

        [Header("Component Quest 13")]
        [SerializeField] GameObject MQ2_3;

        [Header("Component Quest 14")]
        [SerializeField] GameObject MQ2_4;

        [Header("Component Quest 15")]
        [SerializeField] GameObject MQ2_5;
        #endregion
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;
        }
        public static HandleQuest GetInstance()
        {
            return instance;
        }
        // Start is called before the first frame update
        void Start()
        {
            questManager = GetComponent<QuestManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                PlayerPrefs.DeleteKey("Quest");
                PlayerPrefs.DeleteKey("Prologue1");
                PlayerPrefs.DeleteKey("Prologue2");
            }

            Debug.Log("CURRENY INDEX PROLOGUE I = " + PlayerPrefs.GetInt("Prologue1"));
            Debug.Log("QUEST INDEX = " + PlayerPrefs.GetInt("Quest"));
            Debug.Log("CURRENY INDEX PROLOGUE II = " + PlayerPrefs.GetInt("Prologue2"));

            if (PlayerPrefs.GetInt("Quest") < 2)
            {
                SwitchQuest(PlayerPrefs.GetInt("Quest"));
            }

            if (Mision1)
            {
                QuestComplete(quest1, 1);

            }
            if (Mision2)
            {
                QuestComplete(quest2, 2);
            }
            /*if (PlayerPrefs.GetInt("Quest") == 0)
            {
                questManager.CompleteObjectiveQuest(quest1, count);
                SwitchQuest(PlayerPrefs.GetInt("Quest"));
                PlayerPrefs.SetInt("Quest", 1);
                count++;
            }
            else
            {
                questManager.CompleteObjectiveQuest(quest2, count);
                SwitchQuest(PlayerPrefs.GetInt("Quest"));
                PlayerPrefs.SetInt("Quest", 0);
                count++;
            } */
        }

        //Fungsi untuk menghandle perpindahan quest chapter 0
        void SwitchQuest(int questData)
        {
            //Quest chapter 0 tahap pertama
            if (questData == 0)
            {
                questManager.AddCurrentQuest(quest1);
                CurrentFirstQuest(PlayerPrefs.GetInt("Prologue1"));
                quest_type_and_tittle.text = quest1.Type.ToString() + " Quest " + " : " + quest1.Title;
                if (!isNonQuest)
                {
                    quest_description.text = questManager.GetCurrentObjective(quest1).Description;
                }
            }
            //Quest chapter 0 tahap kedua
            else
            {
                questManager.AddCurrentQuest(quest2);
                CurrentSecondQuest(PlayerPrefs.GetInt("Prologue2"));
                quest_type_and_tittle.text = quest2.Type.ToString() + " Quest " + " : " + quest2.Title;
                if (!isNonQuest)
                {
                    quest_description.text = questManager.GetCurrentObjective(quest2).Description;
                }
            }
        }
        //Fungsi static handle quest tahap pertama
        private void CurrentFirstQuest(int indexobjective)
        {
            switch (indexobjective)
            {
                case 0:
                    SpawnQuest(MQ1_0);
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(false);
                    //Pintu Vana masih tertutup
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, -0.02f, 90);
                    pintu_kamar_vana.localPosition = new Vector3(2.927363f, 1.845128f, -2.231239f);
                    break;
                case 1:
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(false);
                    TakeQuestAndIndexScene(MQ1_1, 1, 1);
                    DestroySpawnQuest(MQ1_0);
                    //Pintu Vana masih tertutup
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, -0.02f, 90);
                    pintu_kamar_vana.localPosition = new Vector3(2.927363f, 1.845128f, -2.231239f);
                    break;
                case 2:
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(false);
                    //Pintu Vana masih terbuka
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, 79.969f, 90);
                    pintu_kamar_vana.localPosition = new Vector3(2.639f, 1.845128f, -2.456f);
                    TakeQuestAndIndexScene(MQ1_2, 1, 2);
                    DestroySpawnQuest(MQ1_1);
                    break;
                case 3:
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(false);
                    TakeQuestAndIndexScene(MQ1_3, 1, 3);
                    DestroySpawnQuest(MQ1_2);
                    break;
                case 4:
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(false);
                    TakeQuestAndIndexScene(MQ1_4, 1, 4);
                    DestroySpawnQuest(MQ1_3);
                    break;
                case 5:
                    //Teleport pindah scene masih mati
                    teleport_house_yard.SetActive(true);
                    TakeQuestAndIndexScene(MQ1_5, 2, 5);
                    DestroySpawnQuest(MQ1_4);
                    break;
                case 6:
                    TakeQuestAndIndexScene(MQ1_6, 2, 6);
                    DestroySpawnQuest(MQ1_5);
                    break;
                case 7:
                    TakeQuestAndIndexScene(MQ1_7, 1, 7);
                    DestroySpawnQuest(MQ1_6);
                    break;
                case 8:
                    TakeQuestAndIndexScene(MQ1_8, 1, 8);
                    DestroySpawnQuest(MQ1_7);
                    break;
                case 9:
                    TakeQuestAndIndexScene(MQ1_9, 1, 9);
                    DestroySpawnQuest(MQ1_8);
                    break;
                case 10:
                    PlayerPrefs.SetInt("Quest", 1);
                    DestroySpawnQuest(MQ1_9);
                    break;
            }
        }
        //Fungsi static quest tahap kedua
        private void CurrentSecondQuest(int indexobjective)
        {
            switch (indexobjective)
            {
                case 0:
                    TakeQuestAndIndexScene(MQ2_1, 3, 0);
                    break;
                case 1:
                    TakeQuestAndIndexScene(MQ2_2, 3, 1);
                    DestroySpawnQuest(MQ2_1);
                    break;
                case 2:
                    TakeQuestAndIndexScene(MQ2_3, 3, 2);
                    DestroySpawnQuest(MQ2_2);
                    break;
                case 3:
                    TakeQuestAndIndexScene(MQ2_4, 1, 3);
                    DestroySpawnQuest(MQ2_3);
                    break;
                case 4:
                    TakeQuestAndIndexScene(MQ2_5, 2, 4);
                    DestroySpawnQuest(MQ2_4);
                    break;
                case 5:
                    DestroySpawnQuest(MQ2_5);
                    canvas_dialogue.SetActive(false);
                    break;
            }
        }
        //Fungsi untuk handle apakah quest berada di scene yang benar atau tidak
        void TakeQuestAndIndexScene(GameObject quest, int indexScene, int indexClear)
        {
            //Jika player dan quest berada di scene yang sama 
            if (SceneManager.GetActiveScene().buildIndex == indexScene)
            {
                isNonQuest = false;
                SpawnQuest(quest);
                Clear(indexClear);
            }

            //Quest ada di Home dan player berada di HouseYard
            else if (SceneManager.GetActiveScene().buildIndex > indexScene
            && SceneManager.GetActiveScene().buildIndex == 2)
            {
                isNonQuest = true;
                quest_description.text = "Go to Inside Home";
                SpawnQuest(Quest_Pointer_ToHouse);
            }

            //Quest ada di Home dan player berada di TrainingGround
            else if (SceneManager.GetActiveScene().buildIndex > indexScene
            && SceneManager.GetActiveScene().buildIndex == 3)
            {
                isNonQuest = true;
                quest_description.text = "Go to Inside Home";
                SpawnQuest(Quest_Pointer_ToHouseYardFromTrainingGround);
            }

            //Quest ada di HouseYard dan player berada di Home
            else if (SceneManager.GetActiveScene().buildIndex < indexScene
            && SceneManager.GetActiveScene().buildIndex == 1)
            {
                isNonQuest = true;
                quest_description.text = "Go to House Yard";
                SpawnQuest(Quest_Pointer_ToHouseYard);
            }

            //Quest ada di HouseYard dan player berada di TrainingGround
            else if (SceneManager.GetActiveScene().buildIndex > indexScene
            && SceneManager.GetActiveScene().buildIndex == 3)
            {
                isNonQuest = true;
                quest_description.text = "Go to House Yard";
                SpawnQuest(Quest_Pointer_ToHouseYardFromTrainingGround);
            }

            //Quest ada di TrainingGround dan player berada di Home
            else if (SceneManager.GetActiveScene().buildIndex < indexScene
            && SceneManager.GetActiveScene().buildIndex == 1)
            {
                isNonQuest = true;
                quest_description.text = "Go to Training Ground";
                SpawnQuest(Quest_Pointer_ToHouseYard);
            }

            //Quest ada di TrainingGround dan player berada di HouseYard
            else if (SceneManager.GetActiveScene().buildIndex < indexScene
            && SceneManager.GetActiveScene().buildIndex == 2)
            {
                isNonQuest = true;
                quest_description.text = "Go to Training Ground";
                SpawnQuest(Quest_Pointer_ToTrainingGround);
            }
        }
        //Fungsi untuk mengakhiri quest
        void QuestComplete(QuestData Index, byte Noindex)
        {
            questManager.CompleteObjectiveQuest(Index, PlayerPrefs.GetInt("Prologue" + Noindex));
            int nilai = PlayerPrefs.GetInt("Prologue" + Noindex);
            PlayerPrefs.SetInt("Prologue" + Noindex, nilai + 1);
            hasSpawned = true;
            Mision1 = false;
            Mision2 = false;
        }
        void Clear(int value)
        {
            if (PlayerPrefs.GetInt("Quest") == 0)
            {
                for (int i = 0; i < value; i++)
                {
                    questManager.CompleteObjectiveQuest(quest1, i);
                }
            }
            else
            {
                for (int i = 0; i < value; i++)
                {
                    questManager.CompleteObjectiveQuest(quest2, i);
                }
            }
        }
        void SpawnQuest(GameObject gameObject)
        {
            if (hasSpawned)
            {
                GameObject objects = GameObject.Find(gameObject.name + "(Clone)");

                if (objects != null)
                {
                    Destroy(objects);
                }

                // Hanya instantiate jika GameObject belum ada.
                if (objects == null)
                {
                    Instantiate(gameObject);
                }

                hasSpawned = false;
            }
        }

        void DestroySpawnQuest(GameObject gameObject)
        {
            GameObject objects = GameObject.Find(gameObject.name + "(Clone)");

            if (objects != null)
            {
                Destroy(objects);
                Debug.Log("Berhasil Destroy = " + objects);
            }
        }
    }
    public enum DestroyOrNot
    {
        Yes,
        No
    }
    public enum QuestOneOrNot
    {
        Quest_1,
        Quest_2
    }
}