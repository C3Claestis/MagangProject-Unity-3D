namespace Nivandria.Battle.Action
{
    using UnityEngine;
    using UnityEngine.EventSystems;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle;
    using System;

    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }

        [SerializeField] private Unit selectedUnit;

        public event EventHandler OnSelectedUnitChanged;
        private BaseAction selectedAction;

        private string unitTag = "Units";
        private bool isBusy = false;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitActionSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Update()
        {
            if (isBusy) return;

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetKeyUp(KeyCode.Space))
            {
                SelectFastestUnit();
            }

            HandleSelectedAction();
        }

        /// <summary> Handles the selection of the fastest unit that hasn't moved yet.
        /// </summary>
        private void SelectFastestUnit()
        {
            GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);

            Unit fastestUnit = null;
            int fastestSpeed = int.MinValue;

            foreach (GameObject unitObject in unitObjects)
            {
                Unit unitComponent = unitObject.GetComponent<Unit>();

                if (unitComponent == null) continue;

                int unitSpeed = unitComponent.GetAgility();
                bool hasMoved = unitComponent.GetMoveStatus();

                if (!hasMoved && unitSpeed > fastestSpeed)
                {
                    fastestUnit = unitComponent;
                    fastestSpeed = unitSpeed;
                }
            }

            if (selectedUnit != null)
            {
                ResetSelectedUnit();
            }

            if (fastestUnit != null)
            {
                CameraController.Instance.SetCameraFocusToPosition(fastestUnit.transform.position);
                SelectUnit(fastestUnit);
            }
            else
            {
                selectedUnit = null;
                Debug.Log("All units have already moved");
            }

            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>Resets the selected unit's status and shading after it has been moved.
        /// </summary>
        private void ResetSelectedUnit()
        {
            selectedUnit.SetSelectedStatus(false);
            selectedUnit.ChangeUnitShade();
            selectedUnit.SetMoveStatus(true);
        }

        /// <summary>Selects a unit and updates its status and shading.
        /// </summary>
        /// <param name="unit">The unit to be selected.</param>
        private void SelectUnit(Unit unit)
        {
            selectedUnit = unit;
            selectedUnit.SetMoveStatus(true);
            selectedUnit.SetSelectedStatus(true);
            selectedUnit.ChangeUnitShade();
            SetSelectedAction(unit.GetMoveAction());
            GridSystemVisual.Instance.UpdateGridVisual();

        }

        private void HandleSelectedAction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (selectedUnit == null) return;

                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (selectedAction.IsValidActionGridPosition(mouseGridPosition)){
                    SetBusy();
                    selectedAction.TakeAction(mouseGridPosition, OnActionComplete);
                }
                
            }
        }

        
        private void OnActionComplete(){
            GridSystemVisual.Instance.UpdateGridVisual();
            ClearBusy();
        }

        private void ClearBusy() => isBusy = false;
        private void SetBusy() => isBusy = true;

        public Unit GetSelectedUnit() => selectedUnit;
        public BaseAction GetSelectedAction() =>  selectedAction;
        
        public void SetSelectedAction(BaseAction baseAction) => selectedAction = baseAction;
    }

}