using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Represents a road map connecting towns and roads.
/// </summary>
public class RoadMap
{
    private Dictionary<string, List<string>> townConnections = new Dictionary<string, List<string>>();

    /// <summary> Adds a connection between two towns via a road if it doesn't already exist.
    /// </summary>
    /// <param name="townA">The first town.</param>
    /// <param name="road">The name of the road.</param>
    /// <param name="townB">The second town.</param>
    public void AddConnection(string townA, string road, string townB)
    {
        if (!townConnections.ContainsKey(townA))
        {
            townConnections[townA] = new List<string>();
        }

        if (!townConnections.ContainsKey(townB))
        {
            townConnections[townB] = new List<string>();
        }

        string newConnection = $"{road} to {townB}";

        if (!townConnections[townA].Contains(newConnection))
        {
            townConnections[townA].Add(newConnection);
            townConnections[townB].Add($"{road} to {townA}");
        }
        else
        {
            // Connection already exists, you can handle this case as needed
            Debug.LogError($"Connection between {townA} and {townB} via {road} already exists.");
        }
    }

    /// <summary> Removes a connection between two towns via a road.
    /// </summary>
    /// <param name="townA">The first town.</param>
    /// <param name="townB">The second town.</param>
    public void RemoveConnection(string townA, string townB)
    {
        if (townConnections.ContainsKey(townA) && townConnections.ContainsKey(townB))
        {
            townConnections[townA].Remove($"{GetRoadBetweenTowns(townA,townB)} to {townB}");
            townConnections[townB].Remove($"{GetRoadBetweenTowns(townB,townA)} to {townA}");
        }
    }

    /// <summary> Retrieves a list of towns connected by a specified road.
    /// </summary>
    /// <param name="roadName">The name of the road.</param>
    /// <returns>A list of strings representing towns connected by the specified road.</returns>
    public List<string> GetTownsConnectedByRoad(string roadName)
    {
        List<string> connectedTowns = new List<string>();

        foreach (var town in townConnections.Keys)
        {
            foreach (var connection in townConnections[town])
            {
                string[] parts = connection.Split(new[] { " to " }, StringSplitOptions.RemoveEmptyEntries);
                string road = parts[0];
                string connectedTown = parts[1];

                if (road == roadName)
                {
                    connectedTowns.Add(connectedTown);
                }
            }
        }

        return connectedTowns;
    }

    /// <summary> Retrieves the road that connects two specified towns.
    /// </summary>
    /// <param name="townA">The first town.</param>
    /// <param name="townB">The second town.</param>
    /// <returns>A string representing the road that connects the two specified towns, or null if no such road exists.</returns>
    public string GetRoadBetweenTowns(string townA, string townB)
    {
        if (townConnections.ContainsKey(townA))
        {
            foreach (var connection in townConnections[townA])
            {
                string[] parts = connection.Split(new[] { " to " }, StringSplitOptions.RemoveEmptyEntries);
                string road = parts[0];
                string connectedTown = parts[1];

                if (connectedTown == townB)
                {
                    return road;
                }
            }
        }

        return null; // Return null if no road connects the specified towns.
    }

    /// <summary> Retrieves the list of connections for a given town.
    /// </summary>
    /// <param name="town">The town for which to retrieve connections.</param>
    /// <returns>A list of connections for the specified town.</returns>
    public List<string> GetConnection(string town)
    {
        if (townConnections.ContainsKey(town))
        {
            return townConnections[town];
        }
        else
        {
            return new List<string>(); // Return an empty list if the town has no connections.
        }
    }

    /// <summary> Retrieves a list of all town connections in the road map.
    /// </summary>
    /// <returns>A list of strings representing town connections in the format "TownA - Road - TownB".</returns>
    public List<string> GetAllConnections()
    {
        List<string> allConnections = new List<string>();

        foreach (var town in townConnections.Keys)
        {
            foreach (var connection in townConnections[town])
            {
                allConnections.Add($"{town} - {connection}");
            }
        }

        return allConnections;
    }

    /// <summary> Finds a path between two towns in the road map.
    /// </summary>
    /// <param name="startTown">The starting town.</param>
    /// <param name="targetTown">The target town.</param>
    /// <returns>A list of towns representing the path from startTown to targetTown, or null if no path exists.</returns>
    public List<string> FindPath(string startTown, string targetTown)
    {
        Dictionary<string, string> pathMap = new Dictionary<string, string>();
        Queue<string> queue = new Queue<string>();

        queue.Enqueue(startTown);
        pathMap[startTown] = null;

        while (queue.Count > 0){
            string currentTown = queue.Dequeue();

            if (currentTown == targetTown){  // Found the target town, backtrack to construct the path
                List<string> path = new List<string>();
                while (currentTown != null){
                    path.Add(currentTown);
                    currentTown = pathMap[currentTown];
                }
                path.Reverse();
                return path;
            }

            if (townConnections.ContainsKey(currentTown))
            {
                foreach (string connection in townConnections[currentTown])
                {
                    string[] parts = connection.Split(new[] { " to " }, StringSplitOptions.RemoveEmptyEntries);
                    string road = parts[0];
                    string nextTown = parts[1];

                    if (!pathMap.ContainsKey(nextTown))
                    {
                        queue.Enqueue(nextTown);
                        pathMap[nextTown] = currentTown;
                    }
                }
            }
        }

        return null; // No path found
    }

    /// <summary> Finds a list of roads from a starting town to a target town in the road map.
    /// </summary>
    /// <param name="startTown">The starting town.</param>
    /// <param name="targetTown">The target town.</param>
    /// <returns>A list of road names representing the path from startTown to targetTown, or null if no path exists.</returns>
    public List<string> FindRoadsBetweenTowns(List<string> path)
    {
        List<string> roads = new List<string>();

        if (path == null)
        {
            return null; // No path exists.
        }

        for (int i = 0; i < path.Count - 1; i++)
        {
            string currentTown = path[i];
            string nextTown = path[i + 1];

            // Find the road between the current town and the next town
            string road = GetRoadBetweenTowns(currentTown, nextTown);

            if (road != null)
            {
                roads.Add(road);
            }
            else
            {
                // Handle cases where a road is missing (if needed)
                Debug.LogError($"No road found between {currentTown} and {nextTown}.");
            }
        }

        return roads;
    }

}