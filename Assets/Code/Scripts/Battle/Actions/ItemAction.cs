namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;

    public class ItemAction : BaseAction
    {
        protected override string actionName => itemName;
        protected override string actionDescription => itemDescription;
        protected override ActionCategory actionCategory => ActionCategory.Item;
        protected override ActionType actionType => ActionType.Other;

        private string itemName;
        private string itemDescription;


        public void SetupItemAction(string itemName, string itemDescription)
        {
            this.itemName = itemName;
            this.itemDescription = itemDescription;
        }

        public override void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            base.TakeAction(gridPosition, onActionComplete);
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