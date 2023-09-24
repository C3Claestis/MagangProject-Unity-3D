namespace Nivandria.Battle.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using TMPro;
    using UnityEngine.EventSystems;

    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;
        [SerializeField] private Outline outline;
        

        private BaseAction baseAction;

        /// <summary> Sets the base action and updates the UI accordingly. </summary>
        /// <param name="baseAction">The base action to set.</param>
        public void SetBaseAction(BaseAction baseAction)
        {
            this.baseAction = baseAction;
            EventSystem eventSystem = EventSystem.current;
            textMeshPro.text = baseAction.GetName().ToUpper();

            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
                GridSystemVisual.Instance.UpdateGridVisual();

                if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(baseAction.GetActionType())) return;

                UnitActionSystem.Instance.SetBusyUI();
                baseAction.InitializeCancel();
                PlayerInputController.Instance.SetActionMap("Gridmap");
                Pointer.Instance.SetActive(true);
                eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
            });
        }

        public BaseAction GetBaseAction() => baseAction;

        /// <summary> Updates the visual state of the UI based on the selected base action. </summary>
        public void UpdateUISelectedVisual()
        {

        }

    }
}