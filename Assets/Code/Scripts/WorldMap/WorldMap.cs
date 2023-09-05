using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class WorldMap
{
    private List<Destination> destinationList = new List<Destination>();

    public void AddDestination(Destination target){
        if (destinationList.Contains(target)){
            Debug.LogWarning("Add Failed! target is already on the list.");
            return;
        }

        destinationList.Add(target);
    }

    public void AddDestinations(List<Destination> targetList){
        foreach (Destination target in targetList){
            AddDestination(target);
        }
    }

    public void RemoveDestination(Destination target){
        if (!destinationList.Contains(target)){
            Debug.LogWarning("Remove Failed! Can't find target on the list.");
            return;
        }
        
        destinationList.Add(target);
    }

    public Destination FindDestination(string targetName){
        foreach (Destination item in destinationList){
            if(item.GetName() != targetName) continue;
            return item;
        }

        Debug.LogWarning("Find Failed! Can't find the destination!");
        return null;
    }



    public SplineComputer FindRoad(Destination from, Destination to){
        GameObject[] roadGameObjects = GameObject.FindGameObjectsWithTag("Roads");
        
        foreach (GameObject item in roadGameObjects){
            Road road = item.GetComponent<Road>();
            if(road.CheckDestination(from) && road.CheckDestination(to)){
                return road.GetSpline();
            }
        }

        Debug.LogWarning("Find Failed! Can't find the road!");
        return null;
    }

    
}