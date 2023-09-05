using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldManager : MonoBehaviour
{
    public static WorldManager Instance { get; private set; }

    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask destinationMask;
    [SerializeField] private PlayerPointer playerPointer;

    private WorldMap worldMap = new WorldMap();

    private void Awake() {
        if(Instance != null){
            Debug.LogError("There's more than one WorldManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;  
    }

    private void Start() {
        InitializeRoadMap();
    }

    void Update(){
        HandleRayCast();
    }

    private void InitializeRoadMap(){
        GameObject[] distanceGameObjects = GameObject.FindGameObjectsWithTag("Destinations");
        foreach (GameObject item in distanceGameObjects){
            worldMap.AddDestination(item.GetComponent<Destination>());
        }
    }

    private void HandleRayCast(){
        if (Input.GetMouseButtonDown(0)){
            Vector3 mousePosition = Input.mousePosition;
            Ray ray = mainCamera.ScreenPointToRay(mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, destinationMask);

            if (hit.collider != null){
                GameObject hitObject = hit.collider.gameObject;
                Debug.Log("Hit Object: " + hitObject.name);
                // playerPointer.MoveTo(hitObject.GetComponent<Destination>());
            }
        }
    }

}
