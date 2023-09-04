using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;    
    [SerializeField] private InteraksiNPC interaksiNPC;
    [SerializeField] PlayerInput playerInput;
    private Vector2 movementValue, insertValue;
    private bool isMoving = false;
    private bool canRunning = false;
    
    public void UIAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            insertValue = context.ReadValue<Vector2>();
        }
    }
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

    public void InteractionAction(InputAction.CallbackContext context)
    {
        if (interaksiNPC.isNPC)
        {
            if (context.performed && !interaksiNPC.isTalk)
            {                
                interaksiNPC.isTalk = true;
                playerInput.SwitchCurrentActionMap("UI");
                interaksiNPC.interaksi.text = "Sedang Interaksi";
                animator.SetBool("isWalking", false);
                animator.SetBool("isRun", false);
            }
            else if (context.performed && interaksiNPC.isTalk)
            {             
                interaksiNPC.interaksi.text = "";
                interaksiNPC.isTalk = false;
                playerInput.SwitchCurrentActionMap("Player");
            }
        }        
    }
    //////////////////////////////
    private void SetAnimatorRunning(bool value)
    {
        if(!interaksiNPC.isTalk)
            animator.SetBool("isRun", value);
    }
    private void SetAnimatorWalking(bool value)
    {
        if (!interaksiNPC.isTalk)
            animator.SetBool("isWalking", value);
    }
    public Vector2 GetMovementValue() => movementValue;
    public bool CanRunning() => canRunning;    
}