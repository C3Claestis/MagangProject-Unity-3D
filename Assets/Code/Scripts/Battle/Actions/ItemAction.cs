namespace Nivandria.Battle.Action
{
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UI;
    using System.Collections.Generic;
    using System;

    public class ItemAction : BaseAction
    {
        protected override string actionName => itemName;
        protected override string actionDescription => itemDescription;
        protected override ActionCategory actionCategory => ActionCategory.Item;
        protected override ActionType actionType => ActionType.Other;

        private string itemName;
        private string itemDescription;
        private int itemAmout = 0;

        public void SetupItemAction(string itemName, int itemAmout, string itemDescription, ItemActionButtonContainerUI itemActionButtonContainerUI)
        {
            this.itemName = itemName;
            this.itemAmout = itemAmout;
            this.itemDescription = itemDescription;
            gameObject.name = itemName;

            var itemButton = GetComponent<ItemActionButtonUI>();
            itemButton.InitializeActionButton(this, itemActionButtonContainerUI);
        }

        public void SetUnit(Unit newUnit) => unit = newUnit;

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            base.TakeAction(gridPosition, onActionComplete);
            SetActive(false);

            if (unit.IsEnemy()) YesButtonAction();
            else UnitActionSystemUI.Instance.InitializeConfirmationButton(YesButtonAction, NoButtonAction);
        }

        protected override void CancelAction()
        {
            GridPosition gridPosition = unit.GetGridPosition();

            GridSystemVisual.Instance.HideAllGridPosition();

            var buttonTransform = UnitActionSystemUI.Instance.GetItemButton(GetComponent<ItemAction>());
            UnitActionSystemUI.Instance.SetSelectedGameObject(buttonTransform.gameObject);

            UnitActionSystem.Instance.ShowActionUI();
            var itemActionButtonContainerUI = buttonTransform.GetComponent<ItemActionButtonUI>().GetActionContainer();
            itemActionButtonContainerUI.LinkCancel(false);
            itemActionButtonContainerUI.LinkCancel(true);

            Pointer.Instance.SetPointerOnGrid(gridPosition);
            CameraController.Instance.SetCameraFocusToPosition(LevelGrid.Instance.GetWorldPosition(gridPosition));

            PlayerInputController.Instance.SetActionMap("BattleUI");

            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            List<GridPosition> rangeGridList = new List<GridPosition>();

            foreach (GridPosition grid in GetRangeActionGridPosition())
            {
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(grid))
                {
                    validGridList.Add(grid);
                    continue;
                }

                rangeGridList.Add(grid);
            }

            GridSystemVisual.Instance.ShowGridPositionList(rangeGridList, GridVisualType.BlueSoft);
            return validGridList;
        }

        public List<GridPosition> GetRangeActionGridPosition()
        {
            List<GridPosition> actionGridList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            GridPosition[] offsetGridPositionArray = new GridPosition[5];
            offsetGridPositionArray[0] = new GridPosition(0, 0);
            offsetGridPositionArray[1] = new GridPosition(1, 0);
            offsetGridPositionArray[2] = new GridPosition(-1, 0);
            offsetGridPositionArray[3] = new GridPosition(0, 1);
            offsetGridPositionArray[4] = new GridPosition(0, -1);

            foreach (var offsetGridPosition in offsetGridPositionArray)
            {
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;

                actionGridList.Add(testGridPosition);
            }

            return actionGridList;
        }
    }
}