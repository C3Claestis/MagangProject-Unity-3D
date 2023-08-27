using System;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance { get; private set; }

    private Vector2 cameraMovement;
    private float cameraZoom;

    private bool cameraCanMove = false;  
    private bool cameraCanZoom = true;  

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

        if(context.performed) cameraCanMove = true; 
        else cameraCanMove = false;
    }

    public void CameraZoomAction(InputAction.CallbackContext context){
        if(context.started) return;

        cameraZoom = context.ReadValue<float>();
        
        // if(context.performed) cameraCanZoom = true; 
        // else cameraCanZoom = false;
    }

    public Vector2 GetCameraMovementValue() => cameraMovement;
    public float GetCameraZoomValue() => cameraZoom;

    public bool GetCameraMovementStatus() => cameraCanMove;
    public bool GetCameraZoomStatus() => cameraCanZoom;
}
