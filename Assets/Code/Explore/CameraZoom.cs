using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float zoomSpeed = 1.0f;
    public float minFOV = 40.0f;
    public float maxFOV = 85.0f;    
    
    private void Update()
    { 
        //Membatasi jarak zoom
        float newFOV = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minFOV, maxFOV);       
        virtualCamera.m_Lens.FieldOfView = newFOV;                

        HandleZoom();
    }
    void HandleZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {            
            virtualCamera.m_Lens.FieldOfView--;
        }
        if (Input.mouseScrollDelta.y < 0)
        {         
            virtualCamera.m_Lens.FieldOfView++;
        }
    }
}
