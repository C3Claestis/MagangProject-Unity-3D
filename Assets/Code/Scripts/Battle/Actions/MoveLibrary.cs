namespace Nivandria.Battle.Action
{
    using System.Collections.Generic;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;

    public class MoveLibrary
    {
        private int maxMoveDistance;
        private Unit unit;

        public MoveLibrary(Unit unit, int maxMoveDistance)
        {
            this.unit = unit;
            this.maxMoveDistance = maxMoveDistance;
        }

        public List<GridPosition> NormalMoveValidGrids()
        {
            List<GridPosition> normalMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                    int PathfindingDistanceMultiplier = 10;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                    if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;
                    if (!Pathfinding.Instance.HasPath(unitGridPosition, testGridPosition)) continue;

                    if (Pathfinding.Instance.GetPathLength(unitGridPosition, testGridPosition)
                        > maxMoveDistance * PathfindingDistanceMultiplier)
                    {
                        continue;
                    }

                    normalMoveList.Add(testGridPosition);
                }
            }

            return normalMoveList;
        }

        public List<GridPosition> KingMoveValidGrids()
        {
            List<GridPosition> kingMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int maxMoveDistance = 1;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;

                    kingMoveList.Add(testGridPosition);
                }
            }

            return kingMoveList;
        }

        public List<GridPosition> TigerMoveValidGrids()
        {
            List<GridPosition> tigerMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int maxMoveDistance = 3;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;

                    // Check if the test position is within the move distance and not adjacent to other units
                    if (testDistance > 1 && testDistance <= maxMoveDistance &&
                        !LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        tigerMoveList.Add(testGridPosition);
                    }
                }
            }

            return tigerMoveList;
        }

        public List<GridPosition> BullMoveValidGrids()
        {
            List<GridPosition> bullMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            // Horizontal movement to the right
            for (int xOffset = 1; xOffset < LevelGrid.Instance.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                bullMoveList.Add(testGridPosition);
            }

            // Horizontal movement to the left
            for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement upwards
            for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement downwards
            for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                bullMoveList.Add(testGridPosition);
            }

            return bullMoveList;
        }

        public List<GridPosition> SnakeMoveValidGrids()
        {
            List<GridPosition> snakeMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int gridWidth = LevelGrid.Instance.GetGridWidth();
            int gridHeight = LevelGrid.Instance.GetGridHeight();

            // Diagonal movement to the upper right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the upper left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break; // Stop if obstacle

                snakeMoveList.Add(testGridPosition);
            }

            return snakeMoveList;
        }

        public List<GridPosition> NormalMoveRangeGrids()
        {
            List<GridPosition> normalMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;

                    normalMoveList.Add(testGridPosition);
                }
            }

            return normalMoveList;
        }

        public List<GridPosition> KingMoveRangeGrids()
        {
            List<GridPosition> kingMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            maxMoveDistance = 1;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                    {
                        kingMoveList.Add(testGridPosition);
                    }

                }
            }

            return kingMoveList;
        }

        public List<GridPosition> TigerMoveRangeGrids()
        {
            List<GridPosition> tigerMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int maxMoveDistance = 3;

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;
                    int testDistance = Mathf.Abs(x) + Mathf.Abs(z);

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                    if (unitGridPosition == testGridPosition) continue;
                    if (testDistance > 1 && testDistance <= maxMoveDistance)
                    {
                        if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                        {
                            tigerMoveList.Add(testGridPosition);
                        }
                    }
                }
            }

            return tigerMoveList;
        }

        public List<GridPosition> BullMoveRangeGrids()
        {
            List<GridPosition> bullMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            bool unitDetected = false;

            // Horizontal movement to the right
            for (int xOffset = 1; xOffset < LevelGrid.Instance.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) bullMoveList.Add(testGridPosition);
            }

            // Horizontal movement to the left
            unitDetected = false;
            for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) bullMoveList.Add(testGridPosition);
            }

            // Vertical movement upwards
            unitDetected = false;
            for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) bullMoveList.Add(testGridPosition);
            }

            // Vertical movement downwards
            unitDetected = false;
            for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) bullMoveList.Add(testGridPosition);
            }

            return bullMoveList;
        }

        public List<GridPosition> SnakeMoveRangeGrids()
        {
            List<GridPosition> snakeMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            int gridWidth = LevelGrid.Instance.GetGridWidth();
            int gridHeight = LevelGrid.Instance.GetGridHeight();
            bool unitDetected = false;

            // Diagonal movement to the upper right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) snakeMoveList.Add(testGridPosition);
            }

            unitDetected = false;
            // Diagonal movement to the upper left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) snakeMoveList.Add(testGridPosition);
            }

            unitDetected = false;
            // Diagonal movement to the lower right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) snakeMoveList.Add(testGridPosition);
            }

            unitDetected = false;
            // Diagonal movement to the lower left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break; // Stop if out of bounds
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) unitDetected = true;
                if (unitDetected) snakeMoveList.Add(testGridPosition);
            }

            return snakeMoveList;
        }
    }
}