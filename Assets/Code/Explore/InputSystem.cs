using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMov playerMov;
    [SerializeField] private InteraksiNPC interaksiNPC;
    private Vector2 movementValue;
    private bool isMoving = false;
    private bool canRunning = false;
    
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
                Debug.Log("isWalking: " + animator.GetBool("isWalking") + "; isRun: " + animator.GetBool("isRun"));
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
        if (context.performed && !interaksiNPC.isTalk)
        {
            Debug.Log("Interaksi");
            interaksiNPC.isTalk = true;            
        }
        else if (context.performed && interaksiNPC.isTalk)
        {
            Debug.Log("Tidak Interaksi");
            interaksiNPC.isTalk = false;
        }
    }

    /*
    public void JumpAction(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Loncat");
            SetJumping(true);
        }
        if (context.canceled)
        {
            Debug.Log("Non Lompat");
            SetJumping(false);
        }
    }
    */
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
    /*
    private void SetJumping(bool value)
    {
        if (!interaksiNPC.isTalk)
            //playerMov.Jump(value);
    }*/
    public Vector2 GetMovementValue() => movementValue;
    public bool CanRunning() => canRunning;    
}