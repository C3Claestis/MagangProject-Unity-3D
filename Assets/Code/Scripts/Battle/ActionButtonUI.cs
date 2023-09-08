namespace Nivandria.Battle.UI
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using Nivandria.Battle.Action;

    public class ActionButtonUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private Button button;

        public void SetBaseAction(BaseAction baseAction)
        {
            textMeshPro.text = baseAction.GetActionName().ToUpper();
        }
    }

}