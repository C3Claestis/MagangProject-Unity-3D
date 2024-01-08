namespace Nivandria.Battle.PathfindingSystem
{
    using Nivandria.Battle.Grid;
    using Nivandria.Battle.UnitSystem;
    using System.Collections.Generic;
    using UnityEngine;

    public class Pathfinding : MonoBehaviour
    {
        public static Pathfinding Instance { get; private set; }

        [SerializeField] private Transform gridDebugObjectPrefab;
        [SerializeField] private LayerMask obstaclesLayerMask;
        [SerializeField] private Transform debugParent;

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private int width;
        private int height;
        private float cellSize;

        private GridSystem<PathNode> gridSystem;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There's more than one Pathfinding! " + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;


        }

        /// <summary>Sets up the grid system for the pathfindingh with specified width, height, and cell size.</summary>
        /// <param name="width">The width of the grid.</param>
        /// <param name="height">The height of the grid.</param>
        /// <param name="cellSize">The size of each grid cell.</param>
        public void SetupGrid(int width, int height, float cellSize)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;

            SetupPath(UnitType.Ground);
        }

        public void SetupPath(UnitType unitType)
        {

            gridSystem = new GridSystem<PathNode>(width, height, cellSize,
                (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
            // gridSystem.CreateDebugObjects(gridDebugObjectPrefab, debugParent);

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(gridPosition))
                    {
                        Unit unit = LevelGrid.Instance.GetUnitListAtGridPosition(gridPosition)[0];
                        if (!unit.IsAlive()) continue;
                        if (unit.IsEnemy() == UnitTurnSystem.Instance.GetSelectedUnit().IsEnemy()) continue;
                        GetNode(x, z).SetIsWalkable(false);

                    }
                    else if (IsObstacleOnGrid(worldPosition, out Transform objectTransform))
                    {
                        if (objectTransform.CompareTag("Tier2_Obstacles"))
                        {
                            if (unitType == UnitType.Aerial) continue;
                            if (objectTransform.GetComponent<Obstacle>().IsBroken()) continue;
                        }
                        if (unitType == UnitType.Aerial && objectTransform.CompareTag("Tier3_Obstacles")) continue;
                        GetNode(x, z).SetIsWalkable(false);
                    }

                }
            }
        }

        public void SetupPath(bool ignoreObstacle)
        {

            gridSystem = new GridSystem<PathNode>(width, height, cellSize,
                (GridSystem<PathNode> g, GridPosition gridPosition) => new PathNode(gridPosition));
            // gridSystem.CreateDebugObjects(gridDebugObjectPrefab, debugParent);

            for (int x = 0; x < width; x++)
            {
                for (int z = 0; z < height; z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);

                    if (!IsObstacleOnGrid(worldPosition, out string objectTag)) continue;
                    if (ignoreObstacle && objectTag == "Tier3_Obstacles") continue;

                    GetNode(x, z).SetIsWalkable(false);

                }
            }
        }

        /// <summary>Checks if there is an obstacle at the given world position.</summary>
        /// <param name="worldPosition">The world position to check for obstacles.</param>
        /// <returns>True if an obstacle is present; otherwise, false.</returns>
        public bool IsObstacleOnGrid(Vector3 worldPosition, out string objectTag)
        {
            float raycastOffsetDistance = 5f;
            if (Physics.Raycast(
                        worldPosition + Vector3.down * raycastOffsetDistance,
                        Vector3.up,
                        out RaycastHit hit,
                        raycastOffsetDistance * 2,
                        obstaclesLayerMask
                    ))
            {
                objectTag = hit.transform.gameObject.tag;
                return true;
            }

            objectTag = "";
            return false;
        }

        public bool IsObstacleOnGrid(Vector3 worldPosition, out Transform objectTransform)
        {
            float raycastOffsetDistance = 5f;
            if (Physics.Raycast(
                        worldPosition + Vector3.down * raycastOffsetDistance,
                        Vector3.up,
                        out RaycastHit hit,
                        raycastOffsetDistance * 2,
                        obstaclesLayerMask
                    ))
            {
                objectTransform = hit.transform;
                return true;
            }

            objectTransform = null;
            return false;
        }

        /// <summary>Finds a path from the start grid position to the end grid position.</summary>
        /// <param name="startGridPosition">The starting grid position.</param>
        /// <param name="endGridPosition">The ending grid position.</param>
        /// <param name="pathLength">The length of the found path (output).</param>
        /// <returns>A list of grid positions representing the path, or null if no path is found.</returns>
        public List<GridPosition> FindPath(GridPosition startGridPosition, GridPosition endGridPosition, out int pathLength)
        {
            List<PathNode> openList = new List<PathNode>();
            List<PathNode> closedList = new List<PathNode>();

            PathNode startNode = gridSystem.GetGridObject(startGridPosition);
            PathNode endNode = gridSystem.GetGridObject(endGridPosition);
            openList.Add(startNode);

            for (int x = 0; x < gridSystem.GetWidth(); x++)
            {
                for (int z = 0; z < gridSystem.GetHeight(); z++)
                {
                    GridPosition gridPosition = new GridPosition(x, z);
                    PathNode pathNode = gridSystem.GetGridObject(gridPosition);

                    pathNode.SetGCost(int.MaxValue);
                    pathNode.SetHCost(0);
                    pathNode.CalculateFCost();
                    pathNode.ResetCameFromPathNode();
                }
            }

            startNode.SetGCost(0);
            startNode.SetHCost(CalculateDistance(startGridPosition, endGridPosition));
            startNode.CalculateFCost();

            while (openList.Count > 0)
            {
                PathNode currentNode = GetLowestFCostPathNode(openList);

                if (currentNode == endNode)
                {
                    // Reached final node
                    pathLength = endNode.GetFCost();
                    return CalculatePath(endNode);
                }

                openList.Remove(currentNode);
                closedList.Add(currentNode);

                foreach (PathNode neighbourNode in GetNeighbourList(currentNode))
                {
                    if (closedList.Contains(neighbourNode))
                    {
                        continue;
                    }

                    if (!neighbourNode.IsWalkable())
                    {
                        closedList.Add(neighbourNode);
                        continue;
                    }

                    int tentativeGCost =
                        currentNode.GetGCost() + CalculateDistance(currentNode.GetGridPosition(), neighbourNode.GetGridPosition());

                    if (tentativeGCost < neighbourNode.GetGCost())
                    {
                        neighbourNode.SetCameFromPathNode(currentNode);
                        neighbourNode.SetGCost(tentativeGCost);
                        neighbourNode.SetHCost(CalculateDistance(neighbourNode.GetGridPosition(), endGridPosition));
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);
                        }
                    }
                }
            }

            // No path found
            pathLength = 0;
            return null;
        }

        /// <summary>Calculates the distance between two grid positions.</summary>
        /// <param name="gridPositionA">The first grid position.</param>
        /// <param name="gridPositionB">The second grid position.</param>
        /// <returns>The calculated distance between the two grid positions.</returns>
        public int CalculateDistance(GridPosition gridPositionA, GridPosition gridPositionB)
        {
            GridPosition gridPositionDistance = gridPositionA - gridPositionB;
            int xDistance = Mathf.Abs(gridPositionDistance.x);
            int zDistance = Mathf.Abs(gridPositionDistance.z);
            int remaining = Mathf.Abs(xDistance - zDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
        }

        /// <summary>Gets the lowest FCost path node from a list.</summary>
        /// <param name="pathNodeList">The list of path nodes to search.</param>
        /// <returns>The path node with the lowest FCost.</returns>
        private PathNode GetLowestFCostPathNode(List<PathNode> pathNodeList)
        {
            PathNode lowestFCostPathNode = pathNodeList[0];
            for (int i = 0; i < pathNodeList.Count; i++)
            {
                if (pathNodeList[i].GetFCost() < lowestFCostPathNode.GetFCost())
                {
                    lowestFCostPathNode = pathNodeList[i];
                }
            }
            return lowestFCostPathNode;
        }

        /// <summary>Gets the node at the specified grid position.</summary>
        /// <param name="x">The X-coordinate of the grid position.</param>
        /// <param name="z">The Z-coordinate of the grid position.</param>
        /// <returns>The path node at the specified grid position.</returns>
        private PathNode GetNode(int x, int z)
        {
            return gridSystem.GetGridObject(new GridPosition(x, z));
        }

        /// <summary>Gets a list of neighboring nodes for the given node.</summary>
        /// <param name="currentNode">The node for which to find neighbors.</param>
        /// <returns>A list of neighboring path nodes.</returns>
        private List<PathNode> GetNeighbourList(PathNode currentNode)
        {
            List<PathNode> neighbourList = new List<PathNode>();

            GridPosition gridPosition = currentNode.GetGridPosition();

            if (gridPosition.x - 1 >= 0)
            {
                // Left
                neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 0));
                if (gridPosition.z - 1 >= 0)
                {
                    // Left Down
                    neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z - 1));
                }

                if (gridPosition.z + 1 < gridSystem.GetHeight())
                {
                    // Left Up
                    neighbourList.Add(GetNode(gridPosition.x - 1, gridPosition.z + 1));
                }
            }

            if (gridPosition.x + 1 < gridSystem.GetWidth())
            {
                // Right
                neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 0));
                if (gridPosition.z - 1 >= 0)
                {
                    // Right Down
                    neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z - 1));
                }
                if (gridPosition.z + 1 < gridSystem.GetHeight())
                {
                    // Right Up
                    neighbourList.Add(GetNode(gridPosition.x + 1, gridPosition.z + 1));
                }
            }

            if (gridPosition.z - 1 >= 0)
            {
                // Down
                neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z - 1));
            }
            if (gridPosition.z + 1 < gridSystem.GetHeight())
            {
                // Up
                neighbourList.Add(GetNode(gridPosition.x + 0, gridPosition.z + 1));
            }

            return neighbourList;
        }

        /// <summary>Calculates the path from the end node to the start node.</summary>
        /// <param name="endNode">The end node of the path.</param>
        /// <returns>A list of grid positions representing the path.</returns>
        private List<GridPosition> CalculatePath(PathNode endNode)
        {
            List<PathNode> pathNodeList = new List<PathNode>();
            pathNodeList.Add(endNode);
            PathNode currentNode = endNode;
            while (currentNode.GetCameFromPathNode() != null)
            {
                pathNodeList.Add(currentNode.GetCameFromPathNode());
                currentNode = currentNode.GetCameFromPathNode();
            }

            pathNodeList.Reverse();

            List<GridPosition> gridPositionList = new List<GridPosition>();
            foreach (PathNode pathNode in pathNodeList)
            {
                gridPositionList.Add(pathNode.GetGridPosition());
            }

            return gridPositionList;
        }

        /// <summary>Checks if a grid position is walkable.</summary>
        /// <param name="gridPosition">The grid position to check for walkability.</param>
        /// <returns>True if the grid position is walkable; otherwise, false.</returns>
        public bool IsWalkableGridPosition(GridPosition gridPosition)
        {
            return gridSystem.GetGridObject(gridPosition).IsWalkable();
        }

        /// <summary>Checks if a path exists between two grid positions.</summary>
        /// <param name="startGridPosition">The starting grid position.</param>
        /// <param name="endGridPosition">The ending grid position.</param>
        /// <returns>True if a path exists between the two grid positions; otherwise, false.</returns>
        public bool HasPath(GridPosition startGridPosition, GridPosition endGridPosition)
        {
            return FindPath(startGridPosition, endGridPosition, out int pathLength) != null;
        }

        /// <summary>Gets the length of the path between two grid positions.</summary>
        /// <param name="startGridPosition">The starting grid position.</param>
        /// <param name="endGridPosition">The ending grid position.</param>
        /// <returns>The length of the path between the two grid positions.</returns>
        public int GetPathLength(GridPosition startGridPosition, GridPosition endGridPosition)
        {
            FindPath(startGridPosition, endGridPosition, out int pathLength);
            return pathLength;
        }
    }
}
