namespace Nivandria.Battle.UnitSystem
{
	using Nivandria.Battle.Grid;
	using Nivandria.Battle.Action;
	using Nivandria.Battle;
	using UnityEngine.UI;
	using UnityEngine;
	using System;
	using Nivandria.Battle.UI;

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
				SetBusyUI();
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
			PlayerInputController.Instance.SetActionMap("BattleUI");
			Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();
			selectedUnit.SetActionStatus(selectedAction.GetActionType(), true);

			selectedUnit.UpdateUnitGridPosition();
			selectedUnit.UpdateUnitDirection();

			selectedAction.SetActive(false);

			CameraController.Instance.SetActive(true);
			GridSystemVisual.Instance.HideAllGridPosition();

			ClearBusy();
			ClearBusyUI();

			UnitActionSystemUI.Instance.SelectUIBaseOnSelectedAction();
		}

		#region Getter Setter
		public void SetSelectedAction(BaseAction baseAction)
		{
			selectedAction = baseAction;
			OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
		}

		public void ClearBusy() => isBusy = false;
		public void SetBusy() => isBusy = true;
		public void SetBusyUI() => busyUI.gameObject.SetActive(true);
		public void ClearBusyUI() => busyUI.gameObject.SetActive(false);
		public bool GetBusyStatus() => isBusy;

		public BaseAction GetSelectedAction() => selectedAction;
		#endregion
	}
}