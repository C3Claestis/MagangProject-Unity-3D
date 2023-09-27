namespace Nivandria.Explore.Puzzle
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    /// <summary>
    /// Handles player input and controls character animations in a puzzle environment.
    /// </summary>
    public class PuzzleInputSystem : MonoBehaviour
    {
        [SerializeField] PlayerInput playerInput;     // Reference to the player input component.
        [SerializeField] Animator animator;            // Reference to the character's Animator component.
        
        private Vector2 movementValue;                 // The current movement input.
        private bool isMoving = false;                 // Flag to indicate if the character is moving.
        private bool canRunning = false;               // Flag to indicate if the character can run.

        /// <summary>
        /// Handles the "Run" action, activated by Left Shift.
        /// </summary>
        /// <param name="context">The input action context.</param>
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
        /// Handles the "Moving" action, controlling character movement using WASD.
        /// </summary>
        /// <param name="context">The input action context.</param>
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

        // Additional input handling methods can be added here.

        // Sets the "isRun" parameter in the Animator.
        private void SetAnimatorRunning(bool value) => animator.SetBool("isRun", value);

        // Sets the "isWalking" parameter in the Animator.
        private void SetAnimatorWalking(bool value) => animator.SetBool("isWalking", value);

        // Returns the current movement input.
        public Vector2 GetMovementValue() => movementValue;

        // Checks if the character can run.
        public bool CanRunning() => canRunning;
    }
}