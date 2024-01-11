namespace Nivandria.Battle.UI
{
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;

    public class RandomWordButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private Image pressedCover;

        private char character;
        private bool isPressed = false;

        public void OnButtonClick()
        {
            isPressed = !isPressed;
            ChangeButtonColor();

            if (isPressed) WordActionUI.Instance.AddButtonPressedList(this);
            else WordActionUI.Instance.RemoveButtonPressedList(this);

            WordActionUI.Instance.CheckWord();
        }

        public void ChangeButtonColor()
        {
            Image buttonImage = GetComponent<Image>();
            Color lightShade = new Color(1f, 1f, 1f, 1f);
            Color darkShade = new Color(0.6f, 0.6f, 0.6f, 1f);

            buttonImage.color = isPressed ? darkShade : lightShade;
        }

        public void SetCharacter(char newCharacter)
        {
            character = newCharacter;
            textMeshProUGUI.text = "";
            textMeshProUGUI.text += newCharacter;
        }

        public void SetPressedStatus(bool status) => isPressed = status;

        public char GetCharacter() => character;
    }
}