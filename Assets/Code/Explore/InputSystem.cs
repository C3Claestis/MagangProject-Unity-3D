using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private Vector2 movementValue;
    private bool isMoving = false;
    private bool canRunning = false;

    public void RunAction(InputAction.CallbackContext context){
        if (context.performed){
            canRunning = true;

            if (isMoving){
                SetAnimatorRunning(true);
            }
        }

        if (context.canceled){
            canRunning = false;
            SetAnimatorRunning(false);

            if (isMoving){
                SetAnimatorWalking(true);
                Debug.Log("isWalking: " + animator.GetBool("isWalking") + "; isRunning: " + animator.GetBool("isRunning"));
            }
        }
    }

    public void MovingAction(InputAction.CallbackContext context){
        if (context.performed){
            movementValue = context.ReadValue<Vector2>();
            isMoving = true;

            if (canRunning){
                SetAnimatorRunning(true);
            }
            else{
                SetAnimatorWalking(true);
            }
        }

        if (context.canceled){
            movementValue = context.ReadValue<Vector2>();
            isMoving = false;
            SetAnimatorRunning(false);
            SetAnimatorWalking(false);
        }
    }

    public void InteractionAction(InputAction.CallbackContext context){
        if (context.performed){
            Debug.Log("Interaksi");
        }
    }

    public void JumpAction(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("Loncat");
        }
    }

    //////////////////////////////
    private void SetAnimatorRunning(bool value) => animator.SetBool("isRunning", value);
    private void SetAnimatorWalking(bool value) => animator.SetBool("isWalking", value);

    public Vector2 GetMovementValue() => movementValue;
    public bool CanRunning() => canRunning;   
}
