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

        public void SetBaseAction(BaseAction baseAction)
        {
            this.baseAction = baseAction;
            textMeshPro.text = baseAction.GetActionName().ToUpper();

            button.onClick.AddListener(() =>
            {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
                GridSystemVisual.Instance.UpdateGridVisual();
            });

            UpdateSelectedVisual();
        }

        public void UpdateSelectedVisual()
        {
            BaseAction selectedBaseAction = UnitActionSystem.Instance.GetSelectedAction();
	        outline.enabled = (baseAction == selectedBaseAction);
        }

    }

}