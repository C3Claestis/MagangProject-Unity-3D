namespace Nivandria.Explore.Puzzle
{    
    using UnityEngine;
    using UnityEngine.InputSystem;
    public class PuzzleInputSystem : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;
        [SerializeField] Animator animator;
        private Vector2 movementValue;
        private bool isMoving = false;
        private bool canRunning = false;
        /// <summary>
        /// Untuk Lari Menggunakan Left Shift
        /// </summary>
        /// <param name="context"></param>
        public void RunAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                canRunning = true;

                if (isMoving)
                {
                    SetAnimatorRunning(true);
                }
            }

            if (context.canceled)
            {
                canRunning = false;
                SetAnimatorRunning(false);

                if (isMoving)
                {
                    SetAnimatorWalking(true);
                }
            }
        }

        /// <summary>
        /// Untuk Bergerak Sesuai WASD
        /// </summary>
        /// <param name="context"></param>
        public void MovingAction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                movementValue = context.ReadValue<Vector2>();
                isMoving = true;

                if (canRunning)
                {
                    SetAnimatorRunning(true);
                }
                else
                {
                    SetAnimatorWalking(true);
                }
            }

            if (context.canceled)
            {
                movementValue = context.ReadValue<Vector2>();
                isMoving = false;
                SetAnimatorRunning(false);
                SetAnimatorWalking(false);
            }
        }

        public void PuzzleSacraInteraction(InputAction.CallbackContext context)
        {
            if (context.performed)
            {

            }
        }
        public void PuzzleSacraInput(InputAction.CallbackContext context)
        {
            if (context.performed)
            {

            }
        }
        //////////////////////////////
        private void SetAnimatorRunning(bool value) => animator.SetBool("isRun", value);       
        private void SetAnimatorWalking(bool value) => animator.SetBool("isWalking", value);        
        public Vector2 GetMovementValue() => movementValue;
        public bool CanRunning() => canRunning;        
    }
}