namespace Nivandria.Battle.UnitSystem
{
    using System;
    using UnityEngine;
    using Nivandria.Battle.PathfindingSystem;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.Action;
    using System.Collections.Generic;
    using System.Linq;

    public class UnitTurnSystem : MonoBehaviour
    {
        public static UnitTurnSystem Instance { get; private set; }

        public event EventHandler OnSelectedUnitChanged;

        private List<Unit> waitingUnitList;
        private List<Unit> movedUnitList;
        private Unit selectedUnit;

        private string unitTag = "Units";
        private int turnRounds = 1;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one UnitTurnSystem! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            waitingUnitList = new List<Unit>();
            movedUnitList = new List<Unit>();

            waitingUnitList = SortFromFastestUnit();
        }

        /// <summary> Handles the selection of the fastest unit that hasn't moved yet.</summary>
		public void HandleUnitSelection()
        {
            if (UnitActionSystem.Instance.GetBusyStatus()) return;

            Unit nextUnit = GetAndRemoveFirstUnit();

            if (selectedUnit != null)
            {
                ResetSelectedUnit();
            }

            if (nextUnit != null)
            {
                CameraController.Instance.SetCameraFocusToPosition(nextUnit.transform.position);
                SelectUnit(nextUnit);
                return;
            }

            NextTurn();
        }

        private List<Unit> SortFromFastestUnit()
        {
            GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);
            List<Unit> unitList = new List<Unit>();

            foreach (GameObject unitObject in unitObjects)
            {
                Unit unitComponent = unitObject.GetComponent<Unit>();

                if (unitComponent == null) continue;

                unitList.Add(unitComponent);
            }

            unitList = unitList.OrderByDescending(unit => unit.GetCurrentAgility()).ToList();

            IEnumerable<string> unitNames = unitList.Select(unit => unit.GetCharacterName());
            Debug.Log(string.Join(" || ", unitNames));
            return unitList;
        }

        /// <summary>Resets the selected unit's status and shading after it's turn has ended.</summary>
        private void ResetSelectedUnit()
        {
            selectedUnit.SetSelectedStatus(false);
            selectedUnit.ChangeUnitShade();
            selectedUnit.SetTurnStatus(true);
        }

        /// <summary>Selects a unit and updates its status and shading.</summary>
        /// <param name="unit">The unit to be selected.</param>
        private void SelectUnit(Unit unit)
        {
            selectedUnit = unit;
            selectedUnit.SetTurnStatus(true);
            selectedUnit.SetSelectedStatus(true);
            selectedUnit.ChangeUnitShade();
            selectedUnit.ResetActionStatus();
            Pathfinding.Instance.SetupPath(selectedUnit.GetUnitType());
            UnitActionSystem.Instance.SetSelectedAction(unit.GetAction<MoveAction>());
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        private Unit GetAndRemoveFirstUnit()
        {
            if (waitingUnitList.Count == 0) return null;

            Unit firstUnit = waitingUnitList[0];
            movedUnitList.Add(firstUnit);
            waitingUnitList.RemoveAt(0);

            return firstUnit;
        }

        private void NextTurn()
        {
            waitingUnitList = SortFromFastestUnit();
            movedUnitList.Clear();
            selectedUnit = null;
            turnRounds++;
            UnitActionSystem.Instance.SetSelectedAction(null);
            GridSystemVisual.Instance.UpdateGridVisual();
            Debug.Log("All units have already moved! Next Round : " + turnRounds);
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }

        public Unit GetSelectedUnit() => selectedUnit;
        public int GetTurnNumber() => turnRounds;
    }

}