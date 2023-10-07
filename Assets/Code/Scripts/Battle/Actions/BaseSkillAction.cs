namespace Nivandria.Battle.Action
{
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.UnitSystem;

    public abstract class BaseSkillAction : BaseAction
    {

        protected override void CancelAction()
        {
            GridPosition gridPosition = unit.GetGridPosition();

            GridSystemVisual.Instance.HideAllGridPosition();

            var buttonTransform = UnitActionSystemUI.Instance.GetActionButton(this);
            UnitActionSystemUI.Instance.SetSelectedGameObject(buttonTransform.gameObject);

            UnitActionSystem.Instance.ShowActionUI();
            var skillActionButtonContainerUI = buttonTransform.GetComponent<SkillActionButtonUI>().GetActionContainer();
            skillActionButtonContainerUI.LinkCancel(true);

            Pointer.Instance.SetPointerOnGrid(gridPosition);
            CameraController.Instance.SetCameraFocusToPosition(LevelGrid.Instance.GetWorldPosition(gridPosition));

            PlayerInputController.Instance.SetActionMap("BattleUI");

            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        public abstract List<GridPosition> GetRangeActionGridPosition();

    }
}