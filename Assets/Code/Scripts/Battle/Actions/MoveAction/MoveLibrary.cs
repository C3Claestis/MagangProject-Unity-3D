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
                    if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;

                    kingMoveList.Add(testGridPosition);
                }
            }

            return kingMoveList;
        }

        public List<GridPosition> TigerMoveValidGrids()
        {
            List<GridPosition> tigerMoveList = new List<GridPosition>();
            List<GridPosition> knightMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            // Define the possible knight move offsets
            GridPosition[] knightMoveOffsets =
            {
                new GridPosition(1, 2),
                new GridPosition(2, 1),
                new GridPosition(2, -1),
                new GridPosition(1, -2),
                new GridPosition(-1, -2),
                new GridPosition(-2, -1),
                new GridPosition(-2, 1),
                new GridPosition(-1, 2)
            };

            foreach (GridPosition offsetGridPosition in knightMoveOffsets)
            {
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (LevelGrid.Instance.IsValidGridPosition(testGridPosition) &&
                    !LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition) &&
                    Pathfinding.Instance.IsWalkableGridPosition(testGridPosition))
                {
                    knightMoveList.Add(testGridPosition);
                }
            }

            return knightMoveList;
        }

        public List<GridPosition> BullMoveValidGrids()
        {
            List<GridPosition> bullMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            // Horizontal movement to the right
            for (int xOffset = 1; xOffset < LevelGrid.Instance.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;


                bullMoveList.Add(testGridPosition);
            }

            // Horizontal movement to the left
            for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement upwards
            for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement downwards
            for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

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

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the upper left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) break;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            return snakeMoveList;
        }

    }
}