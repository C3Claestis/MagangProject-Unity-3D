namespace Nivandria.Battle.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using TMPro;

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
            textMeshPro.text = baseAction.GetName().ToUpper();

            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
                GridSystemVisual.Instance.UpdateGridVisual();
            });

            UpdateUISelectedVisual();
        }

        /// <summary> Updates the visual state of the UI based on the selected base action. </summary>
        public void UpdateUISelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
            outline.enabled = (baseAction == selectedBaseAction);
        }

    }
}