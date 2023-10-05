namespace Nivandria.Battle.UI
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.WordSystem;
    using TMPro;
    using UnityEditor.PackageManager;
    using UnityEngine;
    using UnityEngine.UI;

    public class WordActionUI : MonoBehaviour
    {
        public static WordActionUI Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private Transform confirmButtonTransform;
        [SerializeField] private Transform cancelButtonTransform;
        [SerializeField] private Transform wordButtonContainer;
        [SerializeField] private Transform wordButtonPrefab;
        [SerializeField] private TextAsset wordLibraryJson;

        private List<Transform> wordButtonList;
        private List<RandomWordButtonUI> buttonPressedList;

        private AlphabeticalWordList wordLibrary;
        private List<string> currentWordArray;
        private CanvasGroup canvasGroup;
        private string currentStartCharacter;
        private string inputString;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one WordActionUI! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            UnitTurnSystem.Instance.OnSelectedUnitChanged += UnitTurnSystem_OnSelectedUnitChanged;
            wordLibrary = JsonUtility.FromJson<AlphabeticalWordList>(wordLibraryJson.text);
            canvasGroup = GetComponent<CanvasGroup>();
            HideUI(true);

            inputText.text = "";
            inputString = "";
        }

        private void Update() // ! TEMPORARY
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                NewButtons();
            }
        }

        public void NewButtons()
        {
            UnitTurnSystem_OnSelectedUnitChanged(null, null);
        }

        private void UpdateInputText()
        {
            inputText.text = "";
            inputString = "";

            foreach (var button in buttonPressedList)
            {
                inputString += button.GetCharacter();
            }

            inputText.text = inputString;
        }

        public void HideUI(bool status)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            if (status == false) return;

            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void CheckWord()
        {
            if (inputString.Length < 3)
            {
                DisableConfirmButton(true);
                return;
            }

            string newStartCharacter = inputString.Substring(0, 1);

            if (currentStartCharacter != newStartCharacter)
            {
                currentWordArray = new List<string>();
                currentWordArray = GetWordsStartingWith(newStartCharacter);
                currentStartCharacter = newStartCharacter;
            }

            bool isFound = currentWordArray.Contains(inputString);
            Debug.Log("isFound : " + isFound);
            DisableConfirmButton(!isFound);
        }

        private void DisableConfirmButton(bool status)
        {
            Button button = confirmButtonTransform.GetComponent<Button>();
            if (status == true) button.interactable = false;
            else button.interactable = true;
        }

        public char GetRandomCharFromAtoZ()
        {
            char randomChar = (char)Random.Range('A', 'Z' + 1);
            return randomChar;
        }

        public char GetRandomConsonant()
        {
            string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
            char randomConsonant = consonants[Random.Range(0, consonants.Length)];
            return randomConsonant;
        }

        public char GetRandomVowel()
        {
            string vowels = "AEIOU";
            char randomVowel = vowels[Random.Range(0, vowels.Length)];
            return randomVowel;
        }

        private List<string> GetWordsStartingWith(string startsWith)
        {
            List<string> wordArray = new List<string>();

            switch (startsWith.ToLower())
            {
                case "a":
                    wordArray = wordLibrary.A;
                    break;
                case "b":
                    wordArray = wordLibrary.B;
                    break;
                case "c":
                    wordArray = wordLibrary.C;
                    break;
                case "d":
                    wordArray = wordLibrary.D;
                    break;
                case "e":
                    wordArray = wordLibrary.E;
                    break;
                case "f":
                    wordArray = wordLibrary.F;
                    break;
                case "g":
                    wordArray = wordLibrary.G;
                    break;
                case "h":
                    wordArray = wordLibrary.H;
                    break;
                case "i":
                    wordArray = wordLibrary.I;
                    break;
                case "j":
                    wordArray = wordLibrary.J;
                    break;
                case "k":
                    wordArray = wordLibrary.K;
                    break;
                case "l":
                    wordArray = wordLibrary.L;
                    break;
                case "m":
                    wordArray = wordLibrary.M;
                    break;
                case "n":
                    wordArray = wordLibrary.N;
                    break;
                case "o":
                    wordArray = wordLibrary.O;
                    break;
                case "p":
                    wordArray = wordLibrary.P;
                    break;
                case "q":
                    wordArray = wordLibrary.Q;
                    break;
                case "r":
                    wordArray = wordLibrary.R;
                    break;
                case "s":
                    wordArray = wordLibrary.S;
                    break;
                case "t":
                    wordArray = wordLibrary.T;
                    break;
                case "u":
                    wordArray = wordLibrary.U;
                    break;
                case "v":
                    wordArray = wordLibrary.V;
                    break;
                case "w":
                    wordArray = wordLibrary.W;
                    break;
                case "x":
                    wordArray = wordLibrary.X;
                    break;
                case "y":
                    wordArray = wordLibrary.Y;
                    break;
                case "z":
                    wordArray = wordLibrary.Z;
                    break;
                default:
                    Debug.LogError("Can't get starting word list");
                    break;
            }

            return wordArray;
        }

        private void DestroyRandomWordButtons()
        {
            for (int i = 0; i < wordButtonContainer.childCount; i++)
            {
                Transform buttonTransform = wordButtonContainer.GetChild(i);
                Destroy(buttonTransform.gameObject);
            }
        }

        private void CreateRandomWordButtons()
        {
            int wordButtonLength = 15;
            List<char> randomCharList = new List<char>();

            for (int i = 0; i < wordButtonLength - 3; i++)
            {
                randomCharList.Add(GetRandomCharFromAtoZ());
                if (i == 3 || i == 7 || i == 10) randomCharList.Add(GetRandomVowel());
            }

            for (int i = 0; i < wordButtonLength; i++)
            {
                Transform wordButtonTransform = Instantiate(wordButtonPrefab, wordButtonContainer);
                var wordButtonUI = wordButtonTransform.GetComponent<RandomWordButtonUI>();

                wordButtonUI.SetCharacter(randomCharList[0]);
                randomCharList.RemoveAt(0);
                wordButtonList.Add(wordButtonTransform);
            }
        }

        public List<RandomWordButtonUI> GetButtonPressedList() => buttonPressedList;

        public void AddButtonPressedList(RandomWordButtonUI buttonUI)
        {
            buttonPressedList.Add(buttonUI);
            UpdateInputText();
        }

        public void RemoveButtonPressedList(RandomWordButtonUI buttonUI)
        {
            buttonPressedList.Remove(buttonUI);
            UpdateInputText();
        }

        private void UnitTurnSystem_OnSelectedUnitChanged(object sender, System.EventArgs e)
        {
            string unitName = UnitTurnSystem.Instance.GetSelectedUnit().GetCharacterName();

            if (unitName != "Sacra") return;

            wordButtonList = new List<Transform>();
            buttonPressedList = new List<RandomWordButtonUI>();
            DisableConfirmButton(true);
            inputText.text = "";
            inputString = "";

            DestroyRandomWordButtons();
            CreateRandomWordButtons();
        }
    }

}