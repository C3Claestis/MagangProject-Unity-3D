namespace Nivandria.Battle.UI
{
    using Nivandria.Battle.Action;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine.EventSystems;

    public class MoveActionButtonUI : BaseActionButtonUI
    {
        protected override ActionCategory actionCategory => ActionCategory.Move;
        
        public override void ButtonOnClick()
        {
            EventSystem eventSystem = EventSystem.current;
            BaseAction baseAction = unit.GetAction<MoveAction>();

            UnitActionSystem.Instance.SetSelectedAction(baseAction);
            GridSystemVisual.Instance.UpdateGridVisual();

            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(baseAction.GetActionCategory())) return;

            UnitActionSystem.Instance.SetBusyUI();
            Pointer.Instance.SetActive(true);
            baseAction.InitializeCancel();

            PlayerInputController.Instance.SetActionMap("Gridmap");
            eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
        }
    }

}