namespace Nivandria.Battle.Action
{
    using System.Collections.Generic;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.Grid;
    using UnityEngine;
    using Nivandria.Battle.UnitSystem;
    using System;
    using Unity.VisualScripting;
    using UnityEngine.Analytics;

    public class WordAction : BaseSkillAction
    {
        protected override string actionName => "Wording Skill";
        protected override string actionDescription => "Sacra Unique Skill";
        protected override ActionCategory actionCategory => ActionCategory.Skill;
        protected override ActionType actionType => ActionType.Magical;
        private GridPosition currentUnitGridPosition;
        private FacingDirection currentDirection;

        private List<GridPosition> currentAllGridList;
        private List<GridPosition> currentActiveGridList;
        private List<GridPosition> currentEastGridList;
        private List<GridPosition> currentWestGridList;
        private List<GridPosition> currentNorthGridList;
        private List<GridPosition> currentSouthGridList;

        private void Start()
        {
            currentActiveGridList = new List<GridPosition>();
            Pointer.Instance.OnPointerGridChanged += Pointer_OnPointerGridChanged;
        }

        private void Pointer_OnPointerGridChanged(object sender, EventArgs e)
        {
            GridPosition pointerGridPosition = Pointer.Instance.GetCurrentGrid();
            bool gridInsideList = GridChecker(currentActiveGridList, pointerGridPosition);

            if (gridInsideList) return;

            if (currentDirection != FacingDirection.EAST && GridChecker(currentEastGridList, pointerGridPosition))
            {
                currentDirection = FacingDirection.EAST;
                currentActiveGridList = currentEastGridList;
            }
            else if (currentDirection != FacingDirection.WEST && GridChecker(currentWestGridList, pointerGridPosition))
            {
                currentDirection = FacingDirection.WEST;
                currentActiveGridList = currentWestGridList;
            }
            else if (currentDirection != FacingDirection.NORTH && GridChecker(currentNorthGridList, pointerGridPosition))
            {
                currentDirection = FacingDirection.NORTH;
                currentActiveGridList = currentNorthGridList;
            }
            else if (currentDirection != FacingDirection.SOUTH && GridChecker(currentSouthGridList, pointerGridPosition))
            {
                currentDirection = FacingDirection.SOUTH;
                currentActiveGridList = currentSouthGridList;
            }
            else return;

            GridSystemVisual.Instance.UpdateGridVisual();
        }

        private bool GridChecker(List<GridPosition> gridList, GridPosition targetGrid)
        {
            if (gridList == null) return false;

            foreach (GridPosition gridPosition in gridList)
            {
                if (gridPosition == targetGrid) return true;
            }

            return false;
        }

        public override List<GridPosition> GetValidActionGridPosition()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            SetupActionGridPosition();

            switch (currentDirection)
            {
                case FacingDirection.EAST:
                    validGridList = currentEastGridList;
                    break;

                case FacingDirection.WEST:
                    validGridList = currentWestGridList;
                    break;

                case FacingDirection.NORTH:
                    validGridList = currentNorthGridList;
                    break;

                case FacingDirection.SOUTH:
                    validGridList = currentSouthGridList;
                    break;
                default:
                    Debug.LogError($"Can't find current direction for unit {unit.GetCharacterName()}, action {actionName}");
                    break;

            }


            return validGridList;
        }

        public override List<GridPosition> GetRangeActionGridPosition()
        {
            SetupActionGridPosition();
            return currentAllGridList;
        }

        public void SetupActionGridPosition()
        {
            GridPosition newUnitGridPosition = unit.GetGridPosition();

            if (newUnitGridPosition != currentUnitGridPosition)
            {

                currentAllGridList = new List<GridPosition>();
                currentEastGridList = new List<GridPosition>();
                currentWestGridList = new List<GridPosition>();
                currentNorthGridList = new List<GridPosition>();
                currentSouthGridList = new List<GridPosition>();

                currentUnitGridPosition = newUnitGridPosition;
                Pathfinding.Instance.SetupPath(true);
                currentDirection = unit.GetFacingDirection();

                currentEastGridList.AddRange(GetEastActionGrid());
                currentWestGridList.AddRange(GetWestActionGrid());
                currentNorthGridList.AddRange(GetNorthActionGrid());
                currentSouthGridList.AddRange(GetSouthActionGrid());

                currentAllGridList.AddRange(currentEastGridList);
                currentAllGridList.AddRange(currentWestGridList);
                currentAllGridList.AddRange(currentNorthGridList);
                currentAllGridList.AddRange(currentSouthGridList);
            }
        }

        public List<GridPosition> GetEastActionGrid()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            LevelGrid levelGrid = LevelGrid.Instance;

            for (int zOffset = -1; zOffset <= 1; zOffset++)
            {
                GridPosition testZGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);
                if (!levelGrid.IsValidGridPosition(testZGridPosition)) continue;

                for (int xOffset = 1; xOffset < levelGrid.GetGridWidth(); xOffset++)
                {
                    GridPosition testGridPosition = new GridPosition(testZGridPosition.x + xOffset, testZGridPosition.z);
                    if (!levelGrid.IsValidGridPosition(testGridPosition)) break;

                    Vector3 testWorldPosition = levelGrid.GetWorldPosition(testGridPosition);
                    if (Pathfinding.Instance.IsObstacleOnGrid(testWorldPosition, out string objectTag) && objectTag == "Tier1_Obstacles") break;

                    validGridList.Add(testGridPosition);
                }
            }

            return validGridList;
        }

        public List<GridPosition> GetWestActionGrid()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            LevelGrid levelGrid = LevelGrid.Instance;
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int zOffset = -1; zOffset <= 1; zOffset++)
            {
                GridPosition testZGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);
                if (!levelGrid.IsValidGridPosition(testZGridPosition)) continue;

                for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
                {
                    GridPosition testGridPosition = new GridPosition(testZGridPosition.x + xOffset, testZGridPosition.z);
                    if (!levelGrid.IsValidGridPosition(testGridPosition)) break;

                    Vector3 testWorldPosition = levelGrid.GetWorldPosition(testGridPosition);
                    if (Pathfinding.Instance.IsObstacleOnGrid(testWorldPosition, out string objectTag) && objectTag == "Tier1_Obstacles") break;

                    validGridList.Add(testGridPosition);
                }
            }

            return validGridList;
        }

        public List<GridPosition> GetNorthActionGrid()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            LevelGrid levelGrid = LevelGrid.Instance;
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {

                GridPosition testXGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);
                if (!levelGrid.IsValidGridPosition(testXGridPosition)) continue;

                for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
                {
                    GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);
                    if (!levelGrid.IsValidGridPosition(testGridPosition)) break;

                    Vector3 testWorldPosition = levelGrid.GetWorldPosition(testGridPosition);
                    if (Pathfinding.Instance.IsObstacleOnGrid(testWorldPosition, out string objectTag) && objectTag == "Tier1_Obstacles") break;

                    validGridList.Add(testGridPosition);
                }
            }

            return validGridList;
        }

        public List<GridPosition> GetSouthActionGrid()
        {
            List<GridPosition> validGridList = new List<GridPosition>();
            LevelGrid levelGrid = LevelGrid.Instance;
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int xOffset = -1; xOffset <= 1; xOffset++)
            {
                GridPosition testXGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);
                if (!levelGrid.IsValidGridPosition(testXGridPosition)) continue;

                for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
                {
                    GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);
                    if (!levelGrid.IsValidGridPosition(testGridPosition)) break;

                    Vector3 testWorldPosition = levelGrid.GetWorldPosition(testGridPosition);
                    if (Pathfinding.Instance.IsObstacleOnGrid(testWorldPosition, out string objectTag) && objectTag == "Tier1_Obstacles") break;

                    validGridList.Add(testGridPosition);
                }

            }

            return validGridList;
        }
    }
}