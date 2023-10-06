namespace Nivandria.Battle.UI
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.WordSystem;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class WordActionUI : MonoBehaviour
    {
        public static WordActionUI Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private Transform confirmButtonTransform;
        [SerializeField] private Transform cancelButtonTransform;
        [SerializeField] private Transform resetButtonTransform;
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
            // HideUI(true);

            inputText.text = "";
            inputString = "";
        }

        public void NewButtons()
        {
            UnitTurnSystem_OnSelectedUnitChanged(null, null);
        }

        public void UpdateInputText()
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
                ChangeInputColor(false);
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
            DisableConfirmButton(!isFound);
            ChangeInputColor(isFound);
        }

        private void ChangeInputColor(bool status)
        {
            if (status == true) inputText.color = Color.green;
            else inputText.color = Color.red;
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

        public void ResetButtonPressedList() => buttonPressedList.Clear();

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

        public void SetupButtonNavigation()
        {
            int listLength = wordButtonList.Count;

            Button resetButton = resetButtonTransform.GetComponent<Button>();
            Navigation resetButtonNavigation = resetButton.navigation;
            resetButtonNavigation.mode = Navigation.Mode.Explicit;

            Button cancelButton = cancelButtonTransform.GetComponent<Button>();
            Navigation cancelButtonNavigation = cancelButton.navigation;
            cancelButtonNavigation.mode = Navigation.Mode.Explicit;

            Button confirmButton = confirmButtonTransform.GetComponent<Button>();
            Navigation confirmButtonNavigation = confirmButton.navigation;
            confirmButtonNavigation.mode = Navigation.Mode.Explicit;


            for (int i = 0; i < listLength; i++)
            {
                Button button = wordButtonList[i].GetComponent<Button>();
                Navigation newNavigation = button.navigation;
                newNavigation.mode = Navigation.Mode.Explicit;

                switch (i)
                {
                    case 0:
                        newNavigation.selectOnUp = resetButton;
                        resetButtonNavigation.selectOnDown = button;
                        newNavigation.selectOnDown = wordButtonList[i + 5].GetComponent<Button>();
                        break;
                    case 1:
                    case 2:
                        newNavigation.selectOnUp = cancelButton;
                        cancelButtonNavigation.selectOnDown = button;
                        newNavigation.selectOnDown = wordButtonList[i + 5].GetComponent<Button>();
                        break;
                    case 3:
                    case 4:
                        newNavigation.selectOnUp = confirmButton;
                        confirmButtonNavigation.selectOnDown = button;
                        newNavigation.selectOnDown = wordButtonList[i + 5].GetComponent<Button>();
                        break;
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                        newNavigation.selectOnUp = wordButtonList[i - 5].GetComponent<Button>();
                        newNavigation.selectOnDown = wordButtonList[i + 5].GetComponent<Button>();
                        break;
                    case 10:
                        newNavigation.selectOnDown = resetButton;
                        resetButtonNavigation.selectOnUp = button;
                        break;
                    case 11:
                    case 12:
                        newNavigation.selectOnDown = cancelButton;
                        cancelButtonNavigation.selectOnUp = button;
                        break;
                    case 13:
                    case 14:
                        newNavigation.selectOnDown = confirmButton;
                        confirmButtonNavigation.selectOnUp = button;
                        break;
                }

                if (i >= 10 && i <= 14)
                {
                    newNavigation.selectOnUp = wordButtonList[i - 5].GetComponent<Button>();
                }

                if (i % 5 == 0)
                {
                    newNavigation.selectOnLeft = wordButtonList[i + 4].GetComponent<Button>();
                    newNavigation.selectOnRight = wordButtonList[i + 1].GetComponent<Button>();
                }
                else if (i % 5 == 4)
                {
                    newNavigation.selectOnRight = wordButtonList[i - 4].GetComponent<Button>();
                    newNavigation.selectOnLeft = wordButtonList[i - 1].GetComponent<Button>();
                }
                else
                {
                    newNavigation.selectOnRight = wordButtonList[i + 1].GetComponent<Button>();
                    newNavigation.selectOnLeft = wordButtonList[i - 1].GetComponent<Button>();
                }

                button.navigation = newNavigation;
            }
            resetButtonNavigation.selectOnLeft = confirmButton;
            resetButtonNavigation.selectOnRight = cancelButton;
            cancelButtonNavigation.selectOnLeft = resetButton;
            cancelButtonNavigation.selectOnRight = confirmButton;
            confirmButtonNavigation.selectOnLeft = cancelButton;
            confirmButtonNavigation.selectOnRight = resetButton;

            resetButton.navigation = resetButtonNavigation;
            cancelButton.navigation = cancelButtonNavigation;
            confirmButton.navigation = confirmButtonNavigation;
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
            SetupButtonNavigation();
        }
    }

}