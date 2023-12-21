namespace Nivandria.Battle.Action
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;

    public static class MovePatern
    {
        /// <summary>Generates a list of valid grid positions for a normal move. </summary>
        /// <returns>A list of GridPosition objects representing valid move positions.</returns>
        public static List<GridPosition> NormalMoveValidGrids(Unit unit, int maxMoveDistance)
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

        /// <summary>Generates a list of valid grid positions for a king move. </summary>
        /// <returns>A list of GridPosition objects representing valid move positions.</returns>
        public static List<GridPosition> KingMoveValidGrids(Unit unit)
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

        /// <summary>Generates a list of valid grid positions for a tiger move. </summary>
        /// <returns>A list of GridPosition objects representing valid move positions.</returns>
        public static List<GridPosition> TigerMoveValidGrids(Unit unit, LayerMask layerMask)
        {
            List<GridPosition> tigerMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();
            Pathfinding.Instance.SetupPath(UnitType.Aerial);

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

            GridPosition[] specificOffsets =
            {
                new GridPosition(0, 1), new GridPosition(1, 1),
                new GridPosition(1, 1), new GridPosition(1, 0),
                new GridPosition(1, 0), new GridPosition(1, -1),
                new GridPosition(1, -1), new GridPosition(0, -1),
                new GridPosition(0, -1), new GridPosition(-1, -1),
                new GridPosition(-1, -1), new GridPosition(-1, 0),
                new GridPosition(-1, 0), new GridPosition(-1, 1),
                new GridPosition(-1, 1), new GridPosition(0, 1)
            };



            foreach (GridPosition offsetGridPosition in knightMoveOffsets)
            {
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) continue;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) continue;
                if (Pathfinding.Instance.IsObstacleOnGrid(LevelGrid.Instance.GetWorldPosition(testGridPosition), out string objectName)) continue;
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) continue;
                if (HasAnyObstacleOnPath(unitGridPosition, offsetGridPosition, specificOffsets)) continue;

                tigerMoveList.Add(testGridPosition);
            }

            return tigerMoveList;
        }

        /// <summary>Generates a list of valid grid positions for a bull move. </summary>
        /// <returns>A list of GridPosition objects representing valid move positions.</returns>
        public static List<GridPosition> BullMoveValidGrids(Unit unit)
        {
            List<GridPosition> bullMoveList = new List<GridPosition>();
            GridPosition unitGridPosition = unit.GetGridPosition();

            // Horizontal movement to the right
            for (int xOffset = 1; xOffset < LevelGrid.Instance.GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            // Horizontal movement to the left
            for (int xOffset = -1; xOffset >= -LevelGrid.Instance.GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement upwards
            for (int zOffset = 1; zOffset < LevelGrid.Instance.GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            // Vertical movement downwards
            for (int zOffset = -1; zOffset >= -LevelGrid.Instance.GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                bullMoveList.Add(testGridPosition);
            }

            return bullMoveList;
        }

        /// <summary>Generates a list of valid grid positions for a snake move. </summary>
        /// <returns>A list of GridPosition objects representing valid move positions.</returns>
        public static List<GridPosition> SnakeMoveValidGrids(Unit unit)
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
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the upper left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z + offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower right
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            // Diagonal movement to the lower left
            for (int offset = 1; offset < Mathf.Min(gridWidth, gridHeight); offset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x - offset, unitGridPosition.z - offset);

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) break;
                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))
                {
                    Unit testUnit = LevelGrid.Instance.GetUnitListAtGridPosition(testGridPosition)[0];
                    if (!testUnit.IsAlive()) continue;
                    if (testUnit.IsEnemy() != unit.IsEnemy()) break;
                }
                if (!Pathfinding.Instance.IsWalkableGridPosition(testGridPosition)) break;

                snakeMoveList.Add(testGridPosition);
            }

            return snakeMoveList;
        }

        private static bool HasAnyObstacleOnPath(GridPosition unitGridPosition, GridPosition offsetGridPosition, GridPosition[] specificOffsets)
        {

            if (offsetGridPosition == new GridPosition(1, 2))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[0])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[1])) return true;
            }

            if (offsetGridPosition == new GridPosition(2, 1))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[2])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[3])) return true;
            }

            if (offsetGridPosition == new GridPosition(2, -1))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[4])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[5])) return true;
            }

            if (offsetGridPosition == new GridPosition(1, -2))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[6])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[7])) return true;
            }


            if (offsetGridPosition == new GridPosition(-1, -2))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[8])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[9])) return true;
            }


            if (offsetGridPosition == new GridPosition(-2, -1))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[10])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[11])) return true;
            }


            if (offsetGridPosition == new GridPosition(-2, 1))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[12])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[13])) return true;
            }


            if (offsetGridPosition == new GridPosition(-1, 2))
            {
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[14])) return true;
                if (!Pathfinding.Instance.IsWalkableGridPosition(unitGridPosition + specificOffsets[15])) return true;
            }

            return false;
        }
    }
}