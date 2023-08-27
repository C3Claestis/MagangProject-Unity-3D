using System.Collections;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraZoomSpeed = 3f;
    
    private float widthMoveLimit;
    private float heighMoveLimit;

    private float minZoomLimit = 2f;
    private float maxZoomLimit = 7f;
    private float zoomAmmount = 1f;

    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;
    private PlayerInputController playerInputController;

    private void Start() {
        widthMoveLimit = LevelGrid.Instance.GetWidth() * 2f - 1f;
        heighMoveLimit = LevelGrid.Instance.GetHeight() * 2 - 1f;

        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        playerInputController = PlayerInputController.Instance;

        transform.position = new Vector3(widthMoveLimit/2, 0 , 1);
        targetFollowOffset.y = maxZoomLimit;
    }

    void Update(){
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement(){
        Vector2 cameraMovement = playerInputController.GetCameraMovement();

        if(cameraMovement == Vector2.zero) return;

        Vector3 inputMoveDir = new Vector3(cameraMovement.x, 0, cameraMovement.y);
        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        Vector3 newPosition = transform.position + moveVector * cameraMoveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -1f, widthMoveLimit); // Restrict x movement between -1 and 15
        newPosition.z = Mathf.Clamp(newPosition.z, -1f, heighMoveLimit); // Restrict z movement between -1 and 7

        transform.position = newPosition;
    }

    private void HandleCameraZoom(){
        float cameraZoomValue = playerInputController.GetCameraZoom();

        if(cameraZoomValue > 0){
            targetFollowOffset.y -=  zoomAmmount;
        }

        if(cameraZoomValue < 0){
            targetFollowOffset.y +=  zoomAmmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minZoomLimit, maxZoomLimit);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * cameraZoomSpeed);
    }

    public Transform GetCameraController() => transform;
}
