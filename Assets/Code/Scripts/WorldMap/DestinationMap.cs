namespace WorldMap
{
    using System.Collections.Generic;
    using Dreamteck.Splines;
    using UnityEngine;

    public class DestinationMap
    {
        private List<Road> roadList = new List<Road>();

        public void AddRoad(Road target)
        {
            if (roadList.Contains(target))
            {
                Debug.LogWarning("Add Failed! road is already on the list.");
                return;
            }

            roadList.Add(target);
        }

        public void RemoveRoad(Road target)
        {
            if (!roadList.Contains(target))
            {
                Debug.LogWarning("Remove Failed! Can't find target on the list.");
                return;
            }

            AddRoad(target);
        }

        public Destination FindDestinationByName(string targetName)
        {
            if (roadList.Count <= 0)
            {
                Debug.LogError("Road List is empty!");
            }

            foreach (Road road in roadList)
            {
                Destination target = road.GetDestinationByName(targetName);
                if (target == null) continue;
                return target;
            }

            return null;
        }

        //Find oposite destination from this road
        public Destination FindEndDestination(Destination from, Road road)
        {
            List<Destination> destinationList = road.GetDestinationList();
            int index = destinationList.IndexOf(from);

            if (destinationList.Count > 2 || destinationList.Count <= 1)
            {
                Debug.LogError("Make sure to attach road to 2 destination!");
                return null;
            }

            if (index == 0) return destinationList[1];
            else return destinationList[0];
        }

        //Find road that connected two destination
        public SplineComputer FindRoad(Destination from, Destination to)
        {
            GameObject[] roadGameObjects = GameObject.FindGameObjectsWithTag("Roads");

            foreach (GameObject item in roadGameObjects)
            {
                Road road = item.GetComponent<Road>();
                if (road.CheckDestination(from) && road.CheckDestination(to))
                {
                    return road.GetSpline();
                }
            }

            Debug.LogWarning("Find Failed! Can't find the road!");
            return null;
        }


    }
}