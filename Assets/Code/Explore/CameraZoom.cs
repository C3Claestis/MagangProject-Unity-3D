using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 1.0f;
    public float minFOV = 20.0f;
    public float maxFOV = 85.0f;
    
    public float scrollInput;

    private void Update()
    { 
        float newFOV = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFOV, maxFOV);
        Mathf.Clamp(scrollInput, 20, 85);
        virtualCamera.m_Lens.FieldOfView = newFOV;                

        HandleZoom();
    }
    void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            //scrollInput--;
            virtualCamera.m_Lens.FieldOfView--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            //scrollInput++;
            virtualCamera.m_Lens.FieldOfView++;
        }
    }
}
