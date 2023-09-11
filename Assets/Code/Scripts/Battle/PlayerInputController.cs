namespace Nivandria.Battle
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInputController : MonoBehaviour
    {
        public static PlayerInputController Instance { get; private set; }

        private Vector2 cameraMovement;
        private float cameraZoom;

        private bool cameraCanMove = false;
        private bool cameraCanZoom = true;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one PlayerInputController! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        /// <summary> Handles camera movement input actions. </summary>
        /// <param name="context">The input action context.</param>
        public void CameraMovementAction(InputAction.CallbackContext context)
        {
            if (context.started) return;

            cameraMovement = context.ReadValue<Vector2>();

            if (context.performed) cameraCanMove = true;
            else cameraCanMove = false;
        }

        /// <summary> Handles camera zoom input actions. </summary>
        /// <param name="context">The input action context.</param>
        public void CameraZoomAction(InputAction.CallbackContext context)
        {
            if (context.started) return;

            cameraZoom = context.ReadValue<float>();
        }


        #region Getter Setter
        public Vector2 GetCameraMovementValue() => cameraMovement;
        public float GetCameraZoomValue() => cameraZoom;
        public bool GetCameraMovementStatus() => cameraCanMove;
        public bool GetCameraZoomStatus() => cameraCanZoom;
        #endregion
    }
}