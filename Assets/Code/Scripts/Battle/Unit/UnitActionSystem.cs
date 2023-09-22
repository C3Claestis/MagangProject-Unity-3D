namespace Nivandria.Battle.UnitSystem
{
	using Nivandria.Battle.Grid;
	using Nivandria.Battle.Action;
	using Nivandria.Battle;
	using UnityEngine.UI;
	using UnityEngine;
	using System;

	public class UnitActionSystem : MonoBehaviour
	{
		public static UnitActionSystem Instance { get; private set; }

		[SerializeField] private Image busyUI;

		public event EventHandler OnSelectedActionChanged;
		public event EventHandler OnMoveActionPerformed;

		private BaseAction selectedAction;

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


		/// <summary>Handles the selected action when the left mouse button is clicked on the valid grid.</summary>
		public void HandleSelectedAction(string controller)
		{
			if (isBusy) return;
			Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();

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
			Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();
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

		public void ClearBusy()
		{
			busyUI.gameObject.SetActive(false);
			isBusy = false;
		}

		private void SetBusy()
		{
			busyUI.gameObject.SetActive(true);
			isBusy = true;
		}

		public bool GetBusyStatus() => isBusy;

		public BaseAction GetSelectedAction() => selectedAction;
		#endregion
	}
}