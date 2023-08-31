using System;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraMoveSpeed = 3f;
    [SerializeField] private float smoothTime = 0.2f;
    [SerializeField] private float zoomMultiplier = 0.1f;
    private float zoom;
    private float minZoom = 0.5f;
    private float maxZoom = 2.4f;
    private float velocity = 0f;

    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    private PlayerInputController playerInputController;




    private void Start() {
        playerInputController = GetComponent<PlayerInputController>();

    }

    private void Update(){
        HandleCameraMovement();
        HandleCameraZoom();
    }

    /// <summary>Handles camera movement based on player input.
    /// </summary>
    private void HandleCameraMovement(){
        if (!playerInputController.GetCameraMovementStatus()) return;

        Vector2 cameraMovement = playerInputController.GetCameraMovementValue();
        Vector3 inputMoveDir = new Vector3(cameraMovement.x, cameraMovement.y, 0);
        Vector3 newPosition = transform.position + inputMoveDir * cameraMoveSpeed  * Time.deltaTime * cinemachineVirtualCamera.m_Lens.OrthographicSize; 

        newPosition.x = Mathf.Clamp(newPosition.x, -4.73f, 4.73f);
        newPosition.y = Mathf.Clamp(newPosition.y, -2.41f, 2.41f);


        transform.position = newPosition;
    }

    /// <summary>Handles camera zoom based on player input.
    /// </summary>
    private void HandleCameraZoom(){
        float cameraZoomValue = playerInputController.GetCameraZoomValue();
        float zoomValue = cinemachineVirtualCamera.m_Lens.OrthographicSize;
        
        if(cameraZoomValue > 0) zoom -=  zoomMultiplier;
        if(cameraZoomValue < 0) zoom +=  zoomMultiplier;
        

        zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
        cinemachineVirtualCamera.m_Lens.OrthographicSize = Mathf.SmoothDamp(zoomValue, zoom, ref velocity, smoothTime);
    }


}
