using System;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class PointOfInterest : MonoBehaviour{

    [SerializeField] private Transform icon;
    [SerializeField] private string townName;

    [SerializeField] private Road[] roadList;

    private float iconSizeMin = 0.1f;
    private float iconSizeMax = 0.25f;
    private float cameraMaxZoom;
    private float cameraMinZoom; 

    private void Start() {
        gameObject.name = townName;
        cameraMaxZoom = CameraController.Instance.GetMaxZoom();
        cameraMinZoom = CameraController.Instance.GetMinZoom();

        HadleRoadSetting();
    }

    private void Update() {
        HandleIconSize();
    }

    private void HandleIconSize(){
        float currentZoom = CameraController.Instance.GetZoomValue();
        float iconSize = Mathf.Lerp(iconSizeMin, iconSizeMax, Mathf.InverseLerp(cameraMinZoom, cameraMaxZoom, currentZoom));
        icon.localScale = new Vector3(iconSize, iconSize, iconSize);
    }

    private void HadleRoadSetting(){
        if(roadList == null) return;

        foreach (Road road in roadList){
            road.SetNearestPointPosition(transform.position);
            road.AddTown(townName);
        }
    }

    public Road GetRoad(string roadName){
        foreach (Road road in roadList){
            if (road.GetRoadName() == roadName){
                return road;
            }
        }
        return null;
    }
    public Road[] GetRoadList() => roadList;

    public string GetTownName() => townName;
}