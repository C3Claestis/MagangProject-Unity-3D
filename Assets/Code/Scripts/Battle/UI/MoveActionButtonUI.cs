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
            MoveAction moveAction = unit.GetAction<MoveAction>();

            UnitActionSystem.Instance.SetSelectedAction(moveAction);
            GridSystemVisual.Instance.UpdateGridVisual();

            if (UnitTurnSystem.Instance.GetSelectedUnit().GetActionStatus(moveAction.GetActionCategory())) return;

            moveAction.InitPathToTarget();            
            
            UnitActionSystem.Instance.HideActionUI();
            Pointer.Instance.SetActive(true);
            moveAction.InitializeCancel();

            PlayerInputController.Instance.SetActionMap("Gridmap");
            eventSystem.SetSelectedGameObject(null, new BaseEventData(eventSystem));
        }
    }

}