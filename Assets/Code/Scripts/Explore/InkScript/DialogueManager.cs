namespace Nivandria.Explore
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Ink.Runtime;

    /// <summary>
    /// Manages dialogues and choices in a Unity game using the Ink scripting language.
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        // Singleton instance of the DialogueManager
        private static DialogueManager instance;
        private InkExternal inkExternal;
        // The Ink story object
        private Story story;


        // Tag used for identifying speakers in dialogue
        private const string SPEAKER_TAG = "speaker";        

        // Coroutine for displaying dialogue lines
        private Coroutine displayCoroutine;

        // Flags for tracking dialogue state
        private bool isPlaying = false;
        private bool isPilih = false;
        private bool canContinueLine = false;

        // Public getters for the dialogue state flags
        public bool GetPlaying() => isPlaying;
        public bool GetPilih() => isPilih;
        public bool GetContinueLine() => canContinueLine;

        [Header("Dialog UI")]
        [SerializeField] InteraksiNPC interaksiNPC;
        [SerializeField] Text teks;
        [SerializeField] Text speaker;
        [SerializeField] GameObject panel_dialogue, panel_explore, panel_ui;
        [SerializeField] GameObject _cameraMain, _cameraTalk;
        [SerializeField] GameObject _continueIcon;

        [Header("Transisi Dialogue")]
        [SerializeField] Animator transisi;

        [Header("Choice UI")]
        [SerializeField] GameObject[] choices;
        [SerializeField] GameObject[] panel_value;
        [SerializeField] Sprite[] icon_choice;
        [SerializeField] Image choiseIcon;
        private Text[] choiseText;
        private int value_npc;
        
        private void Awake()
        {
            if (instance != null)
            {
                Debug.Log("Instance Sudah Ada");
            }
            instance = this;            
        }

        // Returns the singleton instance of DialogueManager
        public static DialogueManager GetInstance()
        {
            return instance;
        }

        void Start()
        {
            // Initialize the choice text array
            choiseText = new Text[choices.Length];
            int index = 0;
            foreach (GameObject choice in choices)
            {
                choiseText[index] = choice.GetComponentInChildren<Text>();
                index++;
            }

            inkExternal = GetComponent<InkExternal>();
        }

        void Update()
        {
            // Check if dialogue is currently playing
            if (!isPlaying)
            {
                return;
            }
        }

        // Enter dialogue mode and start a new conversation
        public void EnterDialogMode(TextAsset inkJSON, int value)
        {
            if (story == null) // Check if the story object is not initialized
            {
                InputSystem.GetInstance().LockMouse(true);
                ExploreManager.GetInstance().SetIsPause(true);
                story = new Story(inkJSON.text);
                value_npc = value;
                isPlaying = true;
                Invoke("ActivatePanel", 2f);
                _cameraTalk.SetActive(true);
                _cameraMain.SetActive(false);
                
                inkExternal.Bind(story);
                
                ContinueStory();
            }
        }

        // Exit the dialogue mode and reset dialogue state
        private void ExitDialogue()
        {
            inkExternal.Unbind(story);  
            InputSystem.GetInstance().LockMouse(false);    
            ExploreManager.GetInstance().SetIsPause(false);                  
            story = null;
            isPlaying = false;
            teks.text = "";
            panel_dialogue.SetActive(false);
            panel_explore.SetActive(true);
            panel_ui.SetActive(true);
            _cameraTalk.SetActive(false);
            _cameraMain.SetActive(true);
            InputSystem.GetInstance().SetAfterDialogue();
            NPC[] npcs = FindObjectsOfType<NPC>();
            foreach (NPC npc in npcs)
            {
                npc.SetTalk(false);
            }
            NPCQuest[] npcss = FindObjectsOfType<NPCQuest>();
            foreach (NPCQuest nPCQuest in npcss)
            {
                nPCQuest.SetTalk(false);
                transisi.SetTrigger("Dialog");  
            }
        }

        // Continue the story by displaying the next line of dialogue
        public void ContinueStory()
        {
            if (story.canContinue)
            {
                if (displayCoroutine != null)
                {
                    StopCoroutine(displayCoroutine);
                }
                displayCoroutine = StartCoroutine(TypingLine(story.Continue(), 0.05f));
                HandleTags(story.currentTags);
            }
            else
            {
                ExitDialogue();
            }
        }

        // Coroutine for typing out dialogue lines
        private IEnumerator TypingLine(string line, float typingSpeed)
        {
            teks.text = "";
            canContinueLine = false;
            _continueIcon.SetActive(false);
            HideChoice();
            foreach (char letter in line.ToCharArray())
            {
                if (Input.GetMouseButton(0))
                {
                    teks.text = line;
                    break;
                }
                teks.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
            IconHandle();
            DisplayChoice();
            canContinueLine = true;
            _continueIcon.SetActive(true);
        }

        // Handle special tags within dialogue (e.g., speaker tags)
        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTags = tag.Split(':');
                string tagKey = splitTags[0].Trim();
                string tagValue = splitTags[1].Trim();

                switch (tagKey)
                {
                    case SPEAKER_TAG:
                        speaker.text = tagValue;
                        break;                    
                }
            }
        }

        // Hide choice buttons
        private void HideChoice()
        {
            foreach (GameObject choice in choices)
            {
                choice.SetActive(false);
            }
        }

        // Display choice buttons for the current set of choices
        private void DisplayChoice()
        {
            List<Choice> currentChoices = story.currentChoices;

            if (currentChoices.Count > choices.Length)
            {
                Debug.LogError("Kelebihan" + currentChoices.Count);
            }

            int index = 0;

            foreach (Choice choice in currentChoices)
            {
                choices[index].gameObject.SetActive(true);
                choiseText[index].text = choice.text;
                index++;
            }

            for (int i = index; i < choices.Length; i++)
            {
                choices[i].gameObject.SetActive(false);
            }

            foreach (GameObject choice in choices)
            {
                if (choice.activeSelf)
                {
                    isPilih = true;
                }
            }
        }

        // Handle player choice selection
        public void MakeChoice(int choice)
        {
            if (isPilih && canContinueLine)
            {
                story.ChooseChoiceIndex(choice);
                isPilih = false;
                ContinueStory();
                if (choice == 0)
                {
                    OnStoryComplete();
                }
                value_npc = 0;
            }
        }

        //Untuk Handle Icon Di NPC
        void IconHandle()
        {
            switch (value_npc)
            {
                case 1:
                    choiseIcon.sprite = icon_choice[1];
                    break;
                case 2:
                    choiseIcon.sprite = icon_choice[2];
                    break;
                case 3:
                    choiseIcon.sprite = icon_choice[3];
                    break;
                case 4:
                    choiseIcon.sprite = icon_choice[4];
                    break;
                default:
                    choiseIcon.sprite = icon_choice[0];
                    break;
            }
        }
        //Handle NPC Action 
        void OnStoryComplete()
        {
            switch (value_npc)
            {
                case 1:
                    ExitDialogue();
                    panel_value[0].SetActive(true);
                    break;
                case 2:
                    ExitDialogue();
                    panel_value[1].SetActive(true);
                    break;
                case 3:
                    ExitDialogue();
                    panel_value[2].SetActive(true);
                    break;
                case 4:
                    ExitDialogue();
                    panel_value[3].SetActive(true);
                    break;
                default:
                    Debug.Log("Bukan NPC Action");
                    break;
            }
        }
        // Activate the dialogue panel and deactivate the exploration panel
        void ActivatePanel()
        {
            panel_dialogue.SetActive(true);
            panel_ui.SetActive(false);
            panel_explore.SetActive(false);
        }
    }
}