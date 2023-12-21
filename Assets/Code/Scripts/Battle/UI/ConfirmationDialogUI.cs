namespace Nivandria.Battle.UI
{
    using System;
    using TMPro;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class ConfirmationDialogUI : MonoBehaviour
    {
        [SerializeField] Button firstButton;
        [SerializeField] TextMeshProUGUI TextInside;

        private Action onYesButtonSelected;
        private Action onNoButtonSelected;

        private void Start()
        {
            var eventSystem = EventSystem.current;
            eventSystem.SetSelectedGameObject(firstButton.gameObject, new BaseEventData(eventSystem));
            PlayerInputController.Instance.OnCancelUIPressed += PlayerInputController_OnCancelPressed;
        }

        public void SetupWord(string text)
        {
            TextInside.text = text;
        }

        private void OnDestroy()
        {
            PlayerInputController.Instance.OnCancelUIPressed -= PlayerInputController_OnCancelPressed;
        }

        public void InitializeConfirmationButton(Action onYesButtonSelected, Action onNoButtonSelected)
        {

            this.onYesButtonSelected = onYesButtonSelected;
            this.onNoButtonSelected = onNoButtonSelected;
        }

        public void yesButtonClick()
        {
            onYesButtonSelected();
            Destroy(gameObject);
        }

        public void noButtonClick()
        {
            onNoButtonSelected();
            Destroy(gameObject);
        }

        private void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            noButtonClick();
        }
    }
}