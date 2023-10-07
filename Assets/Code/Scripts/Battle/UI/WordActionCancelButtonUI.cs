namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.Action;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;

    public class WordActionCancelButtonUI : MonoBehaviour
    {
        public void ButtonOnClick()
        {
            WordActionUI.Instance.CancelAction();
        }
    }

}