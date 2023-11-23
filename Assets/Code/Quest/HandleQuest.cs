namespace Nivandria.Quest
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine.UI;
    using UnityEngine;
    using Nivandria.Explore;
    using System;
    using Ink.Runtime;

    public class HandleQuest : MonoBehaviour
    {
        private static HandleQuest instance;
        private bool hasSpawned = true;
        QuestManager questManager;

        [Header("Quest Data")]
        [SerializeField] QuestData quest1;
        [SerializeField] QuestData quest2;
        [HideInInspector] public bool Mision1, Mision2;

        [Header("UI Panel Quest")]
        [SerializeField] Text quest_description;
        [SerializeField] Text quest_type_and_tittle;

        [Header("Gameobject Quest")]
        [SerializeField] Transform pointer;

        [Header("Component Quest 1")]
        [SerializeField] Transform pintu_kamar_sacra;
        [SerializeField] GameObject MQ1_1;

        [Header("Component Quest 2")]
        [SerializeField] Transform pintu_kamar_vana;
        [SerializeField] GameObject MQ1_2;

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
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PlayerPrefs.SetInt("Prologue1", 0);
                PlayerPrefs.SetInt("Prologue2", 0);
            }
            //Debug.Log("QUEST INDEX = " + PlayerPrefs.GetInt("Quest"));
            //Debug.Log("PROLOGUE 1 INDEX = " + PlayerPrefs.GetInt("Prologue1"));

            SwitchQuest(PlayerPrefs.GetInt("Quest"));

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

        void SwitchQuest(int questData)
        {
            if (questData == 0)
            {
                questManager.AddCurrentQuest(quest1);
                CurrentFirstQuest(PlayerPrefs.GetInt("Prologue1"));
                quest_type_and_tittle.text = quest1.Type.ToString() + " Quest " + " : " + quest1.Title;
                quest_description.text = questManager.GetCurrentObjective(quest1).Description;

            }
            else
            {
                questManager.AddCurrentQuest(quest2);
                CurrentFirstQuest(PlayerPrefs.GetInt("Prologue2"));
                quest_type_and_tittle.text = quest2.Type.ToString() + " Quest " + " : " + quest2.Title;
                quest_description.text = questManager.GetCurrentObjective(quest2).Description;

            }
        }

        private void CurrentFirstQuest(int indexobjective)
        {
            switch (indexobjective)
            {
                case 0:
                    SpawnQuest(MQ1_1);
                    //QuestPointer(new Vector3(10f, 1f, 10f));                                                            
                    break;
                case 1:
                    SpawnQuest(MQ1_2);
                    questManager.CompleteObjectiveQuest(quest1, 0);
                    pintu_kamar_sacra.localRotation = Quaternion.Euler(0, 120, 0);
                    //QuestPointer(new Vector3(100f, 1f, 50f));                    
                    break;
                case 2:
                    Clear(2);
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, -120, 0);
                    pintu_kamar_sacra.localRotation = Quaternion.Euler(0, 120, 0);
                    break;
                case 3:
                    Clear(3);
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, -120, 0);
                    pintu_kamar_sacra.localRotation = Quaternion.Euler(0, 120, 0);
                    break;
                case 4:
                    Clear(4);
                    pintu_kamar_vana.localRotation = Quaternion.Euler(0, -120, 0);
                    pintu_kamar_sacra.localRotation = Quaternion.Euler(0, 120, 0);
                    break;
                case 5:
                    Clear(5);
                    break;
                case 6:
                    Clear(6);
                    break;
                case 7:
                    Clear(7);
                    break;
            }
        }

        private void CurrentSecondQuest(int indexobjective)
        {
            switch (indexobjective)
            {
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
                case 4:

                    break;
                case 5:

                    break;
            }
        }

        void QuestPointer(Vector3 pointers)
        {
            pointer.position = pointers;
        }
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
            for (int i = 0; i < value; i++)
            {
                questManager.CompleteObjectiveQuest(quest1, i);
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
    }
}