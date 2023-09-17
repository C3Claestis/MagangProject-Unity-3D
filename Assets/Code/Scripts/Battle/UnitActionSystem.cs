namespace Nivandria.Battle.Action
{
	using UnityEngine;
	using UnityEngine.UI;
	using UnityEngine.EventSystems;
	using Nivandria.Battle.Grid;
	using Nivandria.Battle;
	using System;

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

		private void Update()
		{
			if (isBusy) return;

			HandleUnitSelection();
			HandleSelectedAction();
		}

		/// <summary> Handles the selection of the fastest unit that hasn't moved yet.</summary>
		private void HandleUnitSelection()
		{
			if (!Input.GetKeyUp(KeyCode.Space)) return;

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

				int unitSpeed = unitComponent.GetAgility();
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
			SetSelectedAction(unit.GetAction<MoveAction>());
			OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>Handles the selected action when the left mouse button is clicked on the valid grid.</summary>
		private void HandleSelectedAction()
		{
			if (Input.GetMouseButtonDown(0))
			{
				if (selectedUnit == null) return;
				if (EventSystem.current.IsPointerOverGameObject()) return;
				if (selectedUnit.GetActionStatus(selectedAction.GetActionType())) return;
				GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

				if (selectedAction.IsValidActionGridPosition(mouseGridPosition))
				{
					SetBusy();
					selectedAction.TakeAction(mouseGridPosition, OnActionComplete);

					if (selectedAction == selectedUnit.GetAction<MoveAction>())
					{
						OnMoveActionPerformed?.Invoke(this, EventArgs.Empty);
					}
				}

			}
		}

		/// <summary>Function that will be called after Action Completed.</summary>
		private void OnActionComplete()
		{
			selectedUnit.UpdateUnitGridPosition();
			selectedUnit.SetActionStatus(selectedAction.GetActionType(), true);
			selectedUnit.CalculateUnitDirection();
			selectedAction.SetActive(false);
			GridSystemVisual.Instance.UpdateGridVisual();
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