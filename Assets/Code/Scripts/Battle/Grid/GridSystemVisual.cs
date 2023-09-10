namespace Nivandria.Battle.Grid
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.Action;
    using UnityEngine;

    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

        public event EventHandler OnGridVisualUpdated;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one GridSystemVisual! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            InstantiateGrid();
            HideAllGridPosition();
        }

        /// <summary>Instantiates a grid system's visual representation based on the grid's dimensions. </summary>
        private void InstantiateGrid()
        {
            int gridWidth = LevelGrid.Instance.GetGridWidth();
            int gridHeight = LevelGrid.Instance.GetGridHeight();
            gridSystemVisualSingleArray = new GridSystemVisualSingle[gridWidth, gridHeight];


            for (int x = 0; x < gridWidth; x++)
            {
                for (int z = 0; z < gridHeight; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Transform gridSystemVisualSinglTransform =
                        Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                    gridSystemVisualSingleArray[x, z] = gridSystemVisualSinglTransform.GetComponent<GridSystemVisualSingle>();
                }
            }
        }

        /// <summary>Hides the visual representation of all grid positions.</summary>
        public void HideAllGridPosition()
        {
            foreach (GridSystemVisualSingle grid in gridSystemVisualSingleArray)
            {
                grid.HideGrid();
            }
        }

        /// <summary>Shows the visual representation of specific grid positions.</summary>
        /// <param name="gridPositionList">The list of grid positions to show.</param>
        public void ShowGridPositionList(List<GridPosition> gridPositinList)
        {
            foreach (GridPosition gridPosition in gridPositinList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].ShowGrid();
            }
        }

        /// <summary>Updates the visual representation of the grid based on the selected action's valid grid positions.</summary>
        public void UpdateGridVisual()
        {
            HideAllGridPosition();

            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();

            if (selectedAction == null) return;

            ShowGridPositionList(
                selectedAction.GetValidActionGridPosition()
            );

            OnGridVisualUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}