namespace Nivandria.Battle
{
    using Nivandria.Battle.UnitSystem;
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInputController : MonoBehaviour
    {
        public static PlayerInputController Instance { get; private set; }

        public delegate void ActionMapChangedEventHandler(object sender, string actionMap);
        public event ActionMapChangedEventHandler OnActionMapChanged;

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


        ///============================ROTATE UNIT============================///

        public void RotateUnit_Confirm(InputAction.CallbackContext context)
        {
            if (!context.performed) return;
            Unit unit = UnitTurnSystem.Instance.GetSelectedUnit();
            unit.GetRotateAction().ConfirmRotation();
        }

        public void RotateUnit_Rotate(InputAction.CallbackContext context)
        {
            if (!context.performed) return;

            float rotateValue = context.ReadValue<float>();
            Unit unit = UnitTurnSystem.Instance.GetSelectedUnit();

            if (rotateValue > 0) unit.GetRotateAction().RotateRight();
            else if (rotateValue < 0) unit.GetRotateAction().RotateLeft();
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

            if (!context.canceled) return;
            cameraZoomInputValue = 0;

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
            switch (input.currentControlScheme)
            {
                case "Keyboard":
                    Debug.Log("Current controller is now Keyboard.");
                    isCurrentControllerGamepad = false;
                    break;

                case "Gamepad":
                    Debug.Log("Current controller is now Gamepad.");
                    isCurrentControllerGamepad = true;
                    break;
            }
        }

        ///========================================================================///


        public void SetActionMap(string actionMap)
        {
            playerInput.SwitchCurrentActionMap(actionMap);
            OnActionMapChanged?.Invoke(this, actionMap);
        }

        public Vector2 GetCameraMovementInputValue() => cameraMovementInputValue;
        public float GetCameraZoomInputValue() => cameraZoomInputValue;
        public bool IsCurrentControllerGamepad() => isCurrentControllerGamepad;

    }
}