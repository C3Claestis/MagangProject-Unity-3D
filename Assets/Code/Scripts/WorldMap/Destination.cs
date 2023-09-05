using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class Destination : MonoBehaviour{

    [SerializeField] private Transform icon;
    [SerializeField] private string townName;

    private float iconSizeMin = 0.1f;
    private float iconSizeMax = 0.25f;
    private float cameraMaxZoom;
    private float cameraMinZoom; 

    private void Start() {
        gameObject.name = townName;
        cameraMaxZoom = CameraController.Instance.GetMaxZoom();
        cameraMinZoom = CameraController.Instance.GetMinZoom();
    }

    private void Update() {
        HandleIconSize();
    }

    private void HandleIconSize(){
        float currentZoom = CameraController.Instance.GetZoomValue();
        float iconSize = Mathf.Lerp(iconSizeMin, iconSizeMax, Mathf.InverseLerp(cameraMinZoom, cameraMaxZoom, currentZoom));
        icon.localScale = new Vector3(iconSize, iconSize, iconSize);
    }

    public string GetName() => townName;
    public Vector3 GetPosition() => transform.position;
}