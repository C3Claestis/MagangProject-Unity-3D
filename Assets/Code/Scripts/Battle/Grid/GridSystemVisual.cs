namespace Nivandria.Battle.Grid
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Action;
    using UnityEngine;

    public class GridSystemVisual : MonoBehaviour
    {
        public static GridSystemVisual Instance { get; private set; }

        [SerializeField] private Transform gridSystemVisualSinglePrefab;
        [SerializeField] private Transform gridVisualParent;
        [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterialList;
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
                    gridSystemVisualSinglTransform.SetParent(gridVisualParent);
                }
            }
        }

        /// <summary>Hides the visual representation of all grid positions.</summary>
        public void HideAllGridPosition()
        {
            foreach (GridSystemVisualSingle grid in gridSystemVisualSingleArray)
            {
                grid.Hide();
            }
        }

        /// <summary>Shows the visual representation of specific grid positions.</summary>
        /// <param name="gridPositionList">The list of grid positions to show.</param>
        public void ShowGridPositionList(List<GridPosition> gridPositionList, GridVisualType gridVisualType)
        {
            if (gridPositionList == null) return;

            foreach (GridPosition gridPosition in gridPositionList)
            {
                gridSystemVisualSingleArray[gridPosition.x, gridPosition.z].Show(GetGridVisualTypeMaterial(gridVisualType));
            }
        }

        /// <summary>Updates the visual representation of the grid based on the selected action's valid grid positions.</summary>
        public void UpdateGridVisual()
        {
            BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
            GridVisualType invalidVisualType = GridVisualType.Grey;
            GridVisualType visualType;

            HideAllGridPosition();

            if (selectedAction == null) return;

            switch (selectedAction)
            {
                default:
                case MoveAction:
                    visualType = GridVisualType.White;
                    break;
                case SpinAction:
                    visualType = GridVisualType.Blue;
                    break;
                case BaseSkillAction:
                    visualType = GridVisualType.Red;
                    invalidVisualType = GridVisualType.RedSoft;
                    break;
            }

            if (selectedAction.GetActionStatus())
            {
                visualType = invalidVisualType;
            }

            ShowGridPositionList(
                selectedAction.GetValidActionGridPosition(),
                visualType
            );

            if (selectedAction is MoveAction) return;

            BaseSkillAction skillAction = (BaseSkillAction)selectedAction;
            
            ShowGridPositionList(
                skillAction.GetRangeActionGridPosition(),
                invalidVisualType
            );


            OnGridVisualUpdated?.Invoke(this, EventArgs.Empty);
        }

        private Material GetGridVisualTypeMaterial(GridVisualType gridVisualType)
        {
            foreach (GridVisualTypeMaterial gridVisualTypeMaterial in gridVisualTypeMaterialList)
            {
                if (gridVisualTypeMaterial.gridVisualType != gridVisualType) continue;
                return gridVisualTypeMaterial.material;
            }

            Debug.LogError("Couldn't find GridVisualTypeMaterial for GridVisualType : " + gridVisualType);
            return null;
        }
    }
}