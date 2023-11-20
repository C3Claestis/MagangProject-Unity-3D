namespace Nivandria.Battle.UnitSystem
{
	using Nivandria.Battle.Grid;
	using Nivandria.Battle.Action;
	using Nivandria.Battle;
	using UnityEngine;
	using System;
	using Nivandria.Battle.UI;

	public class UnitActionSystem : MonoBehaviour
	{
		public static UnitActionSystem Instance { get; private set; }

		[SerializeField] private CanvasGroup actionSystemUIGroup;

		public event EventHandler OnMoveActionPerformed;
		public event EventHandler OnActionCompleted;

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
			if (selectedUnit.GetActionStatus(selectedAction.GetActionCategory())) return;
			GridPosition pointerGridPosition = Pointer.Instance.GetCurrentGrid();

			if (selectedAction.IsValidActionGridPosition(pointerGridPosition))
			{
				SetBusy();
				HideActionUI();
				selectedAction.TakeAction(pointerGridPosition, OnActionComplete);

				if (selectedAction == selectedUnit.GetAction<MoveAction>())
				{
					OnMoveActionPerformed?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Function that will be called after Action Completed.</summary>
		public void OnActionComplete()
		{
			GridSystemVisual.Instance.HideAllGridPosition();

			if (UnitTurnSystem.Instance.CheckGameOverCondition())
			{
				HideActionUI();
				return;
			}

			PlayerInputController.Instance.SetActionMap("BattleUI");
			Unit selectedUnit = UnitTurnSystem.Instance.GetSelectedUnit();
			Vector3 unitPosition = LevelGrid.Instance.GetWorldPosition(selectedUnit.GetGridPosition());
			selectedUnit.SetActionStatus(selectedAction.GetActionCategory(), true);

			selectedUnit.UpdateUnitGridPosition();
			selectedUnit.UpdateUnitDirection();

			selectedAction.SetActive(false);

			CameraController.Instance.SetActive(true);

			CameraController.Instance.SetCameraFocusToPosition(unitPosition);
			Pointer.Instance.SetPointerOnGrid(unitPosition);

			ClearBusy();
			ShowActionUI();

			OnActionCompleted?.Invoke(this, EventArgs.Empty);
			UnitActionSystemUI.Instance.SelectUIBaseOnSelectedAction();
		}

		#region Getter Setter
		public void SetSelectedAction(BaseAction baseAction)
		{
			selectedAction = baseAction;
		}

		public void HideActionUI()
		{
			actionSystemUIGroup.alpha = 0;
			actionSystemUIGroup.interactable = false;
			actionSystemUIGroup.blocksRaycasts = false;
		}

		public void ShowActionUI()
		{
			actionSystemUIGroup.alpha = 1;
			actionSystemUIGroup.interactable = true;
			actionSystemUIGroup.blocksRaycasts = true;
		}

		public void ClearBusy() => isBusy = false;
		public void SetBusy() => isBusy = true;

		public bool GetBusyStatus() => isBusy;

		public BaseAction GetSelectedAction() => selectedAction;
		#endregion
	}
}