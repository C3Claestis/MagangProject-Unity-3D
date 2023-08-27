using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }

    private Vector2 cameraMovement;
    private float cameraZoom;  

    private void Start() {
        if(Instance != null){
            Debug.LogError("There's more than one PlayerInputController! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;    
    }

    public void CameraMovementAction(InputAction.CallbackContext context){
        if(context.started) return;
        cameraMovement = context.ReadValue<Vector2>();
    }

    public void CameraZoomAction(InputAction.CallbackContext context){
        if(!context.performed) return;
        cameraZoom = context.ReadValue<float>();
    }

    public Vector2 GetCameraMovement() => cameraMovement;
    public float GetCameraZoom() => cameraZoom;
}
