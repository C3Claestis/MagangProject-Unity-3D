using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{

    private Vector2 cameraMovement;
    private float cameraZoom;

    private bool cameraCanMove = false;  

    public void CameraMovementAction(InputAction.CallbackContext context){
        if(context.started) return;

        cameraMovement = context.ReadValue<Vector2>();

        if(context.performed) cameraCanMove = true; 
        else cameraCanMove = false;
    }

    public void CameraZoomAction(InputAction.CallbackContext context){
        if(context.started) return;

        cameraZoom = context.ReadValue<float>();
    }

    public Vector2 GetCameraMovementValue() => cameraMovement;
    public float GetCameraZoomValue() => cameraZoom;

    public bool GetCameraMovementStatus() => cameraCanMove;
}
