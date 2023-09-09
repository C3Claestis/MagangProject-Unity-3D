namespace Nivandria.Battle.Grid
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Nivandria.Battle.Action;

    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        public event EventHandler OnGridVisualUpdated;


        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        private GridSystemVisualSingle[,] gridSystemVisualSingleArray;

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

        public void HideAllGridPosition()
        {
            foreach (GridSystemVisualSingle grid in gridSystemVisualSingleArray)
            {
                grid.HideGrid();
            }
        }

        public void ShowGridPositionList(List<GridPosition> gridPositinList)
        {
            foreach (GridPosition gridPosition in gridPositinList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].ShowGrid();
            }
        }

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