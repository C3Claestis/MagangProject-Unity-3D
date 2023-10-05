namespace Nivandria.Battle.WordSystem
{
    using System.Collections.Generic;
    using UnityEngine;

    public class WordManager : MonoBehaviour
    {
        public static WordManager Instance { get; private set; }

        [SerializeField] private TextAsset jsonWordList;

        private AlphabeticalWordList wordList;
        private List<string> currentWordArray;
        private string currentStartCharacter;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one WordManager! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            wordList = JsonUtility.FromJson<AlphabeticalWordList>(jsonWordList.text);
        }

        public bool CheckWord(string word)
        {
            if (word.Length <= 3) return false;

            string newStartCharacter = word.Substring(0, 1);

            if (currentStartCharacter != newStartCharacter)
            {
                currentWordArray = new List<string>();
                currentWordArray = GetWordsStartingWith(newStartCharacter);
                currentStartCharacter = newStartCharacter;
            }

            if (currentWordArray.Contains(word.ToUpper())) return true;

            return false;
        }

        public string GetRandomStringFromAtoZ(int stringLength)
        {
            string randomChar = "";

            for (int i = 0; i < stringLength; i++)
            {
                randomChar += (char)Random.Range('A', 'Z' + 1);
            }

            return randomChar;
        }

        public string GetRandomConsonant(int stringLength)
        {
            string consonants = "BCDFGHJKLMNPQRSTVWXYZ";
            string randomChar = "";

            for (int i = 0; i < stringLength; i++)
            {
                randomChar += consonants[Random.Range(0, consonants.Length)];
            }
            return randomChar;
        }

        public string GetRandomVowel(int stringLength)
        {
            string vowels = "AEIOU";
            string randomChar = "";

            for (int i = 0; i < stringLength; i++)
            {
                randomChar += vowels[Random.Range(0, vowels.Length)];
            }

            return randomChar;
        }

        private List<string> GetWordsStartingWith(string startsWith)
        {
            List<string> wordArray = new List<string>();

            switch (startsWith.ToLower())
            {
                case "a":
                    wordArray = wordList.A;
                    break;
                case "b":
                    wordArray = wordList.B;
                    break;
                case "c":
                    wordArray = wordList.C;
                    break;
                case "d":
                    wordArray = wordList.D;
                    break;
                case "e":
                    wordArray = wordList.E;
                    break;
                case "f":
                    wordArray = wordList.F;
                    break;
                case "g":
                    wordArray = wordList.G;
                    break;
                case "h":
                    wordArray = wordList.H;
                    break;
                case "i":
                    wordArray = wordList.I;
                    break;
                case "j":
                    wordArray = wordList.J;
                    break;
                case "k":
                    wordArray = wordList.K;
                    break;
                case "l":
                    wordArray = wordList.L;
                    break;
                case "m":
                    wordArray = wordList.M;
                    break;
                case "n":
                    wordArray = wordList.N;
                    break;
                case "o":
                    wordArray = wordList.O;
                    break;
                case "p":
                    wordArray = wordList.P;
                    break;
                case "q":
                    wordArray = wordList.Q;
                    break;
                case "r":
                    wordArray = wordList.R;
                    break;
                case "s":
                    wordArray = wordList.S;
                    break;
                case "t":
                    wordArray = wordList.T;
                    break;
                case "u":
                    wordArray = wordList.U;
                    break;
                case "v":
                    wordArray = wordList.V;
                    break;
                case "w":
                    wordArray = wordList.W;
                    break;
                case "x":
                    wordArray = wordList.X;
                    break;
                case "y":
                    wordArray = wordList.Y;
                    break;
                case "z":
                    wordArray = wordList.Z;
                    break;
                default:
                    Debug.LogError("Can't get starting word list");
                    break;
            }

            return wordArray;
        }

    }
}