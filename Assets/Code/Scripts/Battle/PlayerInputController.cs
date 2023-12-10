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

        /* -------------------------------------------------------------------------- */
        /*                                ConfimationUI                               */
        /* -------------------------------------------------------------------------- */

        public void ConfimationUI_Cancel(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            OnCancelUIPressed?.Invoke(this, EventArgs.Empty);
        }

        /* -------------------------------------------------------------------------- */
        /*                                 ROTATE UNIT                                */
        /* -------------------------------------------------------------------------- */

        public void RotateUnit_Confirm(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            if (!PlacingUnitSystem.Instance.GetStatus())
            {
                PlacingUnitSystem.Instance.GetComponent<RotatePlaceUnit>().ConfirmRotation();
                return;
            }

            Transform turnButton = UnitActionSystemUI.Instance.GetTurnSystemButton();
            RotateAction rotateAction = turnButton.GetComponent<RotateAction>();
            rotateAction.ConfirmRotation();
        }

        public void RotateUnit_Rotate(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            float rotateValue = context.ReadValue<float>();

            if (!PlacingUnitSystem.Instance.GetStatus())
            {
                var rotate = PlacingUnitSystem.Instance.GetComponent<RotatePlaceUnit>();
                if (rotateValue > 0) rotate.RotateRight();
                else if (rotateValue < 0) rotate.RotateLeft();
                return;
            }

            Transform turnButton = UnitActionSystemUI.Instance.GetTurnSystemButton();
            RotateAction rotateAction = turnButton.GetComponent<RotateAction>();

            if (rotateValue > 0) rotateAction.RotateRight();
            else if (rotateValue < 0) rotateAction.RotateLeft();
        }

        /* -------------------------------------------------------------------------- */
        /*                                PlacingUnits                                */
        /* -------------------------------------------------------------------------- */

        public void PlacingUnits_CameraZoom(InputAction.CallbackContext context)
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

        public void PlacingUnits_CameraMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                cameraMovementInputValue = context.ReadValue<Vector2>();
            }

            if (!context.canceled) return;
            cameraMovementInputValue = context.ReadValue<Vector2>();
        }

        public void PlacingUnits_SelectGrid(InputAction.CallbackContext context)
        {
            if (context.performed) PlacingUnitSystem.Instance.PlacingUnit();
        }

        public void PlacingUnits_CancelAction(InputAction.CallbackContext context)
        {
            if (context.performed) PlacingUnitSystem.Instance.Cancel_Action();
        }

        public void PlacingUnits_ChangeUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Debug.Log("Change unit");
            float changeValue = context.ReadValue<float>();
            PlacingUnitSystem.Instance.ChangeSelectedUnit(changeValue);
        }

        public void PlacingUnits_DeleteUnit(InputAction.CallbackContext context)
        {
            if (context.performed) PlacingUnitSystem.Instance.DeleteUnit();
        }

        /* -------------------------------------------------------------------------- */
        /*                                   GridMap                                  */
        /* -------------------------------------------------------------------------- */

        public void GridMap_SelectGrid(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UnitActionSystem.Instance.HandleSelectedAction(context.control.name);
        }

        public void GridMap_NextUnit(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            UnitTurnSystem.Instance.HandleUnitSelection();
        }

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

        public void GridMap_CameraMovement(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                cameraMovementInputValue = context.ReadValue<Vector2>();
            }

            if (!context.canceled) return;
            cameraMovementInputValue = context.ReadValue<Vector2>();
        }

        /* -------------------------------------------------------------------------- */
        /*                              OnControlsChanged                             */
        /* -------------------------------------------------------------------------- */

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