using System.Collections.Generic;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    private SplinePositioner splinePositioner;

    [SerializeField] private Transform icon;
    private float iconSizeMin = 0.3f;
    private float iconSizeMax = 0.7f;
    private float cameraMaxZoom;
    private float cameraMinZoom; 

    private float normalMoveSpeed = 0.2f;
    private float fastMoveSpeed = 0.6f;
    [SerializeField] private bool fastSpeedEnable = false;
    private bool isMoving = false;
    private double moveProgress = 0.0;

    [SerializeField] private Road currentRoad;
    [SerializeField] private PointOfInterest currentTown;
    private PointOfInterest targetTown;
    private List<string> path;

    private void Awake() {
        splinePositioner = GetComponent<SplinePositioner>();
    }

    private void Start() {
        cameraMaxZoom = CameraController.Instance.GetMaxZoom();
        cameraMinZoom = CameraController.Instance.GetMinZoom();
        path = new List<string>();

        if (currentTown != null) {
            SetPlayerPosition(currentTown.transform.position);
        }
    }

    private void Update() {
        HandleIconSize();
        HandlePlayerMove();
    }

    private void HandleIconSize(){
        float currentZoom = CameraController.Instance.GetZoomValue();
        float iconSize = Mathf.Lerp(iconSizeMin, iconSizeMax, Mathf.InverseLerp(cameraMinZoom, cameraMaxZoom, currentZoom));
        icon.localScale = new Vector3(iconSize, iconSize, iconSize);
    }

    private void HandlePlayerMove(){
        if (!isMoving) return;
        if (moveProgress >= 1.0) {
            isMoving = false;
            ChangeCurrentTown();
            CheckIfTarget();
        }

        moveProgress += (fastSpeedEnable ? fastMoveSpeed : normalMoveSpeed) * Time.deltaTime;
        moveProgress = Mathf.Clamp01((float)moveProgress);
        splinePositioner.SetPercent(moveProgress);
    }

    public void MoveTo(PointOfInterest targetTown){
        if (targetTown == null){
            Debug.LogError("townTarget is Null!");
            return;
        }

        if(isMoving){
            Debug.LogWarning("Character is still moving!");
            return;
        }

        if (targetTown == currentTown){
            Debug.LogWarning($"You are currently on {targetTown.GetTownName()}!");
            return;
        }

        Debug.Log("Move to " + targetTown);
        this.targetTown = targetTown;
        path = WorldManager.Instance.FindRoadsBetweenTowns(currentTown.name, targetTown.name);
        ChangeCurrentRoad();
        ResetMoveProgress();
        SetMoveStatus(true);
    }

    private void CheckIfTarget(){
        if (targetTown == currentTown){
            Debug.Log("You arrived!");
            return;
        }

        path.Remove(currentRoad.gameObject.name);
        ChangeCurrentRoad();
        ResetMoveProgress();
        SetMoveStatus(true);
    }

    private void CheckIfRoadReversed(){
        int checkNearest = currentRoad.FindNearestPoint(currentTown.transform.position);
        if(checkNearest != 0) currentRoad.ReversePointOrder();
    }

    private void ChangeCurrentRoad(){
        if (path.Count <= 0) return;
        currentRoad = WorldManager.Instance.FindRoadByName(path[0]);
        splinePositioner.spline = currentRoad.GetSpline();
        CheckIfRoadReversed();
    }

    private void ChangeCurrentTown(){
        List<string> townList = WorldManager.Instance.GetTownsConnectedByRoad(currentRoad.GetRoadName());
        foreach (string town in townList){
            if(town == currentTown.GetTownName()) continue;
            currentTown = WorldManager.Instance.FindTownByName(town);
            Debug.Log(currentTown);
            break;
        }
    }

    public void ResetMoveProgress() => moveProgress = 0.0;

    public double GetMoveProgress() => moveProgress;
    public bool GetMoveStatus() => isMoving;

    public void SetPlayerPosition(Vector3 targetPosition) => transform.position = targetPosition;
    public void SetMoveStatus(bool status) => isMoving = status;  
}
