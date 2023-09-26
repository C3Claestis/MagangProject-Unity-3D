namespace Nivandria.Battle.Grid
{
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;
    using UnityEditor.PackageManager;

    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }

        [SerializeField] private Transform gridObjectDebugPrefab;
        [SerializeField] private int gridWidth = 8;
        [SerializeField] private int gridHeight = 4;
        [SerializeField] private float cellSize = 2;
        [SerializeField] bool showWalkableGrid = false;
        public Unit unitBase;
        public Unit uniTarget;


        private GridSystem<GridObject> gridSystem;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one LevelGrid! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;

            gridSystem = new GridSystem<GridObject>(gridWidth, gridHeight, cellSize,
                        (GridSystem<GridObject> g, GridPosition gridPosition) => new GridObject(g, gridPosition));
            // gridSystem.CreateDebugObjects(gridObjectDebugPrefab);
        }

        private void Start()
        {
            Pathfinding.Instance.SetupGrid(gridWidth, gridHeight, cellSize);
        }

        /// <summary>Adds a unit at the specified grid position.</summary>
        /// <param name="gridPosition">The grid position to add the unit to.</param>
        /// <param name="unit">The unit to be added.</param>
        public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.AddUnit(unit);
        }

        /// <summary>Retrieves the list of units at the specified grid position.</summary>
        /// <param name="gridPosition">The grid position to query.</param>
        /// <returns>A list of units at the specified grid position.</returns>
        public List<Unit> GetUnitListAtGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.GetUnitList();
        }

        /// <summary>Removes a unit from the specified grid position.</summary>
        /// <param name="gridPosition">The grid position to remove the unit from.</param>
        /// <param name="unit">The unit to be removed.</param>
        public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            gridObject.RemoveUnit(unit);
        }

        /// <summary>Moves a unit from one grid position to another.</summary>
        /// <param name="unit">The unit to move.</param>
        /// <param name="fromGridPosition">The source grid position.</param>
        /// <param name="toGridPosition">The target grid position.</param>
        public void UnitMoveGridPosition(Unit unit, GridPosition fromGridPosition, GridPosition toGridPosition)
        {
            RemoveUnitAtGridPosition(fromGridPosition, unit);
            AddUnitAtGridPosition(toGridPosition, unit);
        }

        /// <summary>Checks if a given grid position is within the valid boundaries of the grid.</summary>
        /// <param name="gridPosition">The grid position to check.</param>
        /// <returns>True if the grid position is valid; otherwise, false.</returns>
        public bool IsValidGridPosition(GridPosition gridPosition) => gridSystem.IsValidGridPosition(gridPosition);

        /// <summary>Checks if there is any unit present on a given grid position.</summary>
        /// <param name="gridPosition">The grid position to check for unit presence.</param>
        /// <returns>True if there is any unit on the grid position; otherwise, false.</returns>
        public bool HasAnyUnitOnGridPosition(GridPosition gridPosition)
        {
            GridObject gridObject = gridSystem.GetGridObject(gridPosition);
            return gridObject.HasAnyUnit();
        }

        #region Unit Relative Facing Detector

        // ! Temporary numbering
        // 100 is Front
        // 150 is Side
        // 200 is Back

        public int RelativeFacingChecker(Unit targetUnit, Unit relativeTo)
        {
            GridPosition unitGridPosition = targetUnit.GetGridPosition();
            FacingDirection facingDirection = targetUnit.GetFacingDirection();
            int normalPercentage = 100;
            int oneHalfPercentage = 150;
            int doublePercentage = 200;

            if (targetUnit == null)
            {
                Debug.Log("baseUnit is null!");
                return 0;
            }

            if (relativeTo == null)
            {
                Debug.Log("targetUnit is null!");
                return 0;
            }

            // Check Horizontal to the EAST
            for (int xOffset = 1; xOffset < GetGridWidth(); xOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!IsValidGridPosition(testGridPosition)) break;
                if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                if (testUnit == relativeTo && facingDirection == FacingDirection.WEST) return doublePercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.EAST) return normalPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.NORTH) return oneHalfPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.SOUTH) return oneHalfPercentage;
            }

            // Check Horizontal to the WEST
            for (int xOffset = -1; xOffset >= -GetGridWidth(); xOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                if (!IsValidGridPosition(testGridPosition)) break;
                if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                if (testUnit == relativeTo && facingDirection == FacingDirection.WEST) return normalPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.EAST) return doublePercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.NORTH) return oneHalfPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.SOUTH) return oneHalfPercentage;
            }

            // Check Vertical to the NORTH
            for (int zOffset = 1; zOffset < GetGridHeight(); zOffset++)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!IsValidGridPosition(testGridPosition)) break;
                if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                if (testUnit == relativeTo && facingDirection == FacingDirection.NORTH) return normalPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.SOUTH) return doublePercentage;

                if (testUnit == relativeTo && facingDirection == FacingDirection.WEST) return oneHalfPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.EAST) return oneHalfPercentage;
            }

            // Check Vertical to the SOUTH
            for (int zOffset = -1; zOffset >= -GetGridHeight(); zOffset--)
            {
                GridPosition testGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                if (!IsValidGridPosition(testGridPosition)) break;
                if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                if (testUnit == relativeTo && facingDirection == FacingDirection.NORTH) return doublePercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.SOUTH) return normalPercentage;

                if (testUnit == relativeTo && facingDirection == FacingDirection.WEST) return oneHalfPercentage;
                if (testUnit == relativeTo && facingDirection == FacingDirection.EAST) return oneHalfPercentage;
            }

            if (facingDirection == FacingDirection.EAST)
            {
                for (int xOffset = -1; xOffset >= -GetGridWidth(); xOffset--)
                {
                    GridPosition testXGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                    if (!IsValidGridPosition(testXGridPosition)) break;

                    // Check Vertical to the NORTH
                    for (int zOffset = 1; zOffset < GetGridHeight(); zOffset++)
                    {
                        GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }

                    // Check Vertical to the SOUTH
                    for (int zOffset = -1; zOffset >= -GetGridHeight(); zOffset--)
                    {
                        GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }
                }

                return normalPercentage;
            }

            if (facingDirection == FacingDirection.WEST)
            {
                for (int xOffset = 1; xOffset < GetGridWidth(); xOffset++)
                {
                    GridPosition testXGridPosition = new GridPosition(unitGridPosition.x + xOffset, unitGridPosition.z);

                    if (!IsValidGridPosition(testXGridPosition)) break;

                    // Check Vertical to the NORTH
                    for (int zOffset = 1; zOffset < GetGridHeight(); zOffset++)
                    {
                        GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }

                    // Check Vertical to the SOUTH
                    for (int zOffset = -1; zOffset >= -GetGridHeight(); zOffset--)
                    {
                        GridPosition testGridPosition = new GridPosition(testXGridPosition.x, testXGridPosition.z + zOffset);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }
                }

                return normalPercentage;
            }

            if (facingDirection == FacingDirection.NORTH)
            {
                for (int zOffset = -1; zOffset >= -GetGridHeight(); zOffset--)
                {
                    GridPosition testYGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                    if (!IsValidGridPosition(testYGridPosition)) break;

                    // Check Vertical to the NORTH
                    for (int xOffset = 1; xOffset < GetGridWidth(); xOffset++)
                    {
                        GridPosition testGridPosition = new GridPosition(testYGridPosition.x + xOffset, testYGridPosition.z);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }

                    // Check Vertical to the SOUTH
                    for (int xOffset = -1; xOffset >= -GetGridWidth(); xOffset--)
                    {
                        GridPosition testGridPosition = new GridPosition(testYGridPosition.x + xOffset, testYGridPosition.z);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }
                }

                return normalPercentage;
            }

            if (facingDirection == FacingDirection.SOUTH)
            {
                for (int zOffset = 1; zOffset < GetGridHeight(); zOffset++)
                {
                    GridPosition testYGridPosition = new GridPosition(unitGridPosition.x, unitGridPosition.z + zOffset);

                    if (!IsValidGridPosition(testYGridPosition)) break;

                    // Check Vertical to the NORTH
                    for (int xOffset = 1; xOffset < GetGridWidth(); xOffset++)
                    {
                        GridPosition testGridPosition = new GridPosition(testYGridPosition.x + xOffset, testYGridPosition.z);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }

                    // Check Vertical to the SOUTH
                    for (int xOffset = -1; xOffset >= -GetGridWidth(); xOffset--)
                    {
                        GridPosition testGridPosition = new GridPosition(testYGridPosition.x + xOffset, testYGridPosition.z);

                        if (!IsValidGridPosition(testGridPosition)) break;
                        if (!HasAnyUnitOnGridPosition(testGridPosition)) continue;

                        Unit testUnit = GetUnitListAtGridPosition(testGridPosition)[0];
                        if (testUnit == relativeTo) return oneHalfPercentage;
                    }
                }

                return normalPercentage;
            }

            string targetUnitName = targetUnit.GetCharacterName();
            string relativeToName = relativeTo.GetCharacterName();
            Debug.LogError("Can't find the Relative Facing for " + targetUnitName + " and " + relativeToName + ".");
            return 0;
        }

        #endregion


        #region Getter Setter
        public GridPosition GetGridPosition(Vector3 worldPosition) => gridSystem.GetGridPosition(worldPosition);

        public Vector3 GetWorldPosition(GridPosition gridPosition) => gridSystem.GetWorldPosition(gridPosition);

        public int GetGridWidth() => gridWidth;
        public int GetGridHeight() => gridHeight;
        public float GetCellSize() => cellSize;
        public bool WalkableGridDebugStatus() => showWalkableGrid;
        #endregion
    }
}