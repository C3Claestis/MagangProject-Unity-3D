namespace Nivandria.Battle.Action
{
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using Nivandria.Battle.Grid;
	using Nivandria.Battle;
	using System;
	using Nivandria.Battle.PathfindingSystem;

	public class UnitActionSystem : MonoBehaviour
	{
		public static UnitActionSystem Instance { get; private set; }

		[SerializeField] private Unit selectedUnit;
		[SerializeField] private Image busyUI;

		public event EventHandler OnSelectedUnitChanged;
		public event EventHandler OnSelectedActionChanged;
		public event EventHandler OnMoveActionPerformed;

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

		/// <summary> Handles the selection of the fastest unit that hasn't moved yet.</summary>
		public void HandleUnitSelection()
		{
			if (isBusy) return;

			Unit fastestUnit = SelectFastestUnit();

			if (selectedUnit != null)
			{
				ResetSelectedUnit();
			}

			if (fastestUnit != null)
			{
				CameraController.Instance.SetCameraFocusToPosition(fastestUnit.transform.position);
				SelectUnit(fastestUnit);
				return;
			}

			selectedUnit = null;
			selectedAction = null;
			Debug.Log("All units have already moved");
			GridSystemVisual.Instance.UpdateGridVisual();
			OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
		}

		private Unit SelectFastestUnit()
		{
			GameObject[] unitObjects = GameObject.FindGameObjectsWithTag(unitTag);
			Unit fastestUnit = null;
			int fastestSpeed = int.MinValue;

			foreach (GameObject unitObject in unitObjects)
			{
				Unit unitComponent = unitObject.GetComponent<Unit>();

				if (unitComponent == null) continue;

				int unitSpeed = unitComponent.GetCurrentAgility();
				bool hasMoved = unitComponent.GetTurnStatus();

				if (!hasMoved && unitSpeed > fastestSpeed)
				{
					fastestUnit = unitComponent;
					fastestSpeed = unitSpeed;
				}
			}

			return fastestUnit;
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
			SetSelectedAction(unit.GetAction<MoveAction>());
			OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>Handles the selected action when the left mouse button is clicked on the valid grid.</summary>
		public void HandleSelectedAction(string controller)
		{
			if (isBusy) return;
			if (controller == "leftButton")
			{
				if (MouseWorld.IsPointerOnUI()) return;
			}
			if (selectedUnit == null) return;
			if (selectedUnit.GetActionStatus(selectedAction.GetActionType())) return;
			GridPosition pointerGridPosition = Pointer.Instance.GetCurrentGrid();

			if (selectedAction.IsValidActionGridPosition(pointerGridPosition))
			{
				SetBusy();
				selectedAction.TakeAction(pointerGridPosition, OnActionComplete);

				if (selectedAction == selectedUnit.GetAction<MoveAction>())
				{
					OnMoveActionPerformed?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Function that will be called after Action Completed.</summary>
		private void OnActionComplete()
		{
			selectedUnit.SetActionStatus(selectedAction.GetActionType(), true);
			PlayerInputController.Instance.SetActionMap("Gridmap");
			selectedUnit.UpdateUnitGridPosition();
			selectedUnit.UpdateUnitDirection();
			selectedAction.SetActive(false);

			CameraController.Instance.SetActive(true);
			GridSystemVisual.Instance.UpdateGridVisual();
			Pointer.Instance.SetActive(true);
			ClearBusy();
		}

		#region Getter Setter
		public void SetSelectedAction(BaseAction baseAction)
		{
			selectedAction = baseAction;
			GridSystemVisual.Instance.UpdateGridVisual();
			OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ClearBusy()
		{
			busyUI.gameObject.SetActive(false);
			isBusy = false;
		}
		private void SetBusy()
		{
			busyUI.gameObject.SetActive(true);
			isBusy = true;
		}

		public Unit GetSelectedUnit() => selectedUnit;
		public BaseAction GetSelectedAction() => selectedAction;
		#endregion
	}
}