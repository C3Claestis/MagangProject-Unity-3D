namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Grid;
    using UnityEngine;
    using Nivandria.Battle.Action;

    public class WordActionConfirmButtonUI : MonoBehaviour
    {
        public void OnButtonClick()
        {
            var selectedAction = (WordAction)UnitActionSystem.Instance.GetSelectedAction();
            string word = WordActionUI.Instance.GetInpuString();

            selectedAction.SetupGridPatern(word);
            selectedAction.InitializeCancel();

            Pointer.Instance.SetActive(true);
            GridSystemVisual.Instance.UpdateGridVisual();
            WordActionUI.Instance.HideUI(true);

            WordActionUI.Instance.LinkCancel(false);

            PlayerInputController.Instance.SetActionMap("Gridmap");
            UnitActionSystemUI.Instance.SetSelectedGameObject(null);
        }
    }

}