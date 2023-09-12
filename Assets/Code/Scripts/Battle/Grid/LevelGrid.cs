namespace Nivandria.Battle.Grid
{
    using System.Collections.Generic;
    using Nivandria.Battle.PathfindingSystem;
    using UnityEngine;

    public class LevelGrid : MonoBehaviour
    {
        public static LevelGrid Instance { get; private set; }

        [SerializeField] private Transform gridObjectDebugPrefab;
        [SerializeField] private int gridWidth = 8;
        [SerializeField] private int gridHeight = 4;
        [SerializeField] private float cellSize = 2;
        [SerializeField] bool showWalkableGrid = false;

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

        private void Start() {
            Pathfinding.Instance.Setup(gridWidth, gridHeight, cellSize);
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