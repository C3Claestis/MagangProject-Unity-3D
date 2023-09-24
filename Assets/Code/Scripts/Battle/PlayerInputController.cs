namespace Nivandria.Battle
{
    using System;
    using Nivandria.Battle.Action;
    using Nivandria.Battle.UI;
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInputController : MonoBehaviour
    {
        public static PlayerInputController Instance { get; private set; }

        public delegate void ActionMapChangedEventHandler(object sender, string actionMap);
        public event ActionMapChangedEventHandler OnActionMapChanged;
        public event EventHandler OnCancelActionPressed;
        public event EventHandler OnInputControlChanged;
        public event EventHandler OnCancelUIPressed;

        private PlayerInput playerInput;
        private Vector2 cameraMovementInputValue;
        private float cameraZoomInputValue;
        private bool isCurrentControllerGamepad;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one PlayerInputController! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }

            Instance = this;

            playerInput = GetComponent<PlayerInput>();
        }

        public void Cancel_Action(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            OnCancelActionPressed?.Invoke(this, EventArgs.Empty);
        }

        public void PauseGame(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Debug.Log("Pause Game");
        }

        ///============================CANCEL CONFIRMATION============================///

        public void ConfimationUI_Cancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            OnCancelUIPressed?.Invoke(this, EventArgs.Empty);
        }

        ///============================ROTATE UNIT============================///

        public void RotateUnit_Confirm(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Transform turnButton = UnitActionSystemUI.Instance.GetTurnSystemButton();
            RotateAction rotateAction = turnButton.GetComponent<RotateAction>();

            rotateAction.ConfirmRotation();
        }

        public void RotateUnit_Rotate(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            Transform turnButton = UnitActionSystemUI.Instance.GetTurnSystemButton();
            RotateAction rotateAction = turnButton.GetComponent<RotateAction>();
            float rotateValue = context.ReadValue<float>();

            if (rotateValue > 0) rotateAction.RotateRight();
            else if (rotateValue < 0) rotateAction.RotateLeft();
        }

        ///============================SELECT GRID============================///

        public void GridMap_SelectGrid(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UnitActionSystem.Instance.HandleSelectedAction(context.control.name);
        }

        ///============================NEXT UNIT============================///

        public void GridMap_NextUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UnitTurnSystem.Instance.HandleUnitSelection();
        }

        ///============================CAMERA ZOOM============================///

        public void GridMap_CameraZoom(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                float zoomValue = context.ReadValue<float>();

                if (zoomValue > 0) cameraZoomInputValue = 1f;
                else if (zoomValue < 0) cameraZoomInputValue = -1f;
            }

            if (context.canceled)
            {
                cameraZoomInputValue = 0;
            }
        }

        ///============================CAMERA MOVEMENT============================///

        public void GridMap_CameraMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                cameraMovementInputValue = context.ReadValue<Vector2>();
            }

            if (!context.canceled) return;
            cameraMovementInputValue = context.ReadValue<Vector2>();
        }

        ///============================OnControlsChanged============================///

        public void PlayerInput_onControlsChanged(PlayerInput input)
        {

            string inputControl = input.currentControlScheme;

            isCurrentControllerGamepad = inputControl == "Gamepad";

            Debug.Log("Current input is now : " + inputControl);

            OnInputControlChanged?.Invoke(this, EventArgs.Empty);
        }

        ///========================================================================///


        public void SetActionMap(string actionMap)
        {
            playerInput.SwitchCurrentActionMap(actionMap);
            OnActionMapChanged?.Invoke(this, actionMap);

            if (actionMap == "BattleUI" || actionMap == "ConfirmationUI")
            {
                playerInput.uiInputModule.enabled = true;
            }
            else
            {
                playerInput.uiInputModule.enabled = false;
            }
        }

        public Vector2 GetCameraMovementInputValue() => cameraMovementInputValue;
        public float GetCameraZoomInputValue() => cameraZoomInputValue;
        public bool IsCurrentControllerGamepad() => isCurrentControllerGamepad;

    }
}