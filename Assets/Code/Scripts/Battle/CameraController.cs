using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;

    [SerializeField] private float cameraMoveSpeed = 10f;
    [SerializeField] private float cameraZoomSpeed = 5f;
    private float widthMoveLimit;
    private float heighMoveLimit;
    private float moveAmmount = 1f;
    private float minZoomLimit = 2f;
    private float maxZoomLimit = 7f;
    private float zoomAmmount = 1f;


    private Vector3 targetFollowOffset;
    private CinemachineTransposer cinemachineTransposer;

    private void Start() {
        widthMoveLimit = LevelGrid.Instance.GetWidth() * 2f - 1f;
        heighMoveLimit = LevelGrid.Instance.GetHeight() * 2 - 1f;

        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }

    void Update(){
        HandleCameraMovement();
        HandleCameraZoom();
    }

    private void HandleCameraMovement(){
        Vector3 inputMoveDir = new Vector3(0, 0, 0);
        
        if (Input.GetKey(KeyCode.W))
        {
            inputMoveDir.z = moveAmmount;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputMoveDir.z = -moveAmmount;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputMoveDir.x = moveAmmount;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputMoveDir.x = -moveAmmount;
        }

        Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        Vector3 newPosition = transform.position + moveVector * cameraMoveSpeed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -1f, widthMoveLimit); // Restrict x movement between -1 and 15
        newPosition.z = Mathf.Clamp(newPosition.z, -1f, heighMoveLimit); // Restrict z movement between -1 and 7

        transform.position = newPosition;
    }

    private void HandleCameraZoom(){
        if(Input.mouseScrollDelta.y > 0){
            targetFollowOffset.y -=  zoomAmmount;
        }
        if(Input.mouseScrollDelta.y < 0){
            targetFollowOffset.y +=  zoomAmmount;
        }

        targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minZoomLimit, maxZoomLimit);
        cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * cameraZoomSpeed);
    }


    public Transform GetCameraController() => transform;
}
