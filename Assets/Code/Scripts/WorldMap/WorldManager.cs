using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask pointOfInterestMask;
    [SerializeField] private PlayerPointer playerPointer;

    private RoadMap roadMap;

    private void Awake() {
        if(Instance != null){
            Debug.LogError("There's more than one WorldManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;  
    }

    private void Start() {
        roadMap= new RoadMap();
        InitializeRoadMap();
    }

    void Update(){
        HandleRayCast();
    }

    private void InitializeRoadMap(){
        GameObject[] roadGameObject = GameObject.FindGameObjectsWithTag("Roads");
        if (roadGameObject != null){
            foreach (GameObject gameObject in roadGameObject){
                Road road = gameObject.GetComponent<Road>();
                string[] townNames = road.GetTownNames().ToArray();
                Debug.Log(road.name);
                roadMap.AddConnection(townNames[0], road.GetRoadName(), townNames[1]);
            }
        }
        else{
            Debug.LogError("GameObject with tag 'Roads' not found.");
        }
    }

    private void HandleRayCast(){
        if (Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, pointOfInterestMask);

            if (hit.collider != null){
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Hit Object: " + hitObject.name);
                playerPointer.MoveTo(hitObject.GetComponent<PointOfInterest>());
            }
        }
    }

    public List<string> FindRoadsBetweenTowns(string startTown, string targetTown) {
        List<string> path = roadMap.FindPath(startTown, targetTown);
        return roadMap.FindRoadsBetweenTowns(path);
    }

    public List<string> GetTownsConnectedByRoad(string roadName) => roadMap.GetTownsConnectedByRoad(roadName);

    public PointOfInterest FindTownByName(string townName){
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("PointOfInterest");
        foreach (GameObject town in gameObject) {
            if (town.name == townName){
                return town.GetComponent<PointOfInterest>();
            }
        }

        return null;
    }

    public Road FindRoadByName(string roadName){
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Roads");
        foreach (GameObject road in gameObject) {
            if (road.name == roadName){
                return road.GetComponent<Road>();
            }
        }

        return null;
    }
}
