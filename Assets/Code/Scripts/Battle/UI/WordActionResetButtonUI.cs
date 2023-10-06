namespace Nivandria.Battle.UI
{
    using System.Collections.Generic;
    using UnityEngine;

    public class WordActionResetButtonUI : MonoBehaviour
    {
        public void OnButtonClick()
        {
            List<RandomWordButtonUI> buttonPressedList = WordActionUI.Instance.GetButtonPressedList();

            foreach (var button in buttonPressedList)
            {
                button.SetPressedStatus(false);
                button.ChangeButtonColor();
            }

            WordActionUI.Instance.ResetButtonPressedList();
            WordActionUI.Instance.UpdateInputText();
            WordActionUI.Instance.CheckWord();
        }
    }

}