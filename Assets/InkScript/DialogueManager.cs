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
        [SerializeField] GameObject panel_dialogue, panel_explore;
        [SerializeField] GameObject _cameraMain, _cameraTalk;

        [Header("Choice UI")]
        [SerializeField] GameObject[] choices;
        private Text[] choiseText;

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
        public void EnterDialogMode(TextAsset inkJSON)
        {
            if (story == null) // Check if the story object is not initialized
            {
                story = new Story(inkJSON.text);
                isPlaying = true;
                Invoke("ActivatePanel", 0.7f);
                _cameraTalk.SetActive(true);
                _cameraMain.SetActive(false);
                ContinueStory();
            }
        }

        // Exit the dialogue mode and reset dialogue state
        private void ExitDialogue()
        {
            story = null;
            isPlaying = false;
            teks.text = "";
            panel_dialogue.SetActive(false);
            panel_explore.SetActive(true);
            _cameraTalk.SetActive(false);
            _cameraMain.SetActive(true);
            InputSystem.GetInstance().SetAfterDialogue();
            NPC[] npcs = FindObjectsOfType<NPC>();
            foreach (NPC npc in npcs)
            {
                npc.SetTalk(false);
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
            HideChoice();
            foreach (char letter in line.ToCharArray())
            {
                teks.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            DisplayChoice();
            canContinueLine = true;
        }

        // Handle special tags within dialogue (e.g., speaker tags)
        private void HandleTags(List<string> currentTags)
        {
            foreach (string tag in currentTags)
            {
                string[] splitTags = tag.Split(':');
                if (splitTags.Length != 2)
                {
                    Debug.Log("Tag tidak ada " + tag);
                }
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
            }
        }

        // Activate the dialogue panel and deactivate the exploration panel
        void ActivatePanel()
        {
            panel_dialogue.SetActive(true);
            panel_explore.SetActive(false);
        }
    }
}