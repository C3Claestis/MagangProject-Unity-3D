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

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetActionName().ToUpper();

            button.onClick.AddListener(() => {
                UnitActionSystem.Instance.SetSelectedAction(baseAction);
                GridSystemVisual.Instance.UpdateGridVisual();
            });
        }
    }

}