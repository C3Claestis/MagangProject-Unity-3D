namespace Nivandria.Battle.Action
{
    using System;
    using System.Collections.Generic;
    using Nivandria.Battle.UnitSystem;
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UI;
    using UnityEngine;

    public abstract class BaseAction : MonoBehaviour
    {
        protected Unit unit;
        protected Action onActionComplete;
        protected bool isActive;
        protected abstract string actionName { get; }
        protected abstract ActionType actionType { get; }
        [SerializeField] protected Transform dialogueConfirmationPrefab;

        protected virtual void Awake()
        {
            unit = GetComponent<Unit>();
        }

        /// <summary>Executes an action at a specified grid position and calls a callback function upon completion.</summary>
        /// <param name="gridPosition">The target grid position for the action.</param>
        /// <param name="onActionComplete">Callback function to invoke when the action is complete.</param>
        public virtual void TakeAction(GridPosition gridPosition, Action onActionComplete)
        {
            CameraController.Instance.SetCameraFocusToPosition(LevelGrid.Instance.GetWorldPosition(gridPosition));
            CameraController.Instance.SetActive(false);
            Pointer.Instance.SetActive(false);
            this.onActionComplete = onActionComplete;
            SetActive(true);
        }

        /// <summary>Checks whether the given grid position is a valid action grid position for the unit.</summary>
        /// <param name="gridPosition">The grid position to be checked.</param>
        /// <returns>True if the grid position is a valid action grid position, otherwise false.</returns>
        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPosition();
            return validGridPositionList.Contains(gridPosition);
        }

        protected virtual void InitializeConfirmationButton(Action onYesButtonSelected, Action onNoButtonSelected)
        {
            Transform uiTransform = GameObject.FindGameObjectWithTag("UI").transform;
            Transform confirmationTranform = Instantiate(dialogueConfirmationPrefab, uiTransform);
            PlayerInputController.Instance.SetActionMap("ConfirmationUI");
            confirmationTranform.GetComponent<ConfirmationDialogUI>().InitializeConfirmationButton(onYesButtonSelected, onNoButtonSelected);
        }

        protected virtual void YesButtonAction()
        {
            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        protected virtual void NoButtonAction()
        {
            UnitActionSystem.Instance.ClearBusy();
            PlayerInputController.Instance.SetActionMap("Gridmap");
        }

        protected virtual void CancelAction()
        {
            GridPosition gridPosition = unit.GetGridPosition();

            GridSystemVisual.Instance.HideAllGridPosition();
            UnitActionSystemUI.Instance.SelectUIBaseOnSelectedAction();
            UnitActionSystem.Instance.ClearBusyUI();
            Pointer.Instance.SetPointerOnGrid(gridPosition);
            PlayerInputController.Instance.SetActionMap("BattleUI");
            CameraController.Instance.SetCameraFocusToPosition(LevelGrid.Instance.GetWorldPosition(gridPosition));
            PlayerInputController.Instance.OnCancelActionPressed -= PlayerInputController_OnCancelPressed;
        }

        public void InitializeCancel()
        {
            PlayerInputController.Instance.OnCancelActionPressed += PlayerInputController_OnCancelPressed;
        }

        protected virtual void PlayerInputController_OnCancelPressed(object sender, EventArgs e)
        {
            if (isActive) return;
            CancelAction();
        }

        /// <summary>Gets a list of allowable grid positions for the action.</summary>
        /// <returns>A list of valid grid positions for the action.</returns>
        public abstract List<GridPosition> GetValidActionGridPosition();

        public string GetName() => actionName;
        public bool GetActionStatus() => unit.GetActionStatus(actionType);
        public ActionType GetActionType() => actionType;
        public virtual void SetActive(bool status) => isActive = status;
    }
}