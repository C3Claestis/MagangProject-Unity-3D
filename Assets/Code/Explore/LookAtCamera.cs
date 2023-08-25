using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraTransform;
    
    private void Awake()
    {
        cameraTransform = Camera.main.transform;
    }
    private void LookAtBackwards(Vector3 targetPos) 
    {
        Vector3 offset = transform.position - targetPos;
        transform.LookAt(transform.position + offset);       
    }
    private void LateUpdate()
    {
        LookAtBackwards(cameraTransform.position);
    }
}
